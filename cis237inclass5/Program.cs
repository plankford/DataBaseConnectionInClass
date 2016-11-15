using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237inclass5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make a new instance of the entiries class
            CarsPLankfordEntities carsPlankfordEntities = new CarsPLankfordEntities();

            //***************************************
            //List out the cars on the table
            //***************************************
            Console.WriteLine("Print the list");

            foreach(Car car in carsPlankfordEntities.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //**************************************
            //Find a specific one by the primary key
            //**************************************

            //Pull out a car from the table based on the id which is the primary key
            //If the record doesn't exist in the database, it will return null, so
            //check what you get back and see if it is null. If so, it doesn't exist
            //***********Find will only work for searching by the primary key*******************
            Car foundCar = carsPlankfordEntities.Cars.Find("V0LCD1814");
            /*
             * SQL statement
             * Select * from cars where id="V0LCD1814"
             */
            

            //Print it out
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Print out a found car using the Find Method");
            Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model + " " + foundCar.horsepower);

            //***************************************
            //Find a specific one by any property
            //***************************************

            //Call the Where method on the table cars and pass in a Lambda expression
            //for the criteria we are looking for. There is nothing special about the 
            //word car in the part that reads: car => car.id == "V0....". It could be 
            //any character we want it to be. It is just a variable name for the current
            //car we are considering in the epxression. This will automagically loop
            //through all the Cars, and run the expression against each of them. When the result
            //is finally true, it will return that car.
            Car carToFind = carsPlankfordEntities.Cars.Where(car => car.id == "V0LCD1814").First();
            /*
             *SQL Statement
             *Select * from cars where model ="Challenger" Limit 1
            */

            Car otherCarToFind = carsPlankfordEntities.Cars.Where(car => car.model == "Challenger").First();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find 2 specific cars");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);
            Console.WriteLine(otherCarToFind.id + " " + otherCarToFind.make + " " + otherCarToFind.model);

            //******************************************
            //Get out multiple cars
            //******************************************
            List<Car> queryCars = carsPlankfordEntities.Cars.Where(car => car.cylinders == 8).ToList();
            /*
             * SQL Statement
             * Select * from cars where cyclinders =8
             * */

            //The where clause returns a query object that cane be used to add
            //more queries to iot. Here we hae one where clause, and then
            //after we append another clause. Lastly we put it into a list
            //the actual query is not executed until the last part is done
            //we need to call something like toList or FIrst go get it to actually execute
            //the query on the server
            var queryObjectCars = carsPlankfordEntities.Cars.Where(car => car.cylinders == 8);
            queryObjectCars = queryObjectCars.Where(car => car.horsepower == 400);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find all cars that have 8 cyclinders");
            foreach (Car car in queryCars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //How to use contains to wild card search on a field
            List<Car> foundCars = carsPlankfordEntities.Cars.Where(
                car => car.make.ToLower().Contains("Cha".ToLower())).ToList();

            /****************************************************
             * Add a new car to the database
             * **************************************************/

            //Make a instance of a new car
            Car newCarToAdd = new Car();

            //Assign properties to the parts of the model
            newCarToAdd.id = "8888";
            newCarToAdd.make = "Nissan";
            newCarToAdd.model = "GT-R";
            newCarToAdd.horsepower = 550;
            newCarToAdd.cylinders = 8;
            newCarToAdd.year = "2016";
            newCarToAdd.type = "Car";

            try
            {
                //Add the new car to the Cars Collection
                carsPlankfordEntities.Cars.Add(newCarToAdd);

                //Presist the collection to the database. 
                //This call will actually to the work of saving changes to the database
                carsPlankfordEntities.SaveChanges();
            }
            catch (Exception e)
            {
                //Remove the new car we just added from the Cars Collection since we can't save it
                carsPlankfordEntities.Cars.Remove(newCarToAdd);

                //This catch might get thrown for other reasons than a primary key
                //Here I am assuming that.
                Console.WriteLine("Can't add the record. Already have one with that primary key");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just added a new car. Going to fetch and print to verify");
            carToFind = carsPlankfordEntities.Cars.Find("8888");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);

            /**********************************************
             * Update a Record
             * *******************************************/

            //Get out the car we want to update
            Car carToFindForUpdate = carsPlankfordEntities.Cars.Find("8888");

            //Output the car to find before the update
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("About to do an update on the following car");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model);

            Console.WriteLine("Doing the update now");

            //Update some of the properties of the car we found. We dont have to update all of the fields
            // if we do not want to
            carToFindForUpdate.make = "Nissssssssan";
            carToFindForUpdate.model = "GT-ARRRRRRRGH";
            carToFindForUpdate.horsepower = 8050;
            carToFindForUpdate.cylinders = 7;

            //Save the changes to the database. Since we got the model from the collection all we did was update
            //a reference of the object in the collection we wanted to update. There is no need to put the car back
            //into the collection since it is still there. All we have to do is save it
            carsPlankfordEntities.SaveChanges();

            //Get the car out now that we have updated
            carToFindForUpdate = carsPlankfordEntities.Cars.Find("8888");

            //Output tyhe updated car
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Outputing the Updated car");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model);

            /**********************************************
             * How to delete a record
             * ******************************************/

            //Get a car out of the database that we would like to delete
            Car carToFindForDelete = carsPlankfordEntities.Cars.Find("8888");

            //Remove the Car from the Cars collection
            carsPlankfordEntities.Cars.Remove(carToFindForDelete);

            //Save the changes to the database
            carsPlankfordEntities.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deleted the added car. Looking to see if it is still in the DB");

            //Check to see if the car was deleted
            try
            {
                //This statement will execute just fine. Its when we go to access the property id
                //on null that the exception will get thrown
                carToFindForDelete = carsPlankfordEntities.Cars.Find("8888");
                Console.WriteLine(carToFindForDelete.id + " " + carToFindForDelete.make + " " + carToFindForDelete.model);
            }
            catch (Exception e)
            {
                Console.WriteLine("The model you are looking for does not exist " + e.ToString() + " " + e.StackTrace);
            }

            //Another way to check to see if the record has been deleted
            if (carToFindForDelete == null)
            {
                Console.WriteLine("The model you are looking for does not exist ");
            }
        }
    }
}

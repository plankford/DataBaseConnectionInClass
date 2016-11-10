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

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find all cars that have 8 cyclinders");
            foreach (Car car in queryCars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }
        }
    }
}

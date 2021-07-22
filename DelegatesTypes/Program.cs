using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Func delegate
            FuncCar fc = new FuncCar();
            Func<string, string, double> myFuncDelegate = fc.ReturnPrice;
            double price = myFuncDelegate("Audi", "A5");
            Console.WriteLine($"price = {price}");

            // Action delegate
            Action<string, string> myActionDelegate = fc.DisplayData;
            myActionDelegate("BMW", "750");

            // Predicate delegate
            Car car = new Car();
            Car[] carList = new Car[]
            {
                new Car("Audi","A5", 15_000),
                new Car("Audi","A80", 9_000),
                new Car("BMW","320", 7_000),
                new Car("BMW","520", 5_000),
            };

            Predicate<Car> myPredicateDelegate = car.ReturnCheapPrice;
            Car tempCar = Array.Find(carList, myPredicateDelegate);
            Console.WriteLine($"Car: {tempCar.Brand} {tempCar.Model} , price = {tempCar.Price}");

            List<Car> cars = carList.ToList();
            Car car1 = cars.Find(myPredicateDelegate);
            List<Car> cars1 = cars.FindAll(myPredicateDelegate);
            cars1 = cars.FindAll(x => x.Price < 10_000);


            Console.WriteLine("---------------------");
            Car[] tempCars = Array.FindAll(carList, myPredicateDelegate);
            foreach (var item in tempCars)
            {
                Console.WriteLine($"Car: {item.Brand} {item.Model} , price = {item.Price}");
            }

            Console.ReadKey();
        }
    }

    class FuncCar
    {
        public double ReturnPrice(string brand, string model)
        {
            if (brand.Equals("Audi") && model.Equals("A5"))
            {
                return 45000;
            }
            if (brand.Equals("BMW") && model.Equals("750"))
            {
                return 60000;
            }
            return -1;
        }

        public void DisplayData(string brand, string model)
        {
            if (brand.Equals("Audi") && model.Equals("A5"))
            {
                Console.WriteLine("Audi A5");
                return;
            }
            if (brand.Equals("BMW") && model.Equals("750"))
            {
                Console.WriteLine("BMW 750");
                return;
            }
            Console.WriteLine("Unknown car");
        }
    }

    class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }

        public Car(string brand, string model, double price)
        {
            Brand = brand; Model = model; Price = price;
        }

        public Car() { }

        public bool ReturnCheapPrice(Car car)
        {
            return car.Price < 10_000;
        }
    }
}

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
                new Car("OPEL","Astra", 3_000),
                new Car("BMW","520", 5_000),
            };

            car.MaxPrice = 5000;
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

            // Converter delegate - convert data between datatypes
            UsedCar uc = new UsedCar();
            Converter<Car, UsedCar> myConverterDelegate =
                new Converter<Car, UsedCar>(uc.ReturnUsedCar);
            UsedCar[] ucArray = Array.ConvertAll(carList, myConverterDelegate);
            foreach (var item in ucArray)
            {
                Console.WriteLine($"Car: {item.Brand} {item.Model} , price = {item.Price}, oldCard = {item.IsOldCar} ");
            }

            // Comparison delegate - ordering/sorting elements inside collection
            Comparison<Car> myComparisonDelegate = new Comparison<Car>(car.CompareNames);
            Array.Sort(carList, myComparisonDelegate);
            Console.WriteLine("Sorted car list:");
            foreach (var item in carList)
            {
                Console.WriteLine($"Car: {item.Brand} {item.Model} , price = {item.Price} ");
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

        public double MaxPrice { get; set; }
        public bool ReturnCheapPrice(Car car)
        {
            return car.Price < MaxPrice; // 10_000;
        }

        public int CompareNames(Car car1, Car car2)
        {
            String s1 = $"{car1.Brand} {car1.Model}";
            String s2 = $"{car2.Brand} {car2.Model}";
            return s1.CompareTo(s2);
        }
    }

    class UsedCar : Car
    {
        public bool IsOldCar { get; set; }

        public UsedCar ReturnUsedCar(Car car)
        {
            return new UsedCar()
            {
                Model = car.Model, Brand = car.Brand, Price = car.Price*4.57,
                IsOldCar = true
            };
        }
    }
}

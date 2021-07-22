using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsExample
{
    class CarsComputer
    {
        private int temperature;
        private int pressure;

        public CarsComputer(int t, int p)
        {
            temperature = t;
            pressure = p;
        }
        public int GetPressure()
        {
            return pressure;
        }
        public int GetTemperature()
        {
            return temperature;
        }
    }

    // publisher class, responsible for generating information/sending events
    class DelegateOnBoardComputerEvent
    {
        // declare delegate for define the event
        public delegate void CarsComputerHandler(string info);
        // declare event based on delegate CarsComputerHandler
        public event CarsComputerHandler CarsComputerEventLog;

        Random rnd = new Random();
        public void ProcessLog()
        {
            for (int i = 0; i < 5; i++)
            {
                String s = $"Temp: {rnd.Next(70, 110)}, pressure: {rnd.Next(5, 8)}";
                OnCardComputerEventLog(s);
                Thread.Sleep(1000);
            }
        }

        public void OnCardComputerEventLog(string info)
        {
            if (CarsComputerEventLog!=null)
                CarsComputerEventLog(info);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DelegateOnBoardComputerEvent ce = new DelegateOnBoardComputerEvent();
            //ce.CarsComputerEventLog +=
            //    new DelegateOnBoardComputerEvent.CarsComputerHandler(DisplayInformation);
            ce.CarsComputerEventLog += Ce_CarsComputerEventLog;

            Thread thread = new Thread(() =>
           {
               ce.ProcessLog();
           });
            thread.Start();
            

            Console.ReadKey();
        }

        private static void Ce_CarsComputerEventLog(string info)
        {
            Console.WriteLine(info);
        }

        static void DisplayInformation(string info)
        {
            Console.WriteLine(info);
        }
    }
}

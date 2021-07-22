using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void ProcessLog()
        {

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
        }
    }
}

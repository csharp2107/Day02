using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExample
{

    class DelegateSample
    {
        public int FirstMethod(string s, int n)
        {
            Console.WriteLine("First method");
            Console.WriteLine(s);
            return n;
        }
    }

    class Program
    {
        public delegate double Operation(double d1, double d2);
        public delegate int MyDelegate(string s, int n);

        static int totalCounter;

        public static double Add(double n1, double n2)
        {
            totalCounter++;
            return n1 + n2;
        }

        public static double Sub(double n1, double n2)
        {
            totalCounter++;
            return n1 - n2;
        }

        static int result;
        static MyDelegate myDelegate;
        static void Main(string[] args)
        {

            //Operation oper1 = new Operation(Add);
            Operation oper1 = delegate (double x1, double x2)
            {
                return x1 + x2;
            };
            Console.WriteLine($"2+2 = {oper1(2, 2)}");

            /*
            Operation oper2 = new Operation(Sub);
            Console.WriteLine($"2-4 = {oper2(2,4)}");

            totalCounter = 0;
            Operation complexOper;
            complexOper = oper1;
            complexOper += oper2;
            Console.WriteLine($"Complex result = {complexOper(5, 5)}, total counter={totalCounter}");

            Console.WriteLine("------------");

            complexOper -= oper1;
            Console.WriteLine($"Complex result = {complexOper(2, 5)}, total counter={totalCounter}");
            */

            DelegateSample ds = new DelegateSample();
            myDelegate = new MyDelegate(ds.FirstMethod);
            result = myDelegate("ABC", 1);
            Console.WriteLine(result);

            // async invoking of delegate
            IAsyncResult asyncResult = myDelegate.BeginInvoke("XYZ", 10, null, null);
            // some delay
            result = myDelegate.EndInvoke(asyncResult);
            Console.WriteLine(result);

            // async invoking - callback method
            result = 0;
            asyncResult = myDelegate.BeginInvoke("XYZ", 20, delegateCallback, null);

            Console.ReadKey();

        }

        static void delegateCallback(IAsyncResult ar)
        {
            result = myDelegate.EndInvoke(ar);
            Console.WriteLine(result);
        }
    }
}

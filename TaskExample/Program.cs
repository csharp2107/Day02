using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task started");

            // simulate cancellation of task
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(500);
            CancellationToken token = cts.Token;

            //Task taskCancel = Task.Run(() =>
            //{
            //    try
            //    {
            //        long res = 0;
            //        for (int i = 0; i < 10_000_000; i++)
            //        {
            //            Thread.Sleep(500);
            //            res += i;
            //            Console.WriteLine($"counter = {i}");
            //            if (token.IsCancellationRequested)
            //            {
            //                token.ThrowIfCancellationRequested();
            //            }
            //        }
            //    }
            //    catch (OperationCanceledException exc)
            //    {
            //        return;
            //    }
            //}, token);

            //taskCancel.Wait();
            //Console.WriteLine(taskCancel.Status.ToString());


            Task task1 = new Task(PrintCounter);
            task1.Start();
            //task1.Wait();

            Task task2 = Task.Factory.StartNew(PrintCounter);

            Task task3 = Task.Run(() =>
           {
               PrintCounter();
           });

            Task<List<int>> task4 = Task.Run(() =>
            {
                List<int> list = new List<int>();
                list.Add(1);
                Console.WriteLine("Task4 done");
                return list;
            });
            task4.ContinueWith((x) =>
           {
               List<int> input_ = x.Result;
               Console.WriteLine("Continue after task 4");
           }, TaskContinuationOptions.OnlyOnRanToCompletion);

            //task4.Wait();
            //List<int> res = task4.Result;
            //Console.WriteLine($"Finished with value {res[0]}");

            Task[] taskArray = new Task[] { task1, task2, task3, task4 };
            Task.WaitAll(taskArray);
            //Task.WaitAny(taskArray);

            Console.WriteLine("Task finished");

            Console.ReadKey();
        }

        static public void PrintCounter()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] count value: {i}");
                Thread.Sleep(500);
            }
        }
    }

}

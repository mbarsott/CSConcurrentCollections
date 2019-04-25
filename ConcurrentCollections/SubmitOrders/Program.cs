using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubmitOrders
{
    internal class Program
    {
        private static readonly object _lockObj = new object();

        private static void Main(string[] args)
        {
            var orders = new Queue<string>();
            //            var orders = new ConcurrentQueue<string>();
            //            var task1 = Task.Run(()=>PlaceOrders(orders, "Mark"));
            //            var task2 = Task.Run(()=> PlaceOrders(orders, "Ramdevi"));
            //            Task.WaitAll(task1, task2);
            PlaceOrders(orders, "Mark");
            PlaceOrders(orders, "Ramdevi");
            //            foreach (var order in orders) ProcessOrder(order);
            Parallel.ForEach(orders, ProcessOrder);
            foreach (var order in orders)
            {
                Console.WriteLine($"ORDER: {order}");
            }
        }

        private static void ProcessOrder(string order)
        {
        }

        //        private static void PlaceOrders(ConcurrentQueue<string> orders, string customerName)
        private static void PlaceOrders(Queue<string> orders, string customerName)
        {
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(10);
                var orderName = string.Format($"{customerName} wants t-shirt {i + 1}");
                lock (_lockObj)
                {
                    orders.Enqueue(orderName);
                }
            }
        }
    }
}
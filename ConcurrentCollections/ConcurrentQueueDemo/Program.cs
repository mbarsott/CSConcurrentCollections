using System;
using System.Collections.Concurrent;

namespace ConcurrentQueueDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
//            var shirts = new ConcurrentQueue<string>();
//            var shirts = new ConcurrentStack<string>();
//            var shirts = new ConcurrentBag<string>();
            IProducerConsumerCollection<string> shirts = new ConcurrentBag<string>();

            // You enqueue and dequeue with queues
//            shirts.Enqueue("Pluralsight");
//            shirts.Enqueue("WordPress");
//            shirts.Enqueue("Code School");

            // You push and pop with stacks
//            shirts.Push("Pluralsight");
//            shirts.Push("WordPress");
//            shirts.Push("Code School");

            // You add and take with bags
//            shirts.Add("Pluralsight");
//            shirts.Add("WordPress");
//            shirts.Add("Code School");

            shirts.TryAdd("Pluralsight");
            shirts.TryAdd("WordPress");
            shirts.TryAdd("Code School");

            Console.WriteLine("After enqueuing, count = " + shirts.Count);

            bool success = shirts.TryTake(out string item1);
            if (success)
            {
                Console.WriteLine($"\r\nRemoving {item1}");
            }
            else
            {
                Console.WriteLine($"queue was empty");
            }
            
            // IProducerConsumerCollection does not allow to peek
//            success= shirts.TryPeek(out string item2);
//            if (success)
//            {
//                Console.WriteLine($"Peeking {item2}");
//            }
//            else
//            {
//                Console.WriteLine($"Peeking {item2}");
//            }
            Console.WriteLine("\r\nEnumerating:");
            foreach (var item in shirts) Console.WriteLine(item);

            Console.WriteLine($"\r\nAfter enumerating, count = {shirts.Count.ToString()}");
        }
    }
}
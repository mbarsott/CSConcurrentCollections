using System;
using System.Collections.Concurrent;

namespace BasicDictionaryOps
{
    class Program
    {
        static void Main(string[] args)
        {
            //            IDictionary<string, int> stock = new ConcurrentDictionary<string, int>();
            var stock = new ConcurrentDictionary<string, int>();
            stock.TryAdd("jDays", 4);
            stock.TryAdd("technologyhour", 3);

            Console.WriteLine($"No. of shirts in stock = {stock.Count}");

            bool success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine($"Add succeeded? {success}");
            success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine($"Add succeeded? {success}");

            stock["buddhistgeeks"] = 5;

            //            stock["pluralsight"] = 7; // up from 6, we just bought one
            //            success = stock.TryUpdate("pluralsight", 7, 6);
            //            Console.WriteLine($"\r\npluralsight = {stock["pluralsight"]}, did update work? {success}");
            int psStock = stock.AddOrUpdate("pluralsight", 1, (key, oldvalue) => oldvalue + 1);
            Console.WriteLine("New value is " + psStock);

            Console.WriteLine($"stock[pluralsight] = {stock.GetOrAdd("pluralsight", 0)}");

            //            stock.Remove("jDays");
            success = stock.TryRemove("jDays", out int jDaysValue);
            if (success)
            {
                Console.WriteLine($"Value removed was: {jDaysValue}");
            }

            Console.WriteLine("\r\nEnumerating:");
            foreach (var keyValPair in stock)
            {
                Console.WriteLine($"{keyValPair.Key}: {keyValPair.Value}");
            }
        }
    }
}

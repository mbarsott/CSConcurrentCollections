using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryPerformance
{
    public class SingleThreadBenchmark
    {
        static void PopulateDict(IDictionary<int, int> dict, int dictSize)
        {
            for (int i = 0; i < dictSize; i++)
            {
                dict.Add(i,0);
            }

            for (int i = 0; i < dictSize; i++)
            {
                dict[i] += 1;
                Worker.DoSomethingTimeConsuming();
            }
        }

        static int GetTotalValue(IDictionary<int, int> dict)
        {
            int total = 0;
            foreach (var item in dict)
            {
                total += dict[item.Key];
                Worker.DoSomethingTimeConsuming();
            }

            return total;
        }

        public static void TimeDict(IDictionary<int, int> dict, int dictSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            PopulateDict(dict, dictSize);
            stopwatch.Stop();
            Console.WriteLine($"Time taken to build dictionary (ms):  {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();
            int total = GetTotalValue(dict);
            stopwatch.Stop();
            Console.WriteLine($"Time taken to enumerate dictionary (ms): {stopwatch.ElapsedMilliseconds}");

            Console.WriteLine($"total is {total.ToString()}");
            if (total != dictSize)
            {
                Console.WriteLine("ERROR IN TOTAL!");
            }
        }
    }
}

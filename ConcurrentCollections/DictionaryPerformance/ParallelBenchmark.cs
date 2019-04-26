using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DictionaryPerformance
{
    public class ParallelBenchmark
    {
        static void PopulateDictParallel(ConcurrentDictionary<int, int> dict, int dictSize)
        {
            Parallel.For(0, dictSize, (i) => dict.TryAdd(i, 0));

            Parallel.For(0, dictSize,
                (i) =>
                {
                    bool done = dict.TryUpdate(i, 1, 0);
                    if (!done)
                    {
                        throw new Exception("Error updating. Old value was " + dict[i]);
                    }
                    Worker.DoSomethingTimeConsuming();
                });
        }

        static int GetTotalValueParallel(ConcurrentDictionary<int, int> dict)
        {
            int total = 0;
            Parallel.ForEach(dict,
                KeyValPair =>
                {
                    Interlocked.Add(ref total, KeyValPair.Value);
                    Worker.DoSomethingTimeConsuming();
                });
            return total;
        }

        public static void TimeDict(ConcurrentDictionary<int, int> dict, int dictSize)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            PopulateDictParallel(dict, dictSize);
            stopwatch.Stop();
            Console.WriteLine($"Time taken to build dictionary (ms):  {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();
            int total = GetTotalValueParallel(dict);
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

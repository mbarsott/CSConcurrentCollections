using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            int dictSize = 1000000;
            Console.WriteLine("Dictionary, single thread:");
            var dict = new Dictionary<int, int>();
            SingleThreadBenchmark.TimeDict(dict, dictSize);

            Console.WriteLine("\r\nConcurrentDictionary, single thread:");
            var dict2 = new ConcurrentDictionary<int, int>();
            SingleThreadBenchmark.TimeDict(dict2, dictSize);
 
            Console.WriteLine("\r\nConcurrentDictionary, multiple thread:");
            dict2 = new ConcurrentDictionary<int, int>();
            ParallelBenchmark.TimeDict(dict2, dictSize);

        }
    }
}

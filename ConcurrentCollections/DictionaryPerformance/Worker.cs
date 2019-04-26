using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryPerformance
{
    public class Worker
    {
        public static int DoSomethingTimeConsuming()
        {
            int total = 0;
            for (int i = 0; i < 1000; i++)
            {
                total += 1; 
            }

            return total;
        } 
    }
}

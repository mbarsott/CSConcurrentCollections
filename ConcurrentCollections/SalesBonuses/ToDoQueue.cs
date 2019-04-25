using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SalesBonuses
{
    public class ToDoQueue
    {
        private readonly ConcurrentQueue<Trade> _queue = new ConcurrentQueue<Trade>();
        private bool _workingDayComplete = false;
        private readonly StaffLogsForBonuses _staffLogs;

        public ToDoQueue(StaffLogsForBonuses staffResults)
        {
            _staffLogs = staffResults;
        }

        public void AddTrade(Trade transaction)
        {
            _queue.Enqueue(transaction);
        }

        public void CompleteAdding()
        {
            _workingDayComplete = true;
        }

        public void MonitorAndLogTrades()
        {
            while (true)
            {
                bool done = _queue.TryDequeue(out Trade nextTrade);
                if (done)
                {
                    _staffLogs.Processtrade(nextTrade);
                    Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
                }
                else if (_workingDayComplete)
                {
                    Console.WriteLine("No more sales to log. Exiting.");
                    break;
                }
                else
                {
                    Console.WriteLine("No transactions available.");
                    Thread.Sleep(500);
                }
            }
        }
    }
}
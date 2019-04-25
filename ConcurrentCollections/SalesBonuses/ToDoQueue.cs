using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SalesBonuses
{
    public class ToDoQueue
    {
        private readonly BlockingCollection<Trade> _queue = new BlockingCollection<Trade>(new ConcurrentQueue<Trade>());
//        private bool _workingDayComplete = false;
        private readonly StaffLogsForBonuses _staffLogs;

        public ToDoQueue(StaffLogsForBonuses staffResults)
        {
            _staffLogs = staffResults;
        }

        public void AddTrade(Trade transaction)
        {
            _queue.Add(transaction);
        }

        public void CompleteAdding()
        {
            //            _workingDayComplete = true;
            _queue.CompleteAdding();
        }

        public void MonitorAndLogTrades()
        {
            while (true)
            {
                //                bool done = _queue.TryTake(out Trade nextTrade);
                //                if (done)
                //                {
                //                    _staffLogs.Processtrade(nextTrade);
                //                    Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
                //                }
                //                else if (_workingDayComplete)
                //                {
                //                    Console.WriteLine("No more sales to log. Exiting.");
                //                    break;
                //                }
                //                else
                //                {
                //                    Console.WriteLine("No transactions available.");
                //                    Thread.Sleep(500);
                //                }
                try
                {
                    Trade nextTransaction = _queue.Take();
                    _staffLogs.Processtrade(nextTransaction);
                    Console.WriteLine("Processing transaction from " + nextTransaction.Person.Name);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }
    }
}
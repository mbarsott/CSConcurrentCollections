using System;
using System.Threading;

namespace BuyAndSell
{
    public class SalesPerson
    {
        public string Name { get; private set; }

        public SalesPerson(string name)
        {
            this.Name = name;
        }

        public void Work(StockController controller, TimeSpan workDay)
        {
            Random rand = new Random(Name.GetHashCode());
            DateTime start = DateTime.Now;
            while (DateTime.Now - start < workDay)
            {
                //                Thread.Sleep(rand.Next(100));
                bool buy = (rand.Next(6) == 0);
                string itemName = Program.AllShirtNames[rand.Next(Program.AllShirtNames.Count)];
                if (buy)
                {
                    int quantity = rand.Next(9) + 1;
                    controller.BuyStock(itemName, quantity);
                    //                    DisplayPurchase(itemName, quantity);
                }
                else
                {
                    bool success = controller.trySellItem(itemName);
                    //                    DisplaySaleAttempt(success, itemName);
                }
            }

            Console.WriteLine($"SalesPerson {Name} signing off");
        }

        private void DisplaySaleAttempt(bool success, string itemName)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (success)
            {
                Console.WriteLine($"Thread {threadId}: {Name} sold {itemName}");
            }
            else
            {
                Console.WriteLine($"Thread {threadId}: {Name}: Out of stock of {itemName}");
            }
        }

        private void DisplayPurchase(string itemName, int quantity)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {Name} bought {quantity} of {itemName}");
        }
    }
}

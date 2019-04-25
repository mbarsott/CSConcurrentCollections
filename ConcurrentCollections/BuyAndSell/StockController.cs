﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace BuyAndSell
{
    public class StockController
    {
        ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
        private int _totalQuantityBought;
        private int _totalQuantitySold;

        public void BuyStock(string item, int quantity)
        {
            _stock.AddOrUpdate(item, quantity, (key, oldValue) => oldValue + quantity);
            Interlocked.Add(ref _totalQuantityBought, quantity);
        }

        public void DisplayStatus()
        {
            int totalStock = _stock.Values.Sum();
            Console.WriteLine("\r\nBought = " + _totalQuantityBought);
            Console.WriteLine("Sold   = " + _totalQuantitySold);
            Console.WriteLine("Stock   = " + totalStock);
            int error = totalStock + _totalQuantitySold - _totalQuantityBought;
            if (error == 0)
            {
                Console.WriteLine("Stock levels match");
            }
            else
            {
                Console.WriteLine("Error in stock level: " + error);
            }

            Console.WriteLine();
            Console.WriteLine("Stock levels by item: ");
            foreach (var itemName in Program.AllShirtNames)
            {
                int stockLevel = _stock.GetOrAdd(itemName, 0);
                Console.WriteLine($"{itemName,-30}: {stockLevel}");
            }
        }

        public bool TrySellItem(string item)
        {
            bool success = false;
            int newStockLevel = _stock.AddOrUpdate(item,
                (itemName) =>
                {
                    success = false;
                    return 0;
                },
                (itemName, oldValue) =>
                {
                    if (oldValue == 0)
                    {
                        success = false;
                        return 0;
                    }
                    else
                    {
                        success = true;
                        return oldValue - 1;
                    }
                });
            if (success)
            {
                Interlocked.Increment(ref _totalQuantitySold);
            }

            return success;
        }

        public bool TrySellItem2(string item)
        {
            int newStockLevel = _stock.AddOrUpdate(item, -1, (key, oldValue) => oldValue - 1);
            if (newStockLevel < 0)
            {
                _stock.AddOrUpdate(item, 1, (key, oldvalue) => oldvalue + 1);
                return false;
            }
            else
            {
                Interlocked.Increment(ref _totalQuantitySold);
                return true;
            }
        }
    }
}

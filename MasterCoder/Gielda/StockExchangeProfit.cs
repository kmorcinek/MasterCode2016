using System.Collections.Generic;

namespace StockExchangeProfit
{
    public class StockExchangeProfit : IStockExchangeProfit
    {
        public new static IStockExchangeProfit GetInstance()
        {
            stockInstance = new StockExchangeProfit();
            return stockInstance;
        }

        public override TaskResult FindMaxProfitRecursive(List<int> values)
        {
            TaskResult bestTask = CreateBestTask(values, 0, 1);

            for (int i = 0; i < values.Count - 1; i++)
            {
                int jMaxIndex = i + 1;
                int jMaxValue = values[jMaxIndex];
                for (int j = i + 1; j < values.Count; j++)
                {
                    if (values[j] > jMaxValue)
                    {
                        jMaxIndex = j;
                        jMaxValue = values[jMaxIndex];
                    }
                }

                var currentTask = CreateBestTask(values, i, jMaxIndex);

                if (currentTask.Value > bestTask.Value)
                {
                    bestTask = currentTask;
                }
            }

            return bestTask;
//            for i od 0 do konca
// szukamy maksa gdzie j > i
// Zapisujemy jako TaskResult, obliczajac profit

            // to samo dla nastepnego i liczymy profit.
        }

        static TaskResult CreateBestTask(List<int> values, int leftIndex, int rightIndex)
        {
            return new TaskResult
            {
                LeftIndex = leftIndex,
                RightIndex = rightIndex,
                Value = values[rightIndex] - values[leftIndex]
            };
        }
    }
}
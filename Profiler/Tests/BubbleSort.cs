using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class BubbleSort
    {
        [TestMethod]
        public void BubbleSortTest()
        {
            Random rand = new Random();
            int[] data = new int[1000];
            for (int i = 0; i < 1000; i++)
                data[i] = rand.Next(int.MinValue, int.MaxValue);
            DoBubbleSort(data);
            Profiler.Profiler.ExportCsv();
        }

        public void DoBubbleSort(int[] arrayToSort)
        {
            Profiler.Profiler.Start("Main Loop");
            for (int i = arrayToSort.Length - 1; i > 0; i--)
            {
                Profiler.Profiler.Start("Second Loop");
                for (int j = 0; j <= i - 1; j++)
                {
                    if (arrayToSort[j] > arrayToSort[j + 1])
                    {
                        Profiler.Profiler.Start("Swap");
                        int maxValue = arrayToSort[j];
                        arrayToSort[j] = arrayToSort[j + 1];
                        arrayToSort[j + 1] = maxValue;
                        Profiler.Profiler.Close(); //Close the "Swap" scope
                    }
                }
                Profiler.Profiler.Close(); //Close the "Second Loop" scope
            }
            Profiler.Profiler.Close(); //Close the "Main Loop" scope
        }
    }
}

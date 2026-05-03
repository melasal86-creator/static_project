using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] data = { 115, 182, 191, 31, 196, 1099, 5, 172, 10, 179, 83, 21, 20, 21, 186, 177, 195, 193, 188, 199, 62, 109, 105, 183, 110 };

            int n = data.Length;
            Array.Sort(data);

            double sum = 0;
            foreach (var x in data) sum += x;
            double mean = sum / n;

            var groups = data.GroupBy(v => v).OrderByDescending(g => g.Count());
            double mode = groups.First().Key;

            double median = GetPercentile(data, 50);

            double varianceSum = 0;
            foreach (var x in data) varianceSum += Math.Pow(x - mean, 2);
            double variance = varianceSum / n;

            double stdDev = Math.Sqrt(variance);

            double p20 = GetPercentile(data, 20);
            double q1 = GetPercentile(data, 25);
            double q3 = GetPercentile(data, 75);

            double range = data.Max() - data.Min();
            double iqr = q3 - q1;

            double sumDev = 0;
            foreach (var x in data) sumDev += (x - mean);

            Console.WriteLine("--- Statistics Results ---");
            Console.WriteLine($"Mean: {mean:F2}");
            Console.WriteLine($"Mode: {mode}");
            Console.WriteLine($"Median: {median}");
            Console.WriteLine($"Variance: {variance:F2}");
            Console.WriteLine($"Standard Deviation: {stdDev:F2}");
            Console.WriteLine($"P20: {p20}");
            Console.WriteLine($"P50 (Median): {median}");
            Console.WriteLine($"First Quartile (Q1): {q1}");
            Console.WriteLine($"Third Quartile (Q3): {q3}");
            Console.WriteLine($"Range: {range}");
            Console.WriteLine($"Interquartile Range (IQR): {iqr}");
            Console.WriteLine($"Summation of Deviations: {Math.Round(sumDev, 2)}");

            Console.WriteLine("\n--- Outliers Detection ---");
            double lowerBound = q1 - 1.5 * iqr;
            double upperBound = q3 + 1.5 * iqr;

            foreach (var x in data)
            {
                if (x < lowerBound || x > upperBound)
                    Console.WriteLine($"{x} is an Outlier");
                else
                    Console.WriteLine($"{x} is Normal");
            }

            Console.ReadKey();
        }

        static double GetPercentile(double[] sortedData, double percentile)
        {
            double i = (percentile / 100) * (sortedData.Length - 1);
            int index = (int)Math.Floor(i);
            double fraction = i - index;

            if (index + 1 < sortedData.Length)
                return sortedData[index] + (fraction * (sortedData[index + 1] - sortedData[index]));
            else
                return sortedData[index];
        }
    }
}
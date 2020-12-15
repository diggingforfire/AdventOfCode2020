using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _15._02
{
    class Program
    {
        static async Task Main()
        {
            var startingNumbers = (await File.ReadAllTextAsync("input.txt")).Split(",").Select(int.Parse).ToArray();

            var spokenNumbersWithCount =
                new Dictionary<int, List<int>>(startingNumbers.Select((number, index) =>
                    new KeyValuePair<int, List<int>>(number, new List<int>(new[] { index }))));

            int lastSpokenNumber = startingNumbers[^1];
            for (int i = startingNumbers.Length; i < 30000000; i++)
            {
                lastSpokenNumber =
                    spokenNumbersWithCount[lastSpokenNumber].Count == 1 ? 0 :
                        spokenNumbersWithCount[lastSpokenNumber][^1] - spokenNumbersWithCount[lastSpokenNumber][^2];

                if (!spokenNumbersWithCount.ContainsKey(lastSpokenNumber))
                {
                    spokenNumbersWithCount.Add(lastSpokenNumber, new List<int>());
                }

                spokenNumbersWithCount[lastSpokenNumber].Add(i);
            }

            Console.WriteLine(lastSpokenNumber);
        }
    }
}

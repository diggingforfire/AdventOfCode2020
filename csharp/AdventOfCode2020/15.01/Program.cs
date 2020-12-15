using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _15._01
{
    class Program
    {
        static async Task Main()
        {
            var startingNumbers = (await File.ReadAllTextAsync("input.txt")).Split(",").Select(int.Parse).ToArray();
            Solve(startingNumbers, 2020);
        }

        static void Solve(int[] startingNumbers, int targetIndex, int index = 0, int lastSpokenNumber = 0, Dictionary<int, List<int>> cache = null)
        {
            cache ??= new Dictionary<int, List<int>>();
            int revolvingIndex = index % startingNumbers.Length;
            int number = startingNumbers[revolvingIndex];
            
            if (cache.ContainsKey(number))
            {
                if (cache[lastSpokenNumber].Count == 1)
                {
                    number = 0;
                }
                else
                {
                    number = cache[lastSpokenNumber][^1] - cache[lastSpokenNumber][^2];
                }

                if (index + 1 == targetIndex)
                {
                    Console.WriteLine(number);
                }

                if (!cache.ContainsKey(number))
                {
                    cache.Add(number, new List<int>());
                }

                cache[number].Add(index);
                
            }
            else
            {
                cache.Add(number, new List<int>{ revolvingIndex });
            }

            Solve(startingNumbers, targetIndex, ++index, number, cache);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _23._01
{
    class Program
    {
        static void Main()
        {
            var sw = Stopwatch.StartNew();

            var input = "389125467".Select(c => int.Parse(c.ToString())).ToList();
            var cups = new LinkedList<int>(input);

            const int numberOfMoves = 100;
            const int sliceSize = 3;

            var current = cups.First;

            for (int i = 0; i < numberOfMoves; i++)
            {
                var nextCups = new List<LinkedListNode<int>>();

                var toMove = current;
                for (int j = 0; j < sliceSize; j++)
                {
                    toMove = toMove.Next ?? cups.First;
                    nextCups.Add(toMove);
                }

                foreach (var node in nextCups)
                {
                    cups.Remove(node);
                }

                int destinationCupValue = current.Value;
                do
                {
                    destinationCupValue--;
                    if (destinationCupValue < cups.Min())
                    {
                        destinationCupValue = cups.Max();
                    }
                } while (!cups.Contains(destinationCupValue));

                var destinationCup = cups.Find(destinationCupValue);

                for (int j = 2; j >= 0; j--)
                {
                    cups.AddAfter(destinationCup, nextCups[j]);
                }

                current = current.Next ?? cups.First;
            }

            var inputLabels = cups.Select(i => i.ToString()).ToList();
            var orderedLabels =
                inputLabels.Concat(inputLabels).Skip(inputLabels.IndexOf("1") + 1).Take(inputLabels.Count - 1)
                    .SelectMany(c => c).ToArray();

            sw.Stop();
            Console.WriteLine(new string(orderedLabels));
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}

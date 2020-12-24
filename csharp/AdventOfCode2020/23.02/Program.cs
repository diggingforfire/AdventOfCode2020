using System;
using System.Collections.Generic;
using System.Linq;

namespace _23._02
{
    class Program
    {
        static void Main()
        {
            var input = "318946572".Select(c => int.Parse(c.ToString()))
                .Concat(Enumerable.Range(10, 1_000_000 - 9)).ToList();
            var cups = new LinkedList<int>(input);

            const int numberOfMoves = 10_000_000;
            const int sliceSize = 3;

            var current = cups.First;

            var lookup = new Dictionary<int, LinkedListNode<int>>();
            LinkedListNode<int> lookupNode = cups.First;
            do
            {
                lookup.Add(lookupNode.Value, lookupNode);
                lookupNode = lookupNode.Next;
            } while (lookupNode != null);

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
                    lookup.Remove(node.Value);
                }

                int destinationCupValue = current.Value;
                do
                {
                    destinationCupValue--;
                    if (destinationCupValue < 1)
                    {
                        destinationCupValue = 1_000_000;
                    }
                } while (!lookup.ContainsKey(destinationCupValue));

                var destinationCup = lookup[destinationCupValue];

                for (int j = 2; j >= 0; j--)
                {
                    cups.AddAfter(destinationCup, nextCups[j]);
                    lookup.Add(nextCups[j].Value, nextCups[j]);
                }

                current = current.Next ?? cups.First;
            }

            long result = (long)lookup[1].Next.Value * (long)lookup[1].Next.Next.Value;

            Console.WriteLine(result);
        }
    }
}

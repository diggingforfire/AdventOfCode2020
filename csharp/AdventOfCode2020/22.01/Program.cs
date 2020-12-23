using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _22._01
{
    class Program
    {
        static async Task Main()
        {
            var decks =
                (await File.ReadAllTextAsync("input.txt"))
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(line => new Queue<int>(Regex.Matches(line, "[0-9]+(?!:)").Select(match => match.Value)
                    .Select(int.Parse).ToArray()))
                .ToArray();

            var score = Play(decks[0], decks[1]);
            Console.WriteLine(score);
        }

        static int Play(Queue<int> p1Deck, Queue<int> p2Deck)
        {
            if (p1Deck.Count == 0 || p2Deck.Count == 0)
            {
                var winningDeck = p1Deck.Count == 0 ? p2Deck : p1Deck;
                return winningDeck.Select((n, i) => n * (winningDeck.Count - i)).Sum();
            }

            var p1Card = p1Deck.Dequeue();
            var p2Card = p2Deck.Dequeue();

            if (p1Card > p2Card)
            {
                p1Deck.Enqueue(p1Card);
                p1Deck.Enqueue(p2Card);
       
            }
            else
            {
                p2Deck.Enqueue(p2Card);
                p2Deck.Enqueue(p1Card);
            }

            return Play(p1Deck, p2Deck);
        }
    }
}

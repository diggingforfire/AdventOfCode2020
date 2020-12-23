using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _22._02
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
            
            Play(decks[0], decks[1]);

            var winningDeck = decks[0].Count == 0 ? decks[1] : decks[0];
            var score = winningDeck.Select((n, i) => n * (winningDeck.Count - i)).Sum();

            Console.WriteLine(score);
        }

        static bool Play(Queue<int> p1Deck, Queue<int> p2Deck)
        {
            var p1PreviousDecks = new List<string>();
            var p2PreviousDecks = new List<string>();

            while (p1Deck.Any() && p2Deck.Any())
            {
                var p1DeckStr = string.Join(",", p1Deck.Select(i => (char)i));
                var p2DeckStr = string.Join(",", p2Deck.Select(i => (char)i));

                bool decksWereSeenBefore =
                    p1PreviousDecks.Any(previous => previous == p1DeckStr) ||
                    p2PreviousDecks.Any(previous => previous == p2DeckStr);

                if (decksWereSeenBefore)
                {
                    return true;
                }

                var p1DeckCopy = string.Join(",", p1Deck.Select(i => (char)i));
                var p2DeckCopy = string.Join(",", p2Deck.Select(i => (char)i));

                p1PreviousDecks.Add(p1DeckCopy);
                p2PreviousDecks.Add(p2DeckCopy);

                var p1Card = p1Deck.Dequeue();
                var p2Card = p2Deck.Dequeue();

                var p1Wins = p1Card > p2Card;

                if (p1Deck.Count >= p1Card && p2Deck.Count >= p2Card)
                {
                    var p1SlicedDeck = new Queue<int>(p1Deck.Take(p1Card));
                    var p2SlicedDeck = new Queue<int>(p2Deck.Take(p2Card));
                    p1Wins = Play(p1SlicedDeck, p2SlicedDeck);
                }

                if (p1Wins)
                {
                    p1Deck.Enqueue(p1Card);
                    p1Deck.Enqueue(p2Card);
                }
                else
                {
                    p2Deck.Enqueue(p2Card);
                    p2Deck.Enqueue(p1Card);
                }
            }

            return p1Deck.Count > 0;
        }
    }
}

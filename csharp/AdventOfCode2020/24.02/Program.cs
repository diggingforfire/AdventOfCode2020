using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _24._02
{
    class Program
    {
        static (int, int) GettOffset(string direction, int currentY)
        {
            // https://www.redblobgames.com/grids/hexagons/#coordinates-offset
            var offset =
                direction switch
                {
                    "e" => (1, 0),
                    "w" => (-1, 0),
                    "se" => (currentY % 2 == 0 ? 0 : 1, 1),
                    "sw" => (currentY % 2 != 0 ? 0 : -1, 1),
                    "ne" => (currentY % 2 == 0 ? 0 : 1, -1),
                    "nw" => (currentY % 2 != 0 ? 0 : -1, -1)
                };

            return offset;
        }

        static (int, int)[] GetNeighbours((int, int) tile)
        {
            var neighbours = new[] {"e", "se", "sw", "w", "nw", "ne"}.Select(direction =>
            {
                var offset = GettOffset(direction, tile.Item2);
                return (tile.Item1 + offset.Item1, tile.Item2 + offset.Item2);
            }).ToArray();

            return neighbours;
        }

        static async Task Main()
        {
            var stepLists =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select(line => Regex.Matches(line, "(se|sw|nw|ne|e|w)+?").Select(m => m.Value)).ToArray();

            // black = true
            var flipState = new Dictionary<(int, int), bool>();

            foreach (var stepList in stepLists)
            {
                var start = (x: 0, y: 0);

                foreach (var step in stepList)
                {
                    var offset = GettOffset(step, start.y);
                    start.x += offset.Item1;
                    start.y += offset.Item2;
                }

                if (!flipState.ContainsKey(start))
                {
                    flipState.Add(start, true);
                }
                else
                {
                    flipState[start] = !flipState[start];
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var allNeighbours = flipState.Select(pair => pair.Key).SelectMany(GetNeighbours).ToArray();
                
                foreach (var neighbour in allNeighbours)
                {
                    flipState.TryAdd(neighbour, false);
                }

                var changes = flipState.Select(pair =>
                {
                    var neighbours = GetNeighbours(pair.Key).Where(tuple => flipState.ContainsKey(tuple));

                    var blackTileCount = neighbours.Select(tuple => flipState[tuple]).Count(b => b);

                    bool flips;
                    if (pair.Value) flips = blackTileCount == 0 || blackTileCount > 2;
                    else flips = blackTileCount == 2;

                    return new
                    {
                        pair.Key,
                        flips
                    };
                }).ToArray();

                foreach (var change in changes)
                {
                    if (change.flips)
                    {
                        flipState[change.Key] = !flipState[change.Key];
                    }

                }
            }

            var flipCount = flipState.Count(pair => pair.Value);
            Console.WriteLine(flipCount);
        }
    }
}

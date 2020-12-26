using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _24._01
{
    class Program
    {
        static async Task Main()
        {
            var stepLists =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select(line => Regex.Matches(line, "(se|sw|nw|ne|e|w)+?").Select(m => m.Value)).ToArray();

            var flipState = new Dictionary<(int, int), bool>();
            foreach (var stepList in stepLists)
            {
                var start = (x: 0, y: 0);

                foreach (var step in stepList)
                {
                    // https://www.redblobgames.com/grids/hexagons/#coordinates-offset
                    var offset =
                        step switch
                        {
                            "e" => (1, 0),
                            "w" => (-1, 0),
                            "se" => (start.y % 2 == 0 ? 0 : 1, 1),
                            "sw" => (start.y % 2 != 0 ? 0 : -1, 1),
                            "ne" => (start.y % 2 == 0 ? 0 : 1, -1),
                            "nw" => (start.y % 2 != 0 ? 0 : -1, -1)
                        };

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

            var flipCount = flipState.Count(pair => pair.Value);
            Console.WriteLine(flipCount);
        }
    }
}

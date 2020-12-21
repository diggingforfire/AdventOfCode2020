using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _20._01
{
    class Program
    {
        static async Task Main()
        {
            var tiles =
                (await File.ReadAllTextAsync("input.txt"))
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(line => line.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
                .Select(tileLines => new Tile(
                    int.Parse(Regex.Match(tileLines[0], "([0-9]{4})").Value),
                    tileLines.Skip(1).Select(line => line.Select(c => c).ToArray()).ToArray()))
                .ToArray();

            var cornerTiles = tiles.Where(tile1 =>
            {
                var alignedTileCount = tiles.Where(tile2 => tile1 != tile2).Count(tile2 =>
                    tile1.Sides.Any(side1 => tile2.Sides.Any(side2 => side1 == side2))
                );

                return alignedTileCount == 2;
            });

            var multipliedIds = cornerTiles.Select(tile => tile.Id).Aggregate((a, b) => a * b);

            Console.WriteLine(multipliedIds);
        }
        
        class Tile
        {
            public Tile(int id, char[][] data)
            {
                Id = id;

                var top = ToBinary(data[0]);
                var bottom = ToBinary(data[^1]);
                var right = ToBinary(data.Select(chars => chars[^1]).ToArray());
                var left = ToBinary(data.Select(chars => chars[0]).ToArray());

                var dataFlippedHorizontally = data.Select(chars => chars.Reverse().ToArray()).ToArray();
                var dataFlippedVertically = dataFlippedHorizontally.Reverse().ToArray();

                var topFlipped = ToBinary(dataFlippedVertically[0]);
                var bottomFlipped = ToBinary(dataFlippedVertically[^1]);
                var rightFlipped = ToBinary(dataFlippedVertically.Select(chars => chars[^1]).ToArray());
                var leftFlipped = ToBinary(dataFlippedVertically.Select(chars => chars[0]).ToArray());

                Sides = new[] {top, bottom, right, left, topFlipped, bottomFlipped, rightFlipped, leftFlipped};
            }

            public int[] Sides { get; }

            public long Id { get; }

            private static int ToBinary(char[] chars)
            {
                var bin = string.Join("", chars.Select(c => c == '#' ? '1' : '0'));
                return Convert.ToInt32(bin, 2);
            }
        }
    }
}

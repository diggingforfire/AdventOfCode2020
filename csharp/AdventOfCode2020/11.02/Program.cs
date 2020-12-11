using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _11._02
{
    class Seat { public char Char { get; set; } public Point Point { get; set; } }

    class Program
    {
        static readonly (int x, int y)[] _adjacents = new[] { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1) };

        static Seat GetNextSeat(int y, int x, (int x, int y) offset, Seat[][] seats)
        {
            var newY = y + offset.y;
            var newX = x + offset.x;
            if (newY < 0 || newY >= seats.Length || newX < 0 || newX >= seats[newY].Length) return null;
            var next = seats[newY][newX];
            if (next.Char != '.') return next;
            return GetNextSeat(newY, newX, offset, seats);
        }

        static async Task Main()
        {
            var points =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select((line, y) => line.Select((c, x) => new Seat {Char = c, Point = new Point(x, y)}).ToArray())
                .ToArray();

            bool changed = true;
            while (changed)
            {
                var previousSeats = points.Select(p => p.Select(b => new Seat {Char = b.Char, Point = b.Point}).ToArray()).ToArray();

                changed = false;

                Parallel.For(0, points.Length, (y, state) =>
                {
                    var row = points[y];

                    for (int x = 0; x < row.Length; x++)
                    {
                        var seat = row[x];
                        if (seat.Char == '.') continue;
                        var adjacents = _adjacents.Select(a => GetNextSeat(y, x, a, previousSeats)).ToArray();

                        seat.Char =
                            seat.Char == 'L' && !adjacents.Any(visiblePoint => visiblePoint?.Char == '#') ? '#' :
                            seat.Char == '#' && adjacents.Count(visiblePoint => visiblePoint?.Char == '#') >= 5 ? 'L' :
                            seat.Char;

                        changed |= seat.Char != previousSeats[y][x].Char;
                    }
                });
            }

            var res = points.Sum(p => p.Count(a => a.Char == '#'));
            Console.WriteLine(res);
        }
    }
}

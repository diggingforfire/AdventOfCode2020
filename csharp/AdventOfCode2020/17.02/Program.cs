using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _17._02
{
    class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }

        public bool IsActive { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            hash = hash * 23 + Z.GetHashCode();
            hash = hash * 23 + W.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{W}";
        }
    }

    class Program
    {
        static async Task Main()
        {
            var cubes =
                (await File.ReadAllLinesAsync("input.txt"))
                .SelectMany((line, y) => line.Select((c, x) => new Cube { X = x, Y = y, IsActive = c == '#' }).Where(cube => cube.IsActive))
                .ToDictionary(cube => cube.GetHashCode());

            var neighbourOffsets = new[] { -1, 0, 1 };

            var allOffsets =
                CartesianProduct(new[] { neighbourOffsets, neighbourOffsets, neighbourOffsets, neighbourOffsets })
                .Where(ints => !ints.All(i => i == 0))
                .ToArray();

            for (int i = 0; i < 6; i++)
            {
                var allNeighbourCubes = cubes.Select(pair => pair.Value).SelectMany(cube => GetNeighbours(cube, allOffsets)).ToArray();
                Array.ForEach(allNeighbourCubes, cube => cubes.TryAdd(cube.GetHashCode(), cube));

                var changes = cubes.Select(pair => pair.Value).Select(cube =>
                {
                    var neighbours = GetNeighbours(cube, allOffsets).Where(cube1 => cubes.ContainsKey(cube1.GetHashCode())).Select(cube1 => cubes[cube1.GetHashCode()]);
                    var activeNeighbourCount = neighbours.Count(neighbour => neighbour.IsActive);
                    bool becomesActive;
                    if (cube.IsActive) becomesActive = activeNeighbourCount == 2 || activeNeighbourCount == 3;
                    else becomesActive = activeNeighbourCount == 3;

                    return new
                    {
                        Cube = cube,
                        BecomesActive = becomesActive
                    };
                }).ToArray();

                foreach (var change in changes)
                {
                    change.Cube.IsActive = change.BecomesActive;
                }
            }

            var activeCount = cubes.Count(pair => pair.Value.IsActive);
            Console.WriteLine(activeCount);
        }

        static Cube[] GetNeighbours(Cube cube, IEnumerable<int>[] offsets)
        {
            return offsets.Select(offset => new Cube
            {
                X = cube.X + offset.ToArray()[0],
                Y = cube.Y + offset.ToArray()[1],
                Z = cube.Z + offset.ToArray()[2],
                W = cube.W + offset.ToArray()[3]
            }).ToArray();
        }

        static IEnumerable<IEnumerable<T>> CartesianProduct<T>
            (IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct =
                new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item }));
        }
    }
}

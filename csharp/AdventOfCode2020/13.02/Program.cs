using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _13._02
{
    
    class Program
    {
        static async Task Main()
        {
            var busIds =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select(line =>
                    line.Split(",")
                        .Select((str, index) => new {str, index}).Where(a => a.str != "x")
                        .Select(a => new{ Number = long.Parse(a.str), Index = a.index }
                       ).ToArray())
                .ToArray()[1];

            long stepSize = busIds[0].Number;
            long timestamp = busIds[0].Number;

            // https://en.wikipedia.org/wiki/Chinese_remainder_theorem#Search_by_sieving
            foreach (var busId in busIds.Skip(1))
            {
                while ((timestamp + busId.Index) % busId.Number != 0)
                {
                    timestamp += stepSize;
                }

                stepSize *= busId.Number;
            }

            Console.WriteLine(timestamp);
        }
    }
}

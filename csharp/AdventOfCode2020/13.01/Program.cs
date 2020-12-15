using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _13._01
{
    class Program
    {
        static async Task Main()
        {
            var lines = await File.ReadAllLinesAsync("input.txt");
            int timestamp = int.Parse(lines[0]);
            var busIds = lines[1].Split(",").Where(i => i != "x").Select(int.Parse).ToArray();

            var result = busIds.Select(busId => new
            {
                BusId = busId,
                NearestToTimestamp = (timestamp / busId + 1) * busId
            })
            .OrderBy(a => a.NearestToTimestamp)
            .Select(a => (a.NearestToTimestamp - timestamp) * a.BusId)
            .First();

            Console.WriteLine(result);
        }
    }
}

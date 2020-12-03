using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _02._01
{
    class Program
    {
        /// <summary>
        /// Parsing and splitting. Maybe I should start looking at regexes at one point.
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            var input =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select(line => new
                {
                    Parts = line.Split(' ')
                })
                .Select(line => new
                {
                    Min = int.Parse(line.Parts[0].Split('-')[0]),
                    Max = int.Parse(line.Parts[0].Split('-')[1]),
                    Policy = line.Parts[1][0],
                    Password = line.Parts[2]
                })
                .ToArray();

            var result = input.Select(p => new
                {
                    p.Min,
                    p.Max,
                    PolicyCount = p.Password.Count(c => c == p.Policy)
                })
                .Count(p => p.PolicyCount >= p.Min && p.PolicyCount <= p.Max);

            Console.WriteLine(result);
        }
    }
}



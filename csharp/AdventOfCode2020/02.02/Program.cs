﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _02._02
{
    class Program
    {
        /// <summary>
        /// Not much different from day one, but there was a use case for xor here. Just != would have sufficed, but isn't nearly as frightening.
        /// </summary>
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
                    FirstIndex = int.Parse(line.Parts[0].Split('-')[0]) - 1,
                    SecondIndex = int.Parse(line.Parts[0].Split('-')[1]) - 1,
                    Policy = line.Parts[1][0],
                    Password = line.Parts[2]
                })
                .ToArray();

            var result = input.Count(line =>
                line.Password[line.FirstIndex] == line.Policy ^
                (line.Password[line.SecondIndex] == line.Policy));

            Console.WriteLine(result);
        }
    }
}




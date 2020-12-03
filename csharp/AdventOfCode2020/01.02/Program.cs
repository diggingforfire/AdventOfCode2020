using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _01._02
{
    class Program
    {
        /// <summary>
        /// Like day one, but with an extra loop. O(n^3), but can't be bothered to optimize.
        /// </summary>
        static async Task Main()
        {
            var input = (await File.ReadAllLinesAsync("input.txt")).Select(int.Parse).ToArray();

            foreach (int i in input)
            {
                foreach (int j in input)
                {
                    foreach (int k in input)
                    {
                        if (i + j + k == 2020)
                        {
                            Console.WriteLine(i * j * k);
                            return;
                        }
                    }
                }
            }
        }
    }
}



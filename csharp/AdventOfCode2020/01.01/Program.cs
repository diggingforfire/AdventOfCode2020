using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _01._01
{
    class Program
    {
        static async Task Main()
        {
            var input = (await File.ReadAllLinesAsync("input.txt")).Select(int.Parse).ToArray();

            foreach (int i in input)
            {
                foreach (int j in input)
                {
                    if (i + j == 2020)
                    {
                        Console.WriteLine(i * j);
                        return;
                    }
                }
            }
        }
    }
}



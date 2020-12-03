using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _01._01
{
    class Program
    {
        /// <summary>
        /// Some loops and checks to start off with. Managed to make it O(n log n), although not really worth it with this input.
        /// </summary>
        static async Task Main()
        {
            var input = (await File.ReadAllLinesAsync("input.txt")).Select(int.Parse).ToArray();

            // From .NET 4.5 onwards this is introspective sort, which comes down to either insertion, heap or quicksort
            // https://docs.microsoft.com/en-us/dotnet/api/system.array.sort?view=net-5.0
            Array.Sort(input);

            int start = 0, end = input.Length - 1;

            for (int sum = 0; sum != 2020;)
            {
                sum = input[start] + input[end];
                if (sum > 2020) end--;
                else if (sum < 2020) start++;
            }

            int result = input[start] * input[end];
            Console.WriteLine(result);
        }
    }
}




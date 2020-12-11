using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var input =
    (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.ToCharArray().Prepend('.').Append('.').ToArray()).ToArray();

input = input
    .Prepend(new string('.', input[0].Length).ToCharArray())
    .Append(new string('.', input[0].Length).ToCharArray()).ToArray();

bool changed = true;
while (changed)
{
    changed = false;
    var previousInput = input.Select(row => row.Select(@char => @char).ToArray()).ToArray();
    
    for (int y = 1; y < input.Length - 1; y++)
    {
        var line = input[y];
        
        for (int x = 1; x < line.Length - 1; x++)
        {
            var @char = line[x];
            var adjacents = GetAdjacents(previousInput, y, x);

            input[y][x] = 
                @char == 'L' && !adjacents.Any(a => a == '#') ? '#' :
                @char == '#' && adjacents.Count(a => a == '#') >= 4 ? 'L' : 
                input[y][x];

            changed |= input[y][x] != previousInput[y][x];
        }
    }
}

var count = input.Sum(row => row.Count(a => a == '#'));
Console.WriteLine(count);

static char[] GetAdjacents(char[][] input, int x, int y)
{
    return new[]
    {
        input[x][y - 1], input[x + 1][y - 1], input[x + 1][y], input[x + 1][y + 1],
        input[x][y + 1], input[x - 1][y + 1], input[x - 1][y], input[x - 1][y - 1]
    };
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var count =
    (await File.ReadAllTextAsync("input.txt"))
    .Split($"{Environment.NewLine}{Environment.NewLine}")
    .Select(group => group.Split(Environment.NewLine))
    .Select(group => group.SelectMany(letter => letter))
    .Select(group => group.GroupBy(letter => letter).Count())
    .Sum();

Console.WriteLine(count);
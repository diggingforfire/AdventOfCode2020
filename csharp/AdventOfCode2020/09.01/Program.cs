using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

const int preambleSize = 25;

var numbers =
    (await File.ReadAllLinesAsync("input.txt"))
    .Select(long.Parse)
    .ToList();

var noSum = numbers
    .Skip(preambleSize)
    .Select((value, index) => new
    {
        Numbers = numbers.GetRange(index, preambleSize),
        PotentialSum = value
    })
    .Select(group => new
    {
        group.PotentialSum,
        ExclusiveCartesian = group.Numbers.SelectMany(
            number => group.Numbers.Where(otherNumber => otherNumber != number),
            (number, otherNumber) => number + otherNumber)
    })
    .Single(group => !group.ExclusiveCartesian.Contains(group.PotentialSum))
    .PotentialSum;

Console.WriteLine(noSum);

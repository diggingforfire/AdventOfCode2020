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

for (int i = 0; i < numbers.Count; i++)
{
    long sum = 0;
    int j = i;

    while (sum < noSum)
    {
        sum += numbers[j];
        j++;
    }

    if (sum == noSum)
    {
        var range = numbers.GetRange(i, j - i);
        long weakness = range.Min() + range.Max();
        Console.WriteLine(weakness);
        break;
    }
}
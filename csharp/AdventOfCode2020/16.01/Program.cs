using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

var parts =
    (await File.ReadAllTextAsync("input.txt"))
    .Split($"{Environment.NewLine}{Environment.NewLine}")
    .Select(line => line.Split(Environment.NewLine))
    .ToArray();

var fields = parts[0].Select(line => new
{
    Values = Regex.Matches(line, "([0-9]+)-([0-9]+)").Select(match => new
    {
        Min = int.Parse(match.Groups[1].Value),
        Max = int.Parse(match.Groups[2].Value)
    }).ToArray()
}).ToArray();

var nearbyTickets = parts[2].Skip(1).Select(line => line.Split(",").Select(int.Parse).ToArray()).ToArray();

var result = nearbyTickets.Select(ticket =>
    ticket.Where(value =>
        !fields.Any(field =>
            field.Values.Any(fieldValue =>
                value >= fieldValue.Min && value <= fieldValue.Max))))
    .Sum(ticket => ticket.Sum());

Console.WriteLine(result);
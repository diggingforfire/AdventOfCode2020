using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _16._02
{
    class Program
    {
        static async Task Main()
        {
            var parts =
                (await File.ReadAllTextAsync("input.txt"))
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(line => line.Split(Environment.NewLine))
                .ToArray();

            var fields = parts[0].Select(line => new
            {
                Name = line.Substring(0, line.IndexOf(':')),
                Values = Regex.Matches(line, "([0-9]+)-([0-9]+)").Select(match => new
                {
                    Min = int.Parse(match.Groups[1].Value),
                    Max = int.Parse(match.Groups[2].Value)
                }).ToArray()
            }).ToArray();

            var myTicket = parts[1].Skip(1).Select(line => line.Split(",").Select(long.Parse).ToArray()).Single();
            
            var nearbyTickets =
                parts[2].Skip(1).Select(line => line.Split(",").Select(int.Parse).ToArray())
                    .Where(ticket => ticket.All(value => // discard invalid tickets
                        fields.Any(field => field.Values.Any(fieldValue => value >= fieldValue.Min && value <= fieldValue.Max)))).ToArray();

            var positions = nearbyTickets
                .SelectMany(ticket => ticket)
                .Select((value, index) => new
                {
                    Position = index % nearbyTickets[0].Length,
                    Value = value
                })
                .ToLookup(value => value.Position)
                .Select(grp => new
                 {
                     grp.Key,
                     Field = fields.Where(field =>
                         grp.All(ticketValue =>
                             field.Values.Any(fieldValue => ticketValue.Value >= fieldValue.Min && ticketValue.Value <= fieldValue.Max))).ToArray()

                 }).OrderBy(position => position.Field.Length).ToArray();

            for (int i = 1; i < positions.Length; i++)
            {
                var ticket = positions[i];
                var previousTickets = positions[..i];
                positions[i] = new
                {
                    ticket.Key,
                    Field = ticket.Field.Where(field => !previousTickets.Any(position => position.Field.Any(previousField => previousField.Name == field.Name))).ToArray()
                };
            }

            var result = positions.Where(position => position.Field[0].Name.StartsWith("departure"))
                .Select(arg => myTicket[arg.Key]).Aggregate((a, b) => a * b);

            Console.WriteLine(result);
        }
    }
}

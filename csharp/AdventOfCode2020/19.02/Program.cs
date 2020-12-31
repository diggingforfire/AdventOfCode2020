using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _19._02
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

            var rules = parts[0].Select(line =>
            (
                Id: int.Parse(line.Substring(0, line.IndexOf(':'))),
                Letter: line.Contains('"') ? line[^2] : default,
                SubRules: !line.Contains('"')
                    ? line.Substring(line.IndexOf(':') + 1).Split("|").Select(rulePart =>
                            rulePart.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                        .ToArray()
                    : new int[][] { }
            )).ToDictionary(tuple => tuple.Id);

            var messages = parts[1];

            var count = messages.Count(message => GetValidMessages(message, rules[0], rules).Any(subMessage => subMessage.Length == 0));

            Console.WriteLine(count);
        }

        static string[] GetValidMessages(
            string message,
            (int Id, char Letter, int[][] SubRules) rule,
            IDictionary<int, (int Id, char Letter, int[][] SubRules)> rules)
        {
            if (rule.Letter != default(char))
            {
                if (message.Length > 0 && message[0] == rule.Letter)
                {
                    return new[] { message.Substring(1) };
                }

                return new string[] { };
            }

            List<string> list = new List<string>();

            foreach (var subRuleGroup in rule.SubRules)
            {
                var subMessages = subRuleGroup.Aggregate((IEnumerable<string>)new[] { message },
                    (options, subRule) =>
                        options.SelectMany(option => GetValidMessages(option, rules[subRule], rules)));

                list.AddRange(subMessages);
            }

            return list.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _19._01
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
                Letter: line.Contains('"') ? line[^2].ToString() : null,
                SubRules: !line.Contains('"')
                    ? line.Substring(line.IndexOf(':') + 1).Split("|").Select(rulePart =>
                            rulePart.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
                        .ToArray()
                    : new int[][] { }
            )).ToDictionary(tuple => tuple.Id);

            var messages = parts[1];

            var validMessages = GetValidMessages(rules[0], rules);

            var validCount = messages.Count(message => validMessages.Contains(message));

            Console.WriteLine(validCount);
        }

        static string[] GetValidMessages(
            (int Id, string Letter, int[][] SubRules) rule, 
            IDictionary<int, (int Id, string Letter, int[][] SubRules)> rules, int depth = 0)
        {
            depth++;

            if (rule.Letter != null)
            {
                return new[] {rule.Letter};
            }

            List<string> list = new List<string>();

            foreach (var subRuleGroup in rule.SubRules)
            {
                var message = subRuleGroup.Select(subRuleId => GetValidMessages(rules[subRuleId], rules, depth)).ToArray();
                var cartesianProduct = CartesianProduct(message).Select(c => string.Join("", c)).ToArray();
                list.AddRange(cartesianProduct);
            }
            
            return list.ToArray();
        }

        static IEnumerable<IEnumerable<T>> CartesianProduct<T>
            (IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct =
                new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item }));
        }
    }
}

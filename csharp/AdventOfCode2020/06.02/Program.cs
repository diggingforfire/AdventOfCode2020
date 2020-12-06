using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var count =
    (await File.ReadAllTextAsync("input.txt"))
    .Split($"{Environment.NewLine}{Environment.NewLine}")
    .Select(group => group.Split(Environment.NewLine))
    .Select(group => new
    {
        Answers = group,
        UniqueLetters = group
            .SelectMany(letter => letter)
            .GroupBy(letter => letter)
            .Select(letter => letter.Key)

    })
    .Sum(group =>
        group.UniqueLetters.Count(letter => group.Answers.All(answer => answer.Contains(letter))));

Console.WriteLine(count);
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var validPasswordCount =
    (await File.ReadAllTextAsync("input.txt"))
    .Split($"{Environment.NewLine}{Environment.NewLine}")
    .Select(p =>
        p.Split(' ')
            .SelectMany(r =>
                r.Split(Environment.NewLine))
            .Select(q => new {Parts = q.Split(':')})
            .Select(s => new {Name = s.Parts[0], Value = s.Parts[1]})
    )
    .Count(passport =>
        new[] {"ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt"}
        .All(field => passport.Any(passportField => passportField.Name == field)));

Console.WriteLine(validPasswordCount);

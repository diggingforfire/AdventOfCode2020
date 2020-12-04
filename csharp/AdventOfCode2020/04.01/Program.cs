using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var validPasswordCount =
    (await File.ReadAllTextAsync("input.txt"))
    .Split($"{Environment.NewLine}{Environment.NewLine}")
    .Select(lines =>
        lines.Split(' ')
            .SelectMany(line =>
                line.Split(Environment.NewLine))
            .Select(keyValue => new { Parts = keyValue.Split(':') })
            .Select(keyValue => new { Name = keyValue.Parts[0], Value = keyValue.Parts[1] })
    )
    .Count(passport =>
        new[] { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" }
            .All(field => passport.Any(passportField => passportField.Name == field)));

Console.WriteLine(validPasswordCount);

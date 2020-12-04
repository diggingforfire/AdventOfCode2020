using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        new[] 
            {
                new { Name = "ecl", IsValid = new Func<string, bool>(value => new[]{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(value))},
                new { Name = "pid", IsValid = new Func<string, bool>(value => Regex.IsMatch(value, "^[0-9]{9}$"))},
                new { Name = "eyr", IsValid = new Func<string, bool>(value => int.TryParse(value, out var val) && val >= 2020 && val <= 2030)},
                new { Name = "hcl", IsValid = new Func<string, bool>(value => Regex.IsMatch(value, "^#[0-9a-f]{6}$"))},
                new { Name = "byr", IsValid = new Func<string, bool>(value => int.TryParse(value, out var val) && val >= 1920 && val <= 2002)},
                new { Name = "iyr", IsValid = new Func<string, bool>(value => int.TryParse(value, out var val) && val >= 2010 && val <= 2020)},
                new { Name = "hgt", IsValid = new Func<string, bool>(value => 
                    value.EndsWith("cm") && int.Parse(value.Replace("cm", "")) >= 150 && int.Parse(value.Replace("cm", "")) <= 193 ||
                    value.EndsWith("in") && int.Parse(value.Replace("in", "")) >= 59 && int.Parse(value.Replace("in", "")) <= 76)},
            }
            .All(field => 
                passport.Any(passportField => 
                    passportField.Name == field.Name &&
                    field.IsValid(passportField.Value)
                    )
                )
        );

Console.WriteLine(validPasswordCount);

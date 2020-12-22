using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _21._02
{
    class Program
    {
        static async Task Main()
        {
            var foods = (await File.ReadAllLinesAsync("input.txt"))
                .Select(line => line.Split("("))
                .Select(parts =>
                {
                    return new
                    {
                        Ingredients = Regex.Matches(parts[0], "([a-z]+)").Select(match => match.Value).ToList(),
                        Allergens = Regex.Matches(parts[1].Substring(8, parts[1].Length - 9), "([a-z]+)").ToArray()
                            .Select(match => match.Value).ToList()
                    };
                }).ToArray();

            var allergenMapping = new Dictionary<string, string>();

            var allAllergens = foods.SelectMany(food => food.Allergens).ToHashSet();
            var allIngredients = foods.SelectMany(food => food.Ingredients).ToHashSet();

            while (allergenMapping.Count != allAllergens.Count)
            {
                foreach (var allergen in allAllergens.Where(a => !allergenMapping.ContainsKey(a)))
                {
                    var containingIngredients = foods.Where(food => food.Allergens.Contains(allergen)).Select(food => food.Ingredients.Where(ingredient => !allergenMapping.ContainsValue(ingredient))).ToList();
                    var intersection = IntersectMany(containingIngredients, list => list).ToList();

                    if (intersection.Count == 1)
                    {
                        allergenMapping.Add(allergen, intersection[0]);
                    }
                }
            }

            var orederedIngredients = allergenMapping.OrderBy(pair => pair.Key).Select(pair => pair.Value);
            var ingredientsList = string.Join(",", orederedIngredients);
            Console.WriteLine(ingredientsList);
        }

        public static IEnumerable<TResult> IntersectMany<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            using var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
                return new TResult[0];

            var ret = selector(enumerator.Current);

            while (enumerator.MoveNext())
            {
                ret = ret.Intersect(selector(enumerator.Current));
            }

            return ret;
        }
    }
}

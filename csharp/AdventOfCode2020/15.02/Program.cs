using System;
using System.Collections.Generic;
using System.Linq;

var startingNumbers = new [] {19, 20, 14, 0, 9, 1};

var spokenNumbersWithCount =
    new Dictionary<int, int[]>(startingNumbers.Select((number, index) =>
        new KeyValuePair<int, int[]>(number, new[] { index + 1, 0 })));

int lastSpokenNumber = startingNumbers[^1];
for (int i = startingNumbers.Length; i < 30000000; i++)
{
    lastSpokenNumber =
        spokenNumbersWithCount[lastSpokenNumber][1] == 0 ? 
            0 :
            spokenNumbersWithCount[lastSpokenNumber][0] - spokenNumbersWithCount[lastSpokenNumber][1];

    if (spokenNumbersWithCount.ContainsKey(lastSpokenNumber))
    {
        spokenNumbersWithCount[lastSpokenNumber][1] = spokenNumbersWithCount[lastSpokenNumber][0];
        spokenNumbersWithCount[lastSpokenNumber][0] = i + 1;
    }
    else
    {
        spokenNumbersWithCount.Add(lastSpokenNumber, new[] { i + 1, 0 });
    }
}

Console.WriteLine(lastSpokenNumber);
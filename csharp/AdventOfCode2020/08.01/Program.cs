using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var instructions =
    (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Split(' '))
    .Select(parts => new { Operation = parts[0], Argument = int.Parse(parts[1]) })
    .ToArray();

int programCounter = 0;
int accumulator = 0;
var programCounters = new HashSet<int>();

while (!programCounters.Contains(programCounter))
{ 
    programCounters.Add(programCounter);
    var instruction = instructions[programCounter];
    
    switch (instruction.Operation)
    {
        case "nop":
            programCounter++;
            break;
        case "acc":
            programCounter++;
            accumulator += instruction.Argument;
            break;
        case "jmp":
            programCounter += instruction.Argument;
            break;
    }
}

Console.WriteLine(accumulator);
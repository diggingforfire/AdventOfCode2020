using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

var instructions =
    (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Split(' '))
    .Select((parts, index) => (Operation: parts[0], Argument: int.Parse(parts[1]), Index: index))
    .ToArray();

foreach (var instruction in instructions.Where(instruction => instruction.Operation != "acc"))
{
    var modifiedInstructions = instructions.Select((modifiedInstruction, index) =>
        index == instruction.Index ? (instruction.Operation switch {"nop" => "jmp", "jmp" => "nop"}, instruction.Argument, instruction.Index) : modifiedInstruction).ToArray();

    var result = Solve(modifiedInstructions);
    if (result.Completed)
    {
        Console.WriteLine(result.Accumulator);
        break;
    }
}

static (int Accumulator, int ProgramCounter, bool Completed) Solve(
    (string Operation, int Argument, int Index)[] instructions)
{
    int programCounter = 0;
    int accumulator = 0;
    var programCounters = new HashSet<int>();

    while (!programCounters.Contains(programCounter) && programCounter <= instructions.Length - 1)
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

    return (accumulator, programCounter, programCounter == instructions.Length);
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18._02
{
    class Program
    {
        static async Task Main()
        {
            var result =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select(infix => EvaluatePostfix(ToPostfix(infix)))
                .Sum();

            Console.WriteLine(result);
        }

        static long EvaluatePostfix(string postfix)
        {
            Stack<long> operands = new Stack<long>();

            foreach (var token in postfix)
            {
                if (int.TryParse(token.ToString(), out var op))
                {
                    operands.Push(op);
                }
                else
                {
                    var op1 = operands.Pop();
                    var op2 = operands.Pop();

                    switch (token)
                    {
                        case '*':
                            operands.Push(op1 * op2);
                            break;
                        case '+':
                            operands.Push(op1 + op2);
                            break;
                    }
                }
            }

            return operands.Single();
        }

        static string ToPostfix(string infix)
        {
            var output = new StringBuilder();
            var operators = new Stack<string>();

            var precedence = new Dictionary<string, int>
            {
                {"+", 2},
                {"*", 1},
            };

            var tokens = infix.Replace(" ", "").Select(c => c.ToString()).ToArray();

            // http://mathcenter.oxford.emory.edu/site/cs171/shuntingYardAlgorithm/
            foreach (var token in tokens)
            {
                if (int.TryParse(token, out _))
                {
                    output.Append(token);
                }
                else
                {
                    string op = token;
                    switch (token)
                    {
                        case "(":
                            operators.Push(token);
                            break;
                        case ")":
                            while (operators.Count > 0 && (op = operators.Pop()) != "(")
                            {
                                output.Append(op);
                            }
                            break;
                        default:
                            if (operators.Count == 0 || operators.Peek() == "(")
                            {
                                operators.Push(token);
                            }
                            else if (precedence[token] > precedence[operators.Peek()])
                            {
                                operators.Push(token);
                            }
                            else
                            {
                                while (operators.Count > 0 && operators.Peek() != "(" && precedence[op] <= precedence[operators.Peek()])
                                {
                                    op = operators.Pop();
                                    output.Append(op);
                                }

                                operators.Push(token);
                            }

                            break;
                    }
                }
            }

            while (operators.Count > 0)
            {
                output.Append(operators.Pop());
            }

            return output.ToString();
        }
    }
}

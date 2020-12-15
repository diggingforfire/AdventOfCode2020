using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _13._02
{
    class Holder
    {
        public NumberWithIndex One { get; set; }
        public NumberWithIndex Two { get; set; }
        public long Multiple { get; set; }
    }

    class NumberWithIndex
    {
        public int Number { get; set; }
        public int Index { get; set; }
    }

    class Program
    {
        static async Task Main()
        {
            var busIds =
                (await File.ReadAllLinesAsync("input.txt"))
                .Select(line =>
                    line.Split(",")
                        .Select((str, index) => new {str, index}).Where(a => a.str != "x")
                        .Select(a => new{ Number = int.Parse(a.str), Index = a.index }
                       ).ToArray())
                .ToArray()[1];


            var holders = new List<Holder>();

            foreach (var id in busIds)
            {
                foreach (var otherId in busIds)
                {
                    if (id.Number == otherId.Number) continue;
                    if ( (id.Index - otherId.Index) % id.Number == 0)
                    {
                        holders.Add(new Holder
                        {
                            Multiple = id.Number * otherId.Number,
                            One = new NumberWithIndex {Number = id.Number, Index = id.Index},
                            Two = new NumberWithIndex {Number = otherId.Number, Index = otherId.Index}
                        });
                      

                    }
                }
            }

            Parallel.ForEach(holders, (holder) =>
            {
                for (long i = 1; i < long.MaxValue; i++)
                {
                    if (i % holder.Multiple == 0)
                    {
                        var count = busIds.Count(id =>
                        {
                            int altIndex = holder.Two.Index - id.Index;
                            bool testval = (i - altIndex) % id.Number == 0;
                            return testval;
                        });

                        if (count == busIds.Length)
                        {

                        }
                        //if (test)
                        //{
                        //    Console.WriteLine($"yas");
                        //}
                    }
                }
            });




            var sw = Stopwatch.StartNew();

            Parallel.For(1, long.MaxValue, (i) =>
            {
                foreach (var holder in holders)
                {
                    if (i % holder.Multiple == 0)
                    {
                        bool test = busIds.All(id =>
                        {
                            int altIndex = holder.One.Index - id.Index;
                            bool testval = (i - altIndex) % id.Number == 0;
                            return testval;
                        });



                        if (test)
                        {
                            Console.WriteLine($"yas");
                        }
                    }

                    if (i % 10000000 == 0)
                    {
                        Console.WriteLine($"{(i / (double)long.MaxValue) * 100}");
                    }
                }
            });
            //for (long i = 1; i < long.MaxValue; i++)
            //{
            //    foreach (var holder in holders)
            //    {
            //        if (i % holder.Multiple == 0)
            //        {
            //            bool test = busIds.All(id =>
            //            {
            //                int altIndex = holder.One.Index - id.Index;
            //                bool testval = (i - altIndex) % id.Number == 0;
            //                return testval;
            //            });

                       

            //            if (test)
            //            {
            //                Console.WriteLine($"yas");
            //            }
            //        }

            //        if (i % 10000000 == 0)
            //        {
            //            Console.WriteLine($"{(i / long.MaxValue) * 100}");
            //        }
            //    }
                //var test = holders.Any(holder =>
                //{

                //    return true;
                //});
                //if (multiples.All(mul => mul % multiple == 0))
                //{

                //}
                //bool test = busIds.All(id =>
                //{
                //    int altIndex = index - id.index;
                //    bool testval = (i - altIndex) % id.number == 0;
                //    return testval;
                //});

                //if (i % 10000000 == 0)
                //{
                //    Console.WriteLine($"{(i/ long.MaxValue)*100}");
                //}
                //if (test)
                //{
                //    Console.WriteLine(i - index);
                //    sw.Stop();
                //    Console.WriteLine(sw.ElapsedMilliseconds);
                //    break;
                //}
            }
    }
}

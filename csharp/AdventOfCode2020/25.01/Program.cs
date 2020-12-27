using System;

namespace _25._01
{
    class Program
    {
        static void Main()
        {
            var publicKeys = new[] { 1327981, 2822615 };

            long value = 1;
            long subject = 7;
            int loopSize;
            for (loopSize = 1; loopSize < int.MaxValue; loopSize++)
            {
                value = (value * subject) % 20201227;
                if (value == publicKeys[0] || value == publicKeys[1])
                {
                    break;
                }
            }

            subject = value == publicKeys[0] ? publicKeys[1] : publicKeys[0];
            value = 1;

            for (int i = 0; i < loopSize; i++)
            {
                value = (value * subject) % 20201227;
            }

            Console.WriteLine(value);
        }

    }
}

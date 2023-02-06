using System;
using System.Diagnostics;

namespace cs_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Hello/there/a there";

            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 10000000; i++)
            {
                str.Contains("Hello/there");
            }
            s.Stop();
            Console.WriteLine("{0}ms using Contains", s.Elapsed.TotalMilliseconds);

            s.Reset();
            s.Start();
            for (int i = 0; i < 10000000; i++)
            {
                str.StartsWith("Hello/there");
            }
            s.Stop();
            Console.WriteLine("{0}ms using StartsWith", s.Elapsed.TotalMilliseconds);
        }
    }
}

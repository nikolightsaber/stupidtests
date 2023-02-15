using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace cs_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            (int, int) a = (1 ,1);
            (int, int) b = (1 ,2);
            Console.WriteLine("{0}", a < b);
        }
    }
}

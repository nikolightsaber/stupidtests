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
            List<object> list1 = new List<object>() { 1, 2 };
            List<object> list2 = new List<object>();
            Console.WriteLine("first {0}", list1.First());
            Console.WriteLine("first {0} isnull {1}", list2.FirstOrDefault(), list2.FirstOrDefault() == null);
            Console.WriteLine("first {0}", list2.First());
        }
    }
}

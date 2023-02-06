using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace cs_tests
{
    class Program
    {
        static List<uint> _testList = null;
        static List<uint> TestList
        {
            get
            {
                Console.WriteLine("Getting list");
                return _testList;
            }
            set
            {
                Console.WriteLine("Setting list");
                _testList = value;
            }
        }
        static void Main(string[] args)
        {
            TestList = new List<uint>() { 0, 1, 2, 3, 4, 5, 6 };
            TestList.ForEach(i => Console.Write("{0}, ", i));
            Console.WriteLine();
            TestList.RemoveAll(i => i < 3);
            TestList.ForEach(i => Console.Write("{0}, ", i));
            Console.WriteLine();
        }
    }
}

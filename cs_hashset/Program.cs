using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        delegate void TestDelegate();
        static HashSet<TestDelegate> hash2 = new HashSet<TestDelegate>();

        static void adddelegate2(string hallo)
        {
            switch(hallo)
            {
                case "a":
                case "b":
                    hash2.Add(() => { Console.WriteLine("test2"); });
                    break;
                default:
                    break;
            }
        }

        static void Main(string[] args)
        {
            HashSet<uint> hash = new HashSet<uint>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(3);
            hash.Add(4);
            hash.Add(5);
            hash.Add(1);
            hash.Add(2);
            hash.Add(3);
            hash.Add(4);
            hash.Add(5);
            hash.Add(6);
            hash.Remove(5);
            foreach (var i in hash)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Length: " + hash.Count);
            hash.Clear();
            Console.WriteLine("Length: " + hash.Count);

            hash2.Add(() => { Console.WriteLine("Test1"); });
            hash2.Add(() => { Console.WriteLine("Test1"); });
            adddelegate2("a");
            adddelegate2("b");

            foreach (var fn in hash2)
            {
                fn();
            }

            hash2.ElementAt(2)();
            hash2.RemoveAt(2)();
            Console.WriteLine("");
            hash2.ElementAt(2)();
        }
    }
}

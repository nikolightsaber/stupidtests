using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, uint> dict = new Dictionary<string, uint>();
            dict.TryAdd("1", 1);
            dict.TryAdd("2", 2);
            dict.TryAdd("3", 3);
            dict.TryAdd("4", 4);
            dict.TryAdd("5", 5);
            dict.TryAdd("1", 1);
            dict.TryAdd("2", 2);
            dict.TryAdd("3", 3);
            dict.TryAdd("4", 4);
            dict.TryAdd("5", 5);
            dict.TryAdd("6", 6);
            dict.Remove("5");
            foreach (var i in dict)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(dict.ElementAt(0));
            Console.WriteLine(dict.Remove("1"));
            Console.WriteLine(dict.ElementAt(0));
        }
    }
}

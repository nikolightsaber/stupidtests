using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace cs_tests
{
    class Program
    {
        public class Top
        {
            public string Name { get; private set; } = "";

            public Top(string name)
            {
                Name = name;
            }

        }

        public class BotLeft: Top
        {
            public bool IsGood = false;
            public BotLeft(bool isgood, string name): base(name)
            {
                IsGood = isgood;
            }
        }

        public class BotRight: Top
        {
            public BotRight(string name): base(name) {}
        }

        static void Main(string[] args)
        {
            List<Top> list = new List<Top>();
            list.Add(new Top("left"));
            list.Add(null);
            Top correct = new BotLeft(true, "left");
            list.Add(correct);
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotRight("left"));

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < 100000; i++)
            {
                foreach (Top t in list)
                {
                    if ((t as BotLeft)?.IsGood ?? false)
                    {
                        if (t != correct)
                        {
                            throw new Exception("TOZ");
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                foreach (Top t in list)
                {
                    if ((t != null) && (t is BotLeft) && (t as BotLeft).IsGood)
                    {
                        if (t != correct)
                        {
                            throw new Exception("TOZ");
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}

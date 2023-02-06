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
            public uint Id = 0;
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
            (correct as BotLeft).Id = uint.MaxValue;
            list.Add(correct);
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotRight("right"));
            list.Add(new BotRight("right"));
            list.Add(new BotRight("right"));
            list.Add(new BotRight("right"));
            list.Add(new BotRight("right"));
            list.Add(new BotRight("right"));
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);

            var watch = new System.Diagnostics.Stopwatch();
            int count = 0;

            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                foreach (Top t in list)
                {
                    if (((t as BotLeft)?.Id ?? uint.MaxValue) == uint.MaxValue)
                    {
                        if (t is BotLeft && t != correct)
                        {
                            throw new Exception("TOZ");
                        }
                        count++;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, {count}");

            count = 0;
            watch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                foreach (Top t in list)
                {
                    if ((t is BotRight) || (((t as BotLeft)?.Id ?? uint.MaxValue) == uint.MaxValue))
                    {
                        if (t is BotLeft && t != correct)
                        {
                            throw new Exception("TOZ");
                        }
                        count++;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, {count}");

            count = 0;
            watch.Restart();
            for (int i = 0; i < 100000; i++)
            {
                foreach (Top t in list)
                {
                    if (!(t is BotLeft botleft) || botleft.Id == uint.MaxValue)
                    {
                        if (t is BotLeft && t != correct)
                        {
                            throw new Exception("TOZ");
                        }
                        count++;
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms, {count}");
        }
    }
}

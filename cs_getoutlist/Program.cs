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
            public string Name { get; set; } = "";

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
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotLeft(false, "left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(new BotRight("left"));
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);
            list.Add(null);

            Top first = list.FirstOrDefault();
            first.Name = "helloooooooooo";

            Console.WriteLine(list[0].Name);
        }
    }
}

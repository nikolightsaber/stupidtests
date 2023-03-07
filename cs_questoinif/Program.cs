
namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        class Test
        {
            public bool Enabled = false;
        }
        static void Main(string[] args)
        {
            Test toz = null;
            Test toz2 = new Test();

            if (!toz?.Enabled ?? false)
                Console.WriteLine("works1");

            if (!toz2?.Enabled ?? false)
                Console.WriteLine("2works1");

            if (!(toz?.Enabled ?? true))
                Console.WriteLine("works2");

            if (!(toz2?.Enabled ?? true))
                Console.WriteLine("2works2");

        }
    }
}

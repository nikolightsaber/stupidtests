using System;

namespace cs_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            int clearfaults_count = 0;
            while(clearfaults_count < 1000000)
            {
                clearfaults_count++;
                if (clearfaults_count % 10 == 0)
                {
                    Console.WriteLine($"Latched: {clearfaults_count}");
                }
            }
        }
    }
}

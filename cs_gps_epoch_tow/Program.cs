using System;

namespace cs_tests
{
    class Program
    {
        static public Int32 strtol(string s)
        {
            if (s == "")
                return 0;
            else if (s.IndexOf("0x", StringComparison.Ordinal) == 0)
                return Int32.Parse(s.Substring(2), System.Globalization.NumberStyles.HexNumber);
            else
                return Int32.Parse(s);
        }

        static void Main(string[] args)
        {
            string payload = "abc, 3027040";
            DateTime temp = DateTime.Today;
            int diff = ((int)temp.DayOfWeek);
            temp = temp.AddDays(-1 * diff);
            temp = temp.AddMilliseconds(strtol(payload.Split(',')[1]));
            Console.WriteLine(temp.ToString());
        }
    }
}

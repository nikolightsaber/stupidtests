using System;
using System.Text;
using System.Linq;

namespace cs_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string m = "qbc3/1.3/1.2/3/47/1000/22/25";
            string[] mess = m.Split('/');
            float[] f_params;
            int[] i_params;
            uint[] ui_params;

            if(TryGetValuesFromStringArray(mess, 3, 2, out f_params, out i_params))
            {
                Console.Write("Floats: ");
                foreach(float a in f_params)
                    Console.Write(a + " ");
                Console.Write("Ints: ");
                foreach(int a in i_params)
                    Console.Write(a + " ");
            }
            if(TryGetUIntsFromStringArray(mess, 2, out ui_params))
            {
                Console.Write("UInts: ");
                foreach(float a in f_params)
                    Console.Write(a + " ");
            }
            Console.WriteLine();

            double? lat = double.NaN;
            StringBuilder str = new StringBuilder();
            str.AppendFormat("\"Lat\":{0:f6}", lat ?? 0.0);
            Console.WriteLine(str.ToString());
        }

        private static bool TryGetValuesFromStringArray(string[] message, uint f_len, uint i_len, out float[] f_out, out int[] i_out)
        {
            f_out = new float[f_len];
            i_out = new int[i_len];
            if(message.Length < f_len + i_len + 1)
                return false;

            for(uint i = 1; i < f_len + 1; i++)
            {
                if(!float.TryParse(message[i], out f_out[i - 1]))
                    return false;
                Console.WriteLine("f " + i);
            }

            for(uint i = f_len + 1; i < f_len + i_len + 1; i++)
            {
                if(!int.TryParse(message[i], out i_out[i - f_len - 1]))
                    return false;
                Console.WriteLine("i " + i);
            }

            return true;
        }

        private static bool TryGetUIntsFromStringArray(string[] message, uint ui_len, out uint[] ui_out)
        {
            ui_out = new uint[ui_len];

            if(message.Length < ui_len + 1)
                return false;

            for(uint i = 1; i < ui_len + 1; i++)
            {
                if(!uint.TryParse(message[i], out ui_out[i - 1]))
                    return false;
            }
            return true;
        }
    }
}

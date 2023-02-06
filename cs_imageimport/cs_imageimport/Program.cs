using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_imageimport
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bitmap = new Bitmap(@"C:\maze.png");
            Color clr = bitmap.GetPixel(0, 0);
            Console.WriteLine(bitmap);
        }
    }
}

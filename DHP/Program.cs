using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DHP dhp = new DHP();
                dhp.PrintMatrix();
                dhp.PrintOriginalPicture();
                dhp.Encode(4, 4);
                dhp.Decode();
                dhp.PrintDecodePicture();
            }
            catch { }
            Console.ReadKey();
        }
    }
}

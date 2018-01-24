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
            DHP dhp = new DHP();
            dhp.PrintMatrix();
            dhp.Encode(5, 6);
            dhp.Decode();
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSequence
{
    class Program
    {
        static void Main(string[] args)
        {
            BitSequence bs = new BitSequence();

            Console.WriteLine(bs);
            for (int i = 0; i < 8; i++)
            {
                bs.AddBits(11, 5);
                Console.WriteLine(bs);
            }

            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSequence
{
    class BitSequence
    {
        private int[] bits = new int[10];
        private byte currentInt = 0;
        private int currentBit = 0;

        private string IntToBit(byte pos)
        {
            return Convert.ToString(bits[pos], 2).PadLeft(32, '0');
        }

        public void Reset()
        {
            bits[0] = 0;
            currentBit = 0;
            currentInt = 0;
        }
        
        public void AddBits(int bitContainer, byte bitsToAdd)
        {
            if (currentBit == 0)
                bits[currentInt] = bitContainer;
            else
                bits[currentInt] |= bitContainer << currentBit;

            currentBit += bitsToAdd;

            if (currentBit >= 32)
            {
                currentInt++;
                currentBit -= 32;
                if (currentBit > 0)
                    bits[currentInt] = bitContainer >> (bitsToAdd - currentBit);
            }

        }

        public override string ToString()
        {
            string[] ints = new string[currentInt + 1];

            for (byte i = 0; i < currentInt; i++)
                ints[i] = IntToBit(i);
            if (currentBit > 0)
                ints[currentInt] = Convert.ToString(bits[currentInt], 2).PadLeft(currentBit, '0');

            Array.Reverse(ints);

            return '[' + String.Join(" ", ints) + ']';
        }
    }
}

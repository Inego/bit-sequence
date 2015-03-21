using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSequence
{
    class BitSequence
    {
        private List<int> bits = new List<int>(1);
        private int currentBit = 0;

        private string IntToBit(byte pos)
        {
            return Convert.ToString(bits[pos], 2).PadLeft(32, '0');
        }

        public void AddBits(int bitContainer, byte bitsToAdd)
        {
            if (currentBit == 0)
                bits.Add(bitContainer);
            else
                bits[bits.Count -1] |= bitContainer << currentBit;

            currentBit += bitsToAdd;

            if (currentBit >= 32)
            {
                currentBit -= 32;
                if (currentBit > 0)
                    bits.Add(bitContainer >> (bitsToAdd - currentBit));
            }

        }

        public override string ToString()
        {
            string[] ints = new string[bits.Count];

            for (byte i = 0; i < bits.Count - 1; i++)
                ints[i] = IntToBit(i);
            if (currentBit > 0)
                ints[bits.Count - 1] = Convert.ToString(bits[bits.Count - 1], 2).PadLeft(currentBit, '0');

            Array.Reverse(ints);

            return '[' + String.Join(" ", ints) + ']';
        }
    }
}

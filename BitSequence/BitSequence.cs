using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitSequence
{
    class BitSequence : IEquatable<BitSequence>, IComparable<BitSequence>
    {
        private static uint[] startprimes = new uint[32] { 1, 0x7FFFFFFF, 0x3FFFFFDD, 0x1FFFFFFD, 0xFFFFFC7, 0x7FFFFD9, 0x3FFFFFB, 0x1FFFFD9, 0xFFFFFD, 0x7FFFF1, 0x3FFFFD, 0x1FFFF7, 0xFFFFD, 0x7FFFF, 0x3FFFB, 0x1FFFF, 0xFFF1, 0x7FED, 0x3FFD, 0x1FFF, 0xFFD, 0x7F7, 0x3FD, 0x1FD, 0xFB, 0x7F, 0x3D, 0x1F, 0xD, 0x7, 0x3, 0x2 };


        public List<uint> bits = new List<uint>(1);
        private int currentBit = 0;


        public BitSequence()
        {
        }


        public BitSequence(uint container, byte bits)
        {
            this.bits.Add(container);
            currentBit = bits;
        }

        public static byte GetBitNumber(int choices)
        {
            if (choices < 3)
                return 1;
            if (choices < 5)
                return 2;
            if (choices < 9)
                return 3;
            if (choices < 17)
                return 4;
            return 5; // :)
        }

        private string IntToBit(byte pos)
        {
            return Convert.ToString(bits[pos], 2).PadLeft(32, '0');
        }

        public bool Equals(BitSequence other) {
            return bits.SequenceEqual(other.bits);
        }

        public int CompareTo(BitSequence y)
        {
            int result = this.currentBit.CompareTo(y.currentBit);

            if (result != 0)
                return result;

            int blocksCount = this.bits.Count;

            result = blocksCount.CompareTo(y.bits.Count);

            if (result != 0)
                return result;

            for (int i = 0; i < blocksCount; i++)
            {
                result = this.bits[i].CompareTo(y.bits[i]);
                if (result != 0)
                    return result;
            }

            return 0;
        }



        public override bool Equals(Object obj)
        {
            BitSequence other = obj as BitSequence;
            return bits.SequenceEqual(other.bits) && currentBit == other.currentBit;
        }

        public override int GetHashCode()
        {
            uint result;
            int c;

            if (bits.Count == 0)
                return 0;
            
            result = startprimes[currentBit] * (bits[bits.Count - 1] + 1);
            
            c = bits.Count - 2;

            while (c >= 0)
            {
                unchecked
                { 
                    result += bits[c]; 
                }
                c--;
            }

            return unchecked((int)result);
            
        }

        public void AddBits(uint bitContainer, byte bitsToAdd)
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

            return '[' + String.Join(" ", ints) + "] " + GetHashCode().ToString("X8");
        }
    }
}

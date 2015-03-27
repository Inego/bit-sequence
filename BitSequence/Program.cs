using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BitSequence
{
    class BitPattern
    {
        private byte[][] patternBitsRequired;
        private int numberOfSamples;
        private uint[][] samples;


        public BitPattern(int numberOfSamples)
        {
            this.numberOfSamples = numberOfSamples;
            
            samples = new uint[numberOfSamples][];
            patternBitsRequired = new byte[numberOfSamples][];

            fill();
        }

        private void fill()
        {
            Random r = new Random();

            for (int i = 0; i < numberOfSamples; i++)
            {
                int depth = 2 + r.Next(16);
                samples[i] = new uint[depth];
                patternBitsRequired[i] = new byte[depth];
                for (int j = 0; j < depth; j++)
                {
                    int choice = 2 + r.Next(12);
                    samples[i][j] = (uint)r.Next(choice);
                    patternBitsRequired[i][j] = BitSequence.GetBitNumber(choice);
                }
            }
        }

        public BitSequence[] generateBitSequences()
        {
            BitSequence[] result = new BitSequence[numberOfSamples];

            for (int i = 0; i < numberOfSamples; i++)
            {
                BitSequence b = new BitSequence();

                for (int j = 0; j < samples[i].Length; j++)
                {
                    b.AddBits(samples[i][j], patternBitsRequired[i][j]);
                }
                
                result[i] = b;
            }

            return result;
        }




    }


    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(new BitSequence(2, 31));
            //Console.WriteLine(new BitSequence(1, 30));
            //Console.WriteLine(new BitSequence(5, 0));
            //Console.ReadKey();
            //return;

            int samplesCount = 1000;

            

            Stopwatch s = Stopwatch.StartNew();

            BitPattern p = new BitPattern(samplesCount);

            s.Stop();

            Console.WriteLine("Finished populating bytes. " + s.ElapsedMilliseconds);

            s.Start();

            Console.WriteLine("Started populating bit sequences - " + GC.GetTotalMemory(false).ToString());

            BitSequence[] bs = p.generateBitSequences();

            s.Stop();

            Console.WriteLine("Finished populating bit sequences - " + s.ElapsedMilliseconds + " ms " + GC.GetTotalMemory(false).ToString());



            //// Test collisions

            //Console.WriteLine("Starting collision test");

            //Dictionary<int, BitSequence> hashDict = new Dictionary<int, BitSequence>(samplesCount);
            //HashSet<BitSequence> fullSet = new HashSet<BitSequence>();

            //int hashCollisions = 0;
            //int fullCollisions = 0;

            //foreach (BitSequence b in bs)
            //{
            //    int hash = b.GetHashCode();
            //    bool hashContained = hashDict.ContainsKey(hash);
            //    if (!hashContained)
            //        hashDict[hash] = b;

            //    if (fullSet.Contains(b))
            //        fullCollisions++;
            //    else
            //    {
            //        if (hashContained)
            //            hashCollisions++;
            //        fullSet.Add(b);
            //    }
            //}

            //Console.WriteLine("  Hash collisions: " + hashCollisions);
            //Console.WriteLine("  Full collisions: " + fullCollisions);

            //Console.WriteLine("Finished collision test");


            int found;
            bool contained;
            BitSequence b;





            // SORTED DICTIONARY
            SortedDictionary<BitSequence, int> sdic = new SortedDictionary<BitSequence, int>();

            Console.WriteLine("Started sorted dictionary performance test - " + GC.GetTotalMemory(true).ToString());

            s.Start();


            for (int i = 0; i < samplesCount; i++)
            {
                b = bs[i];
                contained = sdic.TryGetValue(b, out found);

                if (!contained)
                    sdic[b] = i;

                if ((i % 3) == 0)
                    contained = sdic.TryGetValue(b, out found);
            }

            s.Stop();

            Console.WriteLine("Finished sorted dictionary performance test - " + s.ElapsedMilliseconds + " ms; " + GC.GetTotalMemory(false).ToString());



            //// Dictionary performance test, with 1/3 returning cases

            //Dictionary<BitSequence, int> dic = new Dictionary<BitSequence, int>();

            //Console.WriteLine("Started dictionary performance test - " + GC.GetTotalMemory(true).ToString());

            //s.Start();


            //for (int i = 0; i < samplesCount; i++)
            //{
            //    b = bs[i];
            //    contained = dic.TryGetValue(b, out found);

            //    if (!contained)
            //        dic[b] = i;

            //    if ((i % 3) == 0)
            //        contained = dic.TryGetValue(b, out found);
            //}

            //s.Stop();

            //Console.WriteLine("Finished dictionary performance test - " + s.ElapsedMilliseconds + " ms; " + GC.GetTotalMemory(false).ToString());

            


            

            
            
            
            Console.ReadKey();
            

        }
    }
}

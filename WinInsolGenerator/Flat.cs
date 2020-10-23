using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinInsolGenerator
{
    public class Flat
    {
        public string Id { get; set; }
        public List<int> Indexes { get; set; }

        /// <summary>
        /// CL, CR, MD, MU, llu
        /// </summary>
        public string FType { get; set; }
        public int TopWindows { get; set; }
        public int BottomWindows { get; set; }
        public List<string> WindowCombinations { get; private set; }
        public List<BitArray> WindowCombinationsBool { get; private set; }


        //private fields
        private List<List<int>> flatWinCombs;
        private Stack<int> comb;
        private int numberOfCombinations;
        private BitArray tempBitArray;


        public Flat() { }
        public Flat(string code)
        {
            if (string.IsNullOrEmpty(code)) return;
            Id = code;
            Indexes = new List<int>();

            if (Id == "llu")
            {
                FType = "llu";
                return;
            }

            var vals = code.Split('_');
            if (code.Length < 3) return;

            FType = vals[0];
            TopWindows = int.Parse(vals[1]);
            BottomWindows = int.Parse(vals[2]);
        }

        public void SetWindowCombinations()
        {
            WindowCombinations = new List<string>();
            if (FType == "llu") return;

            var rooms = Indexes.Count - 1;
            if (rooms < 1) rooms = 1; //для студий

            numberOfCombinations = rooms - 2; //количетво лишних комнат в 4+ квартирах

            if (numberOfCombinations < 2)
            {
                WindowCombinations = Indexes.Select(i => i.ToString()).ToList();
                return;
            }
            else
            {
                comb = new Stack<int>();
                flatWinCombs = new List<List<int>>();

                //запуск перебора
                AddWindow(0);

                if (flatWinCombs.Any())
                {
                    WindowCombinations = flatWinCombs.Select(c => string.Join(',', c)).ToList();
                }
            }
        }
        /*
        private void AddWindow(int currentIndex)
        {
            if (currentIndex == TopWindows + BottomWindows)
                return;

            for (int i = currentIndex; i < (TopWindows + BottomWindows); i++)
            {
                comb.Push(Indexes[i]);
                if (comb.Count == numberOfCombinations)
                {
                    flatWinCombs.Add(comb.Reverse().ToList());
                }
                else
                {
                    AddWindow(i + 1);
                }

                comb.Pop();
            }
        }
        */

        public void SetWindowCombinationsBool()
        {
            WindowCombinationsBool = new List<BitArray>();
            var totalWins = BottomWindows + TopWindows;
            var bitArray = new BitArray(totalWins);

            if (FType == "llu")
            {                
                for (int i = 0; i < totalWins; i++)
                {
                    bitArray[i] = false;
                }
                WindowCombinationsBool.Add(bitArray);
                return;
            }

            var rooms = totalWins - 1;
            if (rooms < 1) rooms = 1; //для студий

            numberOfCombinations = rooms - 2; //количетво лишних комнат в 4+ квартирах

            if (numberOfCombinations < 2)
            {                
                for (int i = 0; i < totalWins; i++)
                {
                    bitArray = new BitArray(totalWins);
                    bitArray[i] = true;
                    WindowCombinationsBool.Add(bitArray);
                }
                return;
            }
            else
            {
                //нужно сделать перебор
            }
        }

        private void AddWindowBit(int currentIndex)
        {
            if (currentIndex == TopWindows + BottomWindows)
                return;

            for (int i = currentIndex; i < (TopWindows + BottomWindows); i++)
            {
                tempBitArray[i] = true;
                if (BitSum(tempBitArray) == numberOfCombinations)
                {
                    WindowCombinationsBool.Add(tempBitArray.co);
                    tempBitArray.c
                }
                else
                {
                    AddWindowBit(i + 1);
                }

                comb.Pop();
            }
        }

        private int BitSum(BitArray bArray)
        {
            int sum = 0;
            for (int i = 0; i < bArray.Length; i++)
            {
                if (bArray[i]) sum++;
            }
            return sum;
        }

        //какое-то говно со Stack Overflow
        //Сумма битов
        public static Int32 GetCardinality(BitArray bitArray)
        {

            Int32[] ints = new Int32[(bitArray.Count >> 5) + 1];

            bitArray.CopyTo(ints, 0);

            Int32 count = 0;

            // fix for not truncated bits in last integer that may have been set to true with SetAll()
            ints[ints.Length - 1] &= ~(-1 << (bitArray.Count % 32));

            for (Int32 i = 0; i < ints.Length; i++)
            {

                Int32 c = ints[i];

                // magic (http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel)
                unchecked
                {
                    c = c - ((c >> 1) & 0x55555555);
                    c = (c & 0x33333333) + ((c >> 2) & 0x33333333);
                    c = ((c + (c >> 4) & 0xF0F0F0F) * 0x1010101) >> 24;
                }

                count += c;

            }

            return count;

        }
    }
}

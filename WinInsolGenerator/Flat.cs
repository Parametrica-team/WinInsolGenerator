using System;
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


        //private fields
        private List<List<int>> flatWinCombs;
        private Stack<int> comb;
        private int numberOfCombinations;


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

        private void AddWindow(int currentIndex)
        {
            if (currentIndex == Indexes.Count)
                return;

            for (int i = currentIndex; i < Indexes.Count; i++)
            {
                comb.Push(Indexes[i]);
                if (comb.Count == numberOfCombinations)
                {
                    flatWinCombs.Add(comb.Reverse().ToList());
                }
                else
                {
                    AddWindow(i+1);
                }

                comb.Pop();
            }
        }
    }

}

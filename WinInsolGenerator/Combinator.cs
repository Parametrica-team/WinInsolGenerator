using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinInsolGenerator
{    
    class Combinator<T>
    {
        public List<List<T>> Combinations { get; private set; }

        private List<List<T>> allItems;
        private Stack<T> comb;

        public Combinator(List<List<T>> items)
        {
            Combinations = new List<List<T>>();
            comb = new Stack<T>();
            allItems = items;

            AddItem(0);
        }

        private void AddItem(int index)
        {
            foreach (var item in allItems[index])
            {
                comb.Push(item);

                if (comb.Count == allItems.Count)
                {
                    Combinations.Add(comb.Reverse<T>().ToList());
                }
                else
                {
                    AddItem(index + 1);
                }

                comb.Pop();
            }
        }
    }
}

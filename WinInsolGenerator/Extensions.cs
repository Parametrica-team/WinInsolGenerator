using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinInsolGenerator
{
    public static class Extensions
    {
        public static List<T> PopMultiple<T>(this Stack<T> stack, int number)
        {
            var result = new List<T>();
            for (int i = 0; i < number; i++)
            {
                if (stack.Any())
                    result.Add(stack.Pop());
                else
                    break;
            }
            return result;
        }

        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static BitArray Prepend(this BitArray current, BitArray before)
        {
            var bools = new bool[current.Count + before.Count];
            before.CopyTo(bools, 0);
            current.CopyTo(bools, before.Count);
            return new BitArray(bools);
        }

        public static BitArray Append(this BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }
    }

    
}

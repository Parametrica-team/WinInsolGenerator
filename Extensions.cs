using System;
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
    }
}

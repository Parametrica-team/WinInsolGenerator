using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WinInsolGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.GetFullPath(path + @"..\..\..\..\..\example\one_string.txt");
            
            
            //read file
            var levelCodes = System.IO.File.ReadAllLines(path);
            foreach (var levelCode  in levelCodes)
            {
                var level = GetFlatsFromCode(levelCode);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelCode"></param>
        /// <param name="sideWindowsQty">количество окон в торце</param>
        /// <returns></returns>
        private static List<Flat> GetFlatsFromCode(string levelCode, int sideWindowsQty)
        {
            if (string.IsNullOrEmpty(levelCode)) return null;
            var codes = levelCode.Trim().Split(';');

            //total number of windows
            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i].Contains("CR))
            }
        }
    }
}

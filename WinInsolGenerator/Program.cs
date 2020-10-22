using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WinInsolGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.GetFullPath(path + @"..\..\..\..\..\example\one_string.txt");
            
            
            //read file
            var levelCodes = System.IO.File.ReadAllLines(path);
            foreach (var levelCode  in levelCodes)
            {
                //потом, когда все строки будут отсортированы, то нужно будет убрать
                var sortedCode = SortCodeClockwise(levelCode);

                var level = GetFlatsFromCode(sortedCode, 2);
                foreach (var flat in level)
                {
                    Console.WriteLine($"{flat.Id}\t{string.Join(',', flat.Indexes)}");
                }
            }

        }

        /// <summary>
        /// Создает квартиры из кода уровня.
        /// В квартиры записывается количество окон и их индексы, начиная с левого нижнего торцевого окна.
        /// </summary>
        /// <param name="levelCode"></param>
        /// <param name="sideWindowsQty">Количество окон в каждом торце. Обязательно должно быть четным числом! Если квартир в торцах нет, то можно ввести ноль</param>
        /// <returns>Список квартир</returns>
        private static List<Flat> GetFlatsFromCode(string levelCode, int sideWindowsQty)
        {
            if (string.IsNullOrEmpty(levelCode)) return null;
            var flats = levelCode.Trim().Split(',').Select(code => new Flat(code)).ToList();

            //total number windows
            int bottomWins = flats.Where(f => f.FType.Contains("C") || f.FType == "MD").Sum(f => f.BottomWindows);
            int topFlatWins = flats.Where(f => f.FType.Contains("C") || f.FType == "MU").Sum(f => f.TopWindows);
            int lluWins = bottomWins - topFlatWins;
            int totalWins = (bottomWins * 2) + (sideWindowsQty * 2);

            //add indexes
            var indexes = new Stack<int>(Enumerable.Range(0, totalWins).Reverse());
            foreach (var flat in flats)
            {
                //если ллу, то пропустить несколько окон
                if (flat.FType == "llu")
                {
                    flat.Indexes.AddRange(indexes.PopMultiple(lluWins));
                    flat.TopWindows = lluWins;
                    continue;
                }

                //угловым квартирам нужно еще на торцы добавить индексов
                if (flat.FType.Contains('C'))
                {
                    //если распашонка, то нужно все торцевые окна добавить, если нет то половину
                    int sideNum = sideWindowsQty;
                    if (flat.TopWindows == 0 || flat.BottomWindows == 0)
                        sideNum = sideWindowsQty / 2;

                    //добавить индексы на торцы
                    flat.Indexes.AddRange(indexes.PopMultiple(sideNum));
                }

                //для квартиры CL не нужно сразу добавлять индексы на нижние окна - они самые последние по порядку
                if (flat.FType != "CL" && flat.BottomWindows != 0)
                    flat.Indexes.AddRange(indexes.PopMultiple(flat.BottomWindows));

                //добавить верхние окна
                if (flat.TopWindows != 0)
                    flat.Indexes.AddRange(indexes.PopMultiple(flat.TopWindows));
            }

            //добавит все что осталось в CL
            flats[0].Indexes.AddRange(indexes);

            return flats;
        }

        

        public static string SortCodeClockwise(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;

            var codes = code.Trim().Split(',').ToList();
            var sorted = new List<string>();
            
            //add CL
            sorted.Add(codes[0]);

            var bottomRow = new List<string>();
            var topRow = new List<string>();
            for (int i = 1; i< codes.Count; i++)
            {
                bottomRow.Add(codes[i]);
                if (codes[i].Contains("CR"))
                {
                    bottomRow.Reverse();
                    topRow = codes.Skip(i + 1).ToList();
                    break;
                }                    
            }

            sorted.AddRange(topRow);
            sorted.AddRange(bottomRow);

            return string.Join(',', sorted);
        }
    }
}

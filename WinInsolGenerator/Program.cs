﻿using Robot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

namespace WinInsolGenerator
{
    public class Program
    {        
        static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            //path = System.IO.Path.GetFullPath(path + @"..\..\..\..\..\example\one_string.txt");
            path = System.IO.Path.GetFullPath(path + @"..\..\..\..\..\example\combinations_7.txt");
            
            var winInsols = new List<WindowInsol>();
            var hblockInsols = new List<HblockInsol>();
            //read file
            var levelCodes = System.IO.File.ReadAllLines(path);

            //потом, когда все строки будут отсортированы, то нужно будет убрать
            //levelCodes = levelCodes.Select(c => SortCodeClockwise(c)).ToArray();

            var winInsol = new WindowInsol()
            {
                llu = "llu",
                HblockId = "S_M_" + GetStepsFromCode(levelCodes[0]),
                WindowId = new HashSet<string>()
            };

            var hblockInsol = new HblockInsol(winInsol.HblockId, levelCodes);

            Console.WriteLine("Calculation started at " + DateTime.Now);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int counter = 0;
            foreach (var levelCode  in levelCodes)
            {
                //процент выполнения
                Console.Write("\r" + (int)(counter * 100 / levelCodes.Length) + " %");

                //Create Flats
                List<Flat> level = GetFlatsFromCode(levelCode, 2);

                //Create combinations inside each flat
                //нужно, чтобы учитывались 4+ комнатные квартиры внутри которых уже несколько комбинаций окон
                level.ForEach(f => f.SetWindowCombinations());

                //создать все возможные комбинации окон
                var combinator = new Combinator<string>(level
                    .Where(f => f.FType != "llu")
                    .Select(f => f.WindowCombinations).ToList());

                //Превратить в строку для winInsol                
                var sortedCombination = new List<string>();
                foreach (var comb in combinator.Combinations)
                {
                    //В больших квартирах записаны пары "1,5" их нужно учитывать
                    var nums = comb.SelectMany(c => c.Split(','))
                        .OrderBy(s => int.Parse(s)) //сортировать по возрастанию чисел                        
                        .ToList();

                    winInsol.WindowId.Add(string.Join(',', nums));
                }                

                //add WinInsol to list
                //winInsols.Add(winInsol);

                
                //собрать списки подходящих уровней для каждой комбинации инсоляции для HBlock                
                foreach (var winIns in winInsol.WindowId)
                {                    
                    if (hblockInsol.InsolData.ContainsKey(winIns))
                        hblockInsol.InsolData[winIns].Add(counter); //counter совпадает с индексом в hblockInsol.LevelCodes
                    else
                        hblockInsol.InsolData.Add(winIns, new HashSet<int>() { counter });
                }

                counter++;
            }

            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("Calculation finished at " + DateTime.Now);
            TimeSpan t = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
            Console.WriteLine($"Total time: {t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s:{t.Milliseconds:D3}ms");

            //сохранение json
            
            
            string folder = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            var winInsolPath = Path.Combine(folder, fileName + "_WinInsol.json");
            var winInsolJson = JsonConvert.SerializeObject(winInsol, Formatting.Indented);
            File.WriteAllText(winInsolPath, winInsolJson);
            Console.WriteLine($"WinInsol saved to {winInsolPath}");

            hblockInsol.Archive();
            string hblockInsolJson = JsonConvert.SerializeObject(hblockInsol, Formatting.Indented);
            var hblockInsolPath = Path.Combine(folder, fileName + "_HblockInsol.json");
            File.WriteAllText(hblockInsolPath, hblockInsolJson);
            Console.WriteLine($"HlockInsol saved to {hblockInsolPath}");
        }

        public static int GetStepsFromCode(string levelCode)
        {
            return  levelCode.Split(',')
                .Where(f => f != "llu")
                .Select(f => int.Parse(f.Split('_')[2]))
                .Sum();
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

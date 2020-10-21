using System;
using System.Collections.Generic;
using System.Text;

namespace WinInsolGenerator
{
    class Flat
    {
        public string Id { get; set; }
        public List<int> Indexes { get; set; }

        /// <summary>
        /// CL, CR, MD, MU, llu
        /// </summary>
        public string FType { get; set; }
        public int TopWindows { get; set; }
        public int BottomWindows { get; set; }

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
    }

}

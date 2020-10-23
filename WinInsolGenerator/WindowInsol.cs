using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinInsolGenerator
{
    [Serializable()]
    public class WindowInsol
    {
        public string HblockId { get; set; }
        public string llu { get; set; }
        public List<BitArray> WindowId { get; set; }

        public WindowInsol()
        {
        }

        public WindowInsol(int windowQty)
        {
            WindowId = new List<BitArray>();
        }
    }
}

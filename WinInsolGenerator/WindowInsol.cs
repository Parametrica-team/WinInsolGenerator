using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class WindowInsol
    {
        public string HblockId { get; set; }
        public string llu { get; set; }
        public byte[] WindowId { get; set; }

        public WindowInsol()
        {
        }

        public WindowInsol(int windowQty)
        {
            WindowId = new byte[windowQty];
        }
    }
}

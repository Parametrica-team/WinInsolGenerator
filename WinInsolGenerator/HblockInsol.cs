using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinInsolGenerator
{
    /// <summary>
    /// Здесь хранятся списки подходящих уровней для каждой комбинации инсоляции для HBlock
    /// </summary>
    public class HblockInsol
    {
        /// <summary>
        /// Тип секции
        /// </summary>
        public string HblockId { get; set; }
        /// <summary>
        /// Словарь winInsol - список уровней для каждой комбинации инсоляции
        /// </summary>
        public Dictionary<string, HashSet<string>> InsolData { get; set; }        

        public HblockInsol()
        {
            InsolData = new Dictionary<string, HashSet<string>>();
        }

        public HblockInsol(string hblockId)
        {
            HblockId = hblockId;
            InsolData = new Dictionary<string, HashSet<string>>();
        }
    }

}

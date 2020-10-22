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
        /// Словарь winInsol - список индексов(!) уровней для каждой комбинации инсоляции
        /// Индексы соответсвуют списку уровней в LevelCodes
        /// </summary>
        public Dictionary<string, HashSet<int>> InsolData { get; set; }        

        /// <summary>
        /// Все доступные уровни для данной секции
        /// </summary>
        public string[] LevelCodes { get; set; }

        public HblockInsol()
        {
            InsolData = new Dictionary<string, HashSet<int>>();
        }

        public HblockInsol(string hblockId)
        {
            HblockId = hblockId;
            InsolData = new Dictionary<string, HashSet<int>>();
        }

        public HblockInsol(string hblockId, string[] levelCodes)
        {
            HblockId = hblockId;
            InsolData = new Dictionary<string, HashSet<int>>();
            LevelCodes = levelCodes;
        }
    }

}

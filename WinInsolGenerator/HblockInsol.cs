using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinInsolGenerator
{
    [Serializable()]
    /// <summary>
    /// Здесь хранятся списки подходящих уровней для каждой комбинации инсоляции для HBlock
    /// </summary>
    public class HblockInsol
    {
        [DataMember]
        /// <summary>
        /// Тип секции
        /// </summary>
        public string HblockId { get; set; }


        /// <summary>
        /// Словарь winInsol - список индексов(!) уровней для каждой комбинации инсоляции
        /// Индексы соответсвуют списку уровней в LevelCodes
        /// </summary>    
        [DataMember]
        public Dictionary<BitArray, HashSet<int>> InsolData { get; set; }

        
        public Dictionary<BitArray, string> InsolDataArchive { get; private set; }

        [DataMember]
        /// <summary>
        /// Все доступные уровни для данной секции
        /// </summary>
        public string[] LevelCodes { get; set; }

        public HblockInsol()
        {
            InsolData = new Dictionary<BitArray, HashSet<int>>();
        }

        public HblockInsol(string hblockId)
        {
            HblockId = hblockId;
            InsolData = new Dictionary<BitArray, HashSet<int>>();
        }

        public HblockInsol(string hblockId, string[] levelCodes)
        {
            HblockId = hblockId;
            InsolData = new Dictionary<BitArray, HashSet<int>>();
            LevelCodes = levelCodes;
        }
    }

}

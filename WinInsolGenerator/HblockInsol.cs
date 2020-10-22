using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinInsolGenerator
{
    [DataContract]
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
        public Dictionary<string, HashSet<int>> InsolData { get; set; }

        [DataMember]
        public Dictionary<string, string> InsolDataArchive { get; private set; }

        [DataMember]
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

        public void Archive()
        {
            InsolDataArchive = new Dictionary<string, string>();
            foreach (var key in InsolData.Keys)
            {
                InsolDataArchive.Add(key, string.Join(',', InsolData[key]));
            }
        }
    }

}

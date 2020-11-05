using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WinInsolGenerator
{
    public class InsolationDataTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HblockType { get; set; }
        public int Steps { get; set; }
        public BitArray WinInsol { get; set; }
        public string LevelCode { get; set; }

        public InsolationDataTest() { }
    }

}

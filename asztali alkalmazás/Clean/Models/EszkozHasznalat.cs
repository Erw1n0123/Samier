using System;
using System.Collections.Generic;

#nullable disable

namespace Clean.Models
{
    public partial class EszkozHasznalat
    {
        public int EhId { get; set; }
        public int EhMId { get; set; }
        public int EhRId { get; set; }
        public int ElhasznaltMennyiseg { get; set; }

        public virtual Munka EhM { get; set; }
        public virtual Raktar EhR { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CleanBackend.Models
{
    public partial class Raktar
    {
        public Raktar()
        {
            EszkozHasznalats = new HashSet<EszkozHasznalat>();
        }

        public int RId { get; set; }
        public string Nev { get; set; }
        public byte[] Kepfajl { get; set; }
        public int Mennyiseg { get; set; }
        public bool Megjelenes { get; set; }

        public virtual ICollection<EszkozHasznalat> EszkozHasznalats { get; set; }
    }
}

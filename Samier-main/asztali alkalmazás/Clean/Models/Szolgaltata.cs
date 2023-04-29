using System;
using System.Collections.Generic;

#nullable disable

namespace Clean.Models
{
    public partial class Szolgaltata
    {
        public Szolgaltata()
        {
            Munkas = new HashSet<Munka>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public byte[] Kepfajl { get; set; }

        public virtual ICollection<Munka> Munkas { get; set; }
    }
}

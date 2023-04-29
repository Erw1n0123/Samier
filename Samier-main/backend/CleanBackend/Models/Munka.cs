using System;
using System.Collections.Generic;

#nullable disable

namespace CleanBackend.Models
{
    public partial class Munka
    {
        public Munka()
        {
            DolgozoBeoszta = new HashSet<DolgozoBeoszta>();
            EszkozHasznalats = new HashSet<EszkozHasznalat>();
        }

        public int MunkaId { get; set; }
        public string MunkaTeljesNev { get; set; }
        public string MunkaEmail { get; set; }
        public string MunkaTelefonszam { get; set; }
        public int MunkaIranyitoszam { get; set; }
        public string MunkaTelepules { get; set; }
        public string MunkaCim { get; set; }
        public string MunkaLeiras { get; set; }
        public int? Ar { get; set; }
        public string Idopont { get; set; }
        public int SzId { get; set; }
        public string Datum { get; set; }
        public int? Allapot { get; set; }

        public virtual Szolgaltata Sz { get; set; }
        public virtual ICollection<DolgozoBeoszta> DolgozoBeoszta { get; set; }
        public virtual ICollection<EszkozHasznalat> EszkozHasznalats { get; set; }
    }
}

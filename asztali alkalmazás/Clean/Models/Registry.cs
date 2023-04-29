using System;
using System.Collections.Generic;

#nullable disable

namespace Clean.Models
{
    public partial class Registry
    {
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Email { get; set; }
        public string Telefonszam { get; set; }
        public int Iranyitoszam { get; set; }
        public string Telepules { get; set; }
        public string Cim { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string Key { get; set; }
    }
}

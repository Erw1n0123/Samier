using System;
using System.Collections.Generic;

#nullable disable

namespace CleanBackend.Models
{
    public partial class DolgozoBeoszta
    {
        public int DbId { get; set; }
        public int DbMId { get; set; }
        public int DId { get; set; }

        public virtual Felhasznalo DIdNavigation { get; set; }
        public virtual Munka DbM { get; set; }
    }
}

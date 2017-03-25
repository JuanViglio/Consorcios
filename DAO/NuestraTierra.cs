using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class NuestraTierra
    {
        public class PadresModel
        {
            public string Mail { get; set; }
            public string Nombre { get; set; }
            public string Password { get; set; }
            public bool Admin { get; set; }
            public bool ? Confirmado { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class SeguroDetalleModel
    {
        public int Cuota { get; set; }

        public int Periodo { get; set; }

        public decimal Importe { get; set; }
    }
}

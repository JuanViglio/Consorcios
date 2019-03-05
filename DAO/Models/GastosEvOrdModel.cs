using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class GastosEvOrdModel
    {
        public decimal ID { get; set; }

        public string Detalle { get; set; }

        public decimal? ImporteCompra { get; set; }

        public decimal? Importe { get; set; }

        public string Proveedor { get; set; }

        public decimal ID_Proveedores { get; set; }
    }
}

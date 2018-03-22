using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class ProveedoresModel
    {
        public decimal Codigo { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Mail { get; set; }

        public string Telefono { get; set; }

        public string Tipo { get; set; }

        public decimal Saldo { get; set; }
    }
}

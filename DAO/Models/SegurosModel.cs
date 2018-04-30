using System;

namespace DAO.Models
{
    public class SegurosModel
    {
        public decimal ID { get; set; }

        public string Compañia { get; set; }

        public string Poliza { get; set; }

        public string Tipo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int CantCuotas { get; set; }

        public int CantCuotas0 { get; set; }

        public string Estado { get; set; }

        public string Consorcio { get; set; }
    }
}

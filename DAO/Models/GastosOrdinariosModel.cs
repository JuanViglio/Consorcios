﻿namespace DAO
{
    public class GastosOrdinariosModel
    {
        public string Detalle { get; set; }

        public decimal? Importe { get; set; }

        public decimal ID { get; set; }

        public bool Sumar { get; set; }

        public int Orden { get; set; }

        public string GastoID { get; set; }
    }
}

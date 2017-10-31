using DAO;
using System.Collections.Generic;
using System.Linq;

namespace Servicios.Mapper
{
    public static class AutoMapper
    {
        public static IEnumerable<GastosOrdinariosModel> MapToGastosOrdinarios(List<ExpensasDetalle> expensasDetalles)
        {
            IEnumerable<GastosOrdinariosModel> gastosOdinarios;

            return gastosOdinarios = from e in expensasDetalles
                              select new GastosOrdinariosModel()
                              {
                                  Detalle = e.Detalle,
                                  ID = e.ID,
                                  Importe = e.Importe,
                                  Sumar = e.Sumar != null ? e.Sumar.Value : false
                              };
        }
    }
}

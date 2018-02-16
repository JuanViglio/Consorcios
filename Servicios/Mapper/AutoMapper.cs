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
                                     orderby e.Orden
                                     select new GastosOrdinariosModel()
                                     {
                                         Detalle = e.Detalle,
                                         ID = e.ID,
                                         Importe = e.Importe,
                                         Sumar = e.Sumar.GetValueOrDefault(),
                                         Orden = e.Orden.GetValueOrDefault(),
                                         GastoID = e.Gastos_ID.ToString()
                                     };
        }

        public static IEnumerable<GastosOrdinariosModel> MapToGastosOrdinariosUF(List<ExpensasUFDetalle> expensasDetalles)
        {
            IEnumerable<GastosOrdinariosModel> gastosOdinarios;

            return gastosOdinarios = from e in expensasDetalles
                                     orderby e.Orden
                                     select new GastosOrdinariosModel()
                                     {
                                         Detalle = e.Detalle,
                                         ID = e.ID,
                                         Importe = e.Importe,
                                         Sumar = e.Sumar.GetValueOrDefault(),
                                         Orden = e.Orden.GetValueOrDefault(),
                                         GastoID = e.Gastos_ID.ToString()
                                     };
        }

        public static IEnumerable <UnidadesFuncionalesModel> MaptToUnidadesFuncionalesModel(List<UnidadesFuncionales> unidadesFuncionales)
        {
            IEnumerable<UnidadesFuncionalesModel> ufModel;

            return ufModel = from u in unidadesFuncionales
                             select new UnidadesFuncionalesModel()
                             {
                                 Departamento = u.Departamento,
                                 ID = u.ID,
                                 Apellido = u.Apellido,
                                 Nombre = u.Nombre,
                                 UF = u.UF,
                                 Cochera = u.Cochera == true ? "SI" : "NO",
                                 Coeficiente = u.Coeficiente.ToString(),
                                 Aplicar = false                        
                             };
        }

        
    }
}

using DAO;
using Servicios.Interfaces;
using System.Linq;

namespace Servicios
{
    public class detallesServ : IDetallesServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public void GuardarDetalle(string detalle, string idConsorcio, decimal idGasto)
        {
            var consorcio = context.Consorcios.Where(x => x.ID == idConsorcio).FirstOrDefault();
            var gasto = context.Gastos.Where(x => x.ID == idGasto).FirstOrDefault();
            var detalles = context.Detalles.Where(x => x.Consorcios.ID == consorcio.ID && x.Gastos.ID == gasto.ID).FirstOrDefault();

            if (detalles != null)
            {
                context.DeleteObject(detalles);
            }

            context.AddToDetalles(new Detalles
            {
                Consorcios =  consorcio,
                Gastos = gasto,
                Detalle = detalle
            });
            context.SaveChanges();
        }

        public string GetDetalle(string idConsorcio, decimal idGasto)
        {
            //var consorcio = context.Consorcios.Where(x => x.ID == idConsorcio).FirstOrDefault();
            //var gasto = context.Gastos.Where(x => x.ID == idGasto).FirstOrDefault();
            var detalle = context.Detalles.Where(x => x.Consorcios_ID == idConsorcio && x.Gastos_ID == idGasto).FirstOrDefault();
            
            return detalle == null ? string.Empty : detalle.Detalle;            
        }
    }
}

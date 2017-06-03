using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class unidadesFuncionalesServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public List<UnidadesFuncionales> GetUnidadesFuncionales(string consorciosID)
        {
            var unidadesFuncionales = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorciosID).ToList();

            return unidadesFuncionales;
        }

        public void CancelarPagos(string consorciosID, int Periodo)
        {
            //var UF = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorciosID).ToList();
            //var pagos = context.Pagos.Where(x => x.Periodo == Periodo).ToList();

            //foreach (var item in pagos)
            //{
            //    context.DeleteObject(item);
            //}

            var consorcio = context.Consorcios.Where(x => x.ID == consorciosID).FirstOrDefault();
            var expensa = context.Expensas.Where(x => x.Consorcios.ID == consorcio.ID && x.PeriodoNumerico == Periodo).FirstOrDefault();

            expensa.Estado = "En Proceso";

            context.SaveChanges();
        }

        public List<UnidadesFuncionalesModel> GetPagos(string consorciosID, int Periodo)
        {            
            var UF = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorciosID).ToList();
            var pagos = context.Pagos.Where(x => x.Periodo == Periodo).ToList();
            List<UnidadesFuncionalesModel> UFlist = new List<UnidadesFuncionalesModel>();
            UnidadesFuncionalesModel UFmodel;

            foreach (var item in pagos)
            {
                UFmodel = new UnidadesFuncionalesModel();

                UFmodel.ID = item.UnidadesFuncionales.UF;
                UFmodel.Dueño = item.UnidadesFuncionales.Dueño;
                UFmodel.Coeficiente = item.Coeficiente.ToString();
                UFmodel.PagoId = item.ID.ToString();

                UFlist.Add(UFmodel);
            }

            return UFlist;
        }

        public Pagos GetPago(string PagoId)
        {
            decimal decPagoId = Convert.ToDecimal(PagoId);
            return context.Pagos.Where(x => x.ID == decPagoId).FirstOrDefault();
        }

        public void GuardarGastoParticular(int UFid, decimal importe, string detalle)
        {
            Pagos pago = context.Pagos.Where(x => x.ID == UFid).FirstOrDefault();
            pago.ImporteGastoParticular = importe;
            pago.DetalleGastoParticular = detalle;
            context.SaveChanges();
        }
    }
}

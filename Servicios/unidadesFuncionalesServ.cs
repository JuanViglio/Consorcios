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
            var consorcio = context.Consorcios.Where(x => x.ID == consorciosID).FirstOrDefault();
            var expensa = context.Expensas.Where(x => x.Consorcios.ID == consorcio.ID && x.PeriodoNumerico == Periodo).FirstOrDefault();

            expensa.Estado = "En Proceso";
            context.SaveChanges();
        }

        public void FinalizarPagos(string consorciosID, int Periodo)
        {
            var consorcio = context.Consorcios.Where(x => x.ID == consorciosID).FirstOrDefault();
            var expensa = context.Expensas.Where(x => x.Consorcios.ID == consorcio.ID && x.PeriodoNumerico == Periodo).FirstOrDefault();

            expensa.Estado = "Finalizado";
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

                UFmodel.ID = Convert.ToDecimal(item.UnidadesFuncionales.UF);
                UFmodel.Dueño = item.UnidadesFuncionales.Dueño;
                UFmodel.Coeficiente = item.Coeficiente.ToString();
                UFmodel.PagoId = item.ID.ToString();

                UFlist.Add(UFmodel);
            }

            return UFlist;
        }

        public IQueryable<PagosModel> GetPagosByFilter(decimal id)
        {
            var pagos = context.Pagos.Where(x => x.UnidadesFuncionales.ID == id && x.ImportePagado != null).ToList();

            var model = from p in context.Pagos  
                    where p.UnidadesFuncionales.ID == id && p.ImportePagado == null
                    select new PagosModel { ID = p.ID, Importe  = p.ImportePago1, FechaPago1 = p.FechaPago1, Importe2 = p.ImportePago2, FechaPago2 = p.FechaPago2, Periodo = p.PeriodoDetalle};

            foreach (var item in model)
            {
                item.Importe = item.Importe1;

                //if (item.FechaPago1 >= System.DateTime.Now)
                //    item.Importe = item.Importe1;
                //else if (item.FechaPago1 >= System.DateTime.Now)
                //    item.Importe = item.Importe2;
                //else
                //    item.Importe = 100000; //cuenta !!!
            }

            return model;
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

        public IQueryable<UnidadesFuncionalesModel> GetUFByFilter(string nombre, string direccion)
        {
            List<UnidadesFuncionalesModel> model = new List<UnidadesFuncionalesModel>();
            var uf = from u in context.UnidadesFuncionales
                     where (u.Dueño.Contains(nombre) || nombre == "") 
                     select new UnidadesFuncionalesModel {ID = u.ID, Direccion = u.Consorcios.Direccion, Dueño = u.Dueño,UF = u.UF };
            
            return uf;
        }

        public void PagarExpensa(decimal importe, decimal idPago)
        {
            Pagos pago = context.Pagos.Where(x => x.ID == idPago).FirstOrDefault();

            pago.ImportePagado = importe;
            pago.FechaPagado = System.DateTime.Now;

            context.SaveChanges();
        }
    }
}

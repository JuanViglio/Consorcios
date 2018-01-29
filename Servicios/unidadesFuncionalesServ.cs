using DAO;
using Servicios.Interfaces;
using Servicios.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class unidadesFuncionalesServ : IUnidadesServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public IEnumerable<UnidadesFuncionalesModel> GetUnidadesFuncionales(string consorciosID)
        {
            var unidadesFuncionales = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorciosID).OrderBy(x => x.UF).ToList();                        

            return AutoMapper.MaptToUnidadesFuncionalesModel(unidadesFuncionales);
        }

        public List<UnidadesFuncionales> GetAllUnidadesFuncionales()
        {
            return context.UnidadesFuncionales.ToList();
        }

        public UnidadesFuncionales GetUnidadFuncional(string consorciosID, string UF)
        {
            var unidadesFuncionales = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorciosID && x.UF == UF).FirstOrDefault();

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
            //var UF = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorciosID).ToList();
            var pagos = from P in context.Pagos
                        join UF in context.UnidadesFuncionales
                        on P.UnidadesFuncionales.ID equals UF.ID
                        where P.Periodo == Periodo
                            && UF.Consorcios.ID == consorciosID
                        select new { P.UnidadesFuncionales.UF, UF.Apellido, UF.Nombre, P.Coeficiente, P.ID };

            List<UnidadesFuncionalesModel> UFlist = new List<UnidadesFuncionalesModel>();

            foreach (var item in pagos)
            {
                UFlist.Add(new UnidadesFuncionalesModel()
                {
                    ID = Convert.ToDecimal(item.UF),
                    Apellido = item.Apellido,
                    Nombre = item.Nombre,
                    Coeficiente = item.Coeficiente.ToString(),
                    PagoId = item.ID.ToString(),
                });                
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

        public IQueryable<UnidadesFuncionalesModel> GetUFByFilter(string apellido, string direccion)
        {
            List<UnidadesFuncionalesModel> model = new List<UnidadesFuncionalesModel>();
            var uf = from u in context.UnidadesFuncionales
                     where (u.Apellido.Contains(apellido) || apellido == "") 
                     select new UnidadesFuncionalesModel {ID = u.ID, Direccion = u.Consorcios.Direccion, Apellido = u.Apellido, UF = u.UF };
            
            return uf;
        }

        public void PagarExpensa(decimal importe, decimal idPago)
        {
            Pagos pago = context.Pagos.Where(x => x.ID == idPago).FirstOrDefault();

            pago.ImportePagado = importe;
            pago.FechaPagado = System.DateTime.Now;

            context.SaveChanges();
        }

        public IEnumerable<UnidadesFuncionalesModel> ModificarUnidades(string idConsorcio, int idUF, string departamento, string numeroUF, string apellido, string nombre, decimal coeficiente, string cochera) 
        {
            var UF = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == idConsorcio && x.ID == idUF).FirstOrDefault();
            UF.UF = numeroUF;
            UF.Departamento = departamento;
            UF.Apellido = apellido;
            UF.Nombre = nombre;
            UF.Coeficiente = coeficiente;
            UF.Cochera = cochera == "SI" ? true : false;
            context.SaveChanges();

            return GetUnidadesFuncionales(idConsorcio);
        }

        public IEnumerable<UnidadesFuncionalesModel> AgregarUnidad(string idConsorcio, string idUf, string departamento, string apellido, string nombre, decimal coeficiente, string cochera)
        {
            var consorcio = context.Consorcios.Where(x => x.ID == idConsorcio).FirstOrDefault();

            UnidadesFuncionales UF = new UnidadesFuncionales();
            UF.UF = idUf;
            UF.Departamento = departamento;
            UF.Consorcios = consorcio;
            UF.Apellido = apellido;
            UF.Nombre = nombre;
            UF.Coeficiente = coeficiente;
            context.AddToUnidadesFuncionales(UF);
            UF.Cochera = cochera == "SI" ? true : false;

            context.SaveChanges();

            return GetUnidadesFuncionales(idConsorcio);
        }
    }
}

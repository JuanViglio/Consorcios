using DAO;
using Servicios.Interfaces;
using Servicios.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSistemmas.Common;

namespace Servicios
{
    public class unidadesFuncionalesServ : IUnidadesServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public IEnumerable<UnidadesFuncionalesModel> GetUnidadesFuncionales(string consorciosID)
        {
            var dueños = context.Dueños;
            return (from u in context.UnidadesFuncionales
                    join d in context.Dueños
                    on u.Dueños.ID equals d.ID
                    where u.Consorcios.ID == consorciosID
                    orderby u.UF
                    select new UnidadesFuncionalesModel()
                    {
                        Departamento = u.Departamento,
                        ID = u.ID,
                        Apellido = d.Apellido,
                        Nombre = d.Nombre,
                        UF = u.UF,
                        Cochera = u.Cochera == true ? "SI" : "NO",
                        Coeficiente = u.Coeficiente,
                        Aplicar = false,
                        Dueños_Id = d.ID
                    }).ToList();
        }

        public IEnumerable<UnidadesFuncionalesModel> GetUFByPropietarioId (decimal propietarioID)
        {
            var dueños = context.Dueños;
            return (from u in context.UnidadesFuncionales
                    join d in context.Dueños
                        on u.Dueños.ID equals d.ID
                    join p in context.Pagos 
                        on u.ID equals p.UnidadesFuncionales.ID
                    where d.ID == propietarioID 
                        && p.Estado == Constantes.EstadoAdeudado
                    orderby u.UF
                    select new UnidadesFuncionalesModel()
                    {
                        Departamento = u.Departamento,
                        ID = p.ID,
                        Apellido = d.Apellido,
                        Nombre = d.Nombre,
                        UF = u.UF,
                        Cochera = u.Cochera == true ? "SI" : "NO",
                        Coeficiente = u.Coeficiente,
                        Aplicar = false,
                        Dueños_Id = d.ID,
                        Direccion = u.Consorcios.Direccion,
                        PeriodoDetalle = p.PeriodoDetalle
                    }).ToList();
        }

        public IEnumerable<UnidadesFuncionalesModel> GetUnidadesFuncionalesCombo(string consorciosID)
        {
            List<UnidadesFuncionalesModel> ufModel = new List<UnidadesFuncionalesModel>();
            var dueños = context.Dueños;

            var UF = (from u in context.UnidadesFuncionales
                      join d in context.Dueños
                      on u.Dueños.ID equals d.ID
                      where u.Consorcios.ID == consorciosID
                      orderby u.UF
                      select new UnidadesFuncionalesModel()
                      {
                          Departamento = u.Departamento,
                          ID = u.ID,
                          Apellido = d.Apellido,
                          Nombre = d.Nombre,
                          UF = u.UF,
                          Cochera = u.Cochera == true ? "SI" : "NO",
                          Coeficiente = u.Coeficiente,
                          Aplicar = false,
                          Dueños_Id = d.ID
                      }).ToList();

            ufModel.Add(new UnidadesFuncionalesModel()
            {
                ID = 0,
                UF = "Seleccione una Unidad Funcional"
            });

            foreach (var item in UF)
            {
                ufModel.Add(new UnidadesFuncionalesModel()
                {
                    ID = item.ID,
                    UF = item.UF
                });
            }

            return ufModel;
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
            var UFlist = (from P in context.Pagos
                        join U in context.UnidadesFuncionales
                        on P.UnidadesFuncionales.ID equals U.ID
                        where P.Periodo == Periodo
                            && U.Consorcios.ID == consorciosID
                        orderby U.Departamento
                        select new UnidadesFuncionalesModel
                        {
                            ID = P.UnidadesFuncionales.ID,
                            Apellido = U.Apellido,
                            Nombre = U.Nombre,
                            Coeficiente = P.Coeficiente,
                            PagoId = P.ID,
                            UF = P.UnidadesFuncionales.UF,
                            PeriodoDetalle = P.PeriodoDetalle
                        }).ToList();

            return UFlist;
        }

        public List<UnidadesFuncionalesModel> GetPagosConCochera(string consorciosID, int Periodo, bool agregarValorCochera)
        {
            var pagos = from P in context.Pagos
                        join UF in context.UnidadesFuncionales
                        on P.UnidadesFuncionales.ID equals UF.ID
                        where P.Periodo == Periodo
                            && UF.Consorcios.ID == consorciosID                     
                        orderby UF.Departamento   
                        select new { P.UnidadesFuncionales.UF, UF.Apellido, UF.Nombre, P.Coeficiente, P.ID, UF.Cochera };

            List<UnidadesFuncionalesModel> UFlist = new List<UnidadesFuncionalesModel>();

            foreach (var item in pagos)
            {
                UFlist.Add(new UnidadesFuncionalesModel()
                {
                    ID = item.ID,
                    Apellido = item.Apellido,
                    Nombre = item.Nombre,
                    Coeficiente = item.Coeficiente,
                    PagoId = item.ID,
                    Aplicar = GetValorCochera(item.Cochera, agregarValorCochera),
                    UF = item.UF
                });
            }

            return UFlist;
        }

        private bool GetValorCochera(bool? valorCochera, bool agregarValorCochera)
        {
            if (agregarValorCochera)
                return valorCochera.GetValueOrDefault();
            else
                return false;
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
        
        public Pagos GetPago(decimal PagoId)
        {
            decimal decPagoId = PagoId;
            return context.Pagos.Where(x => x.ID == decPagoId).FirstOrDefault();
        }

        public void GuardarGastoParticular(decimal UFid, decimal importe, string detalle)
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

            pago.ImportePagado = pago.ImportePagado + importe;
            pago.FechaPagado = System.DateTime.Now;

            if (pago.ImportePagado == pago.ImportePago1) //QUE PASA SI ES MAYOR A LA FECHA DE PAGO 1
                pago.Estado = Constantes.EstadoPagado;

            context.SaveChanges();
        }

        public IEnumerable<UnidadesFuncionalesModel> ModificarUnidades(string idConsorcio, int idUF, string departamento, string numeroUF, decimal coeficiente, string cochera, decimal idDueño) 
        {
            var UF = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == idConsorcio && x.ID == idUF).FirstOrDefault();
            var dueño = context.Dueños.Where(x => x.ID == idDueño).FirstOrDefault();

            UF.UF = numeroUF;
            UF.Departamento = departamento;
            UF.Coeficiente = coeficiente;
            UF.Cochera = cochera == "SI" ? true : false;
            UF.Dueños = dueño;
            context.SaveChanges();

            return GetUnidadesFuncionales(idConsorcio);
        }

        public IEnumerable<UnidadesFuncionalesModel> AgregarUnidad(string idConsorcio, string idUf, string departamento, decimal coeficiente, string cochera, decimal idDueño)
        {
            var consorcio = context.Consorcios.Where(x => x.ID == idConsorcio).FirstOrDefault();
            var dueño = context.Dueños.Where(x => x.ID == idDueño).FirstOrDefault();

            UnidadesFuncionales UF = new UnidadesFuncionales();
            UF.UF = idUf;
            UF.Departamento = departamento;
            UF.Consorcios = consorcio;
            UF.Coeficiente = coeficiente;
            context.AddToUnidadesFuncionales(UF);
            UF.Cochera = cochera == "SI" ? true : false;
            UF.Dueños = dueño;
            context.SaveChanges();

            return GetUnidadesFuncionales(idConsorcio);
        }

        public List<UnidadesFuncionalesCtaCte> GetCtaCte(decimal idUF)
        {
            return context.UnidadesFuncionalesCtaCte.Where(x => x.UnidadesFuncionales.ID == idUF).ToList();
        }

        public void AddHaber (UnidadesFuncionalesCtaCte UFctaCte)
        {
            try
            {
                context.AddToUnidadesFuncionalesCtaCte(UFctaCte);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("No se pude agregar el dato en la Cuenta Corriente de la UF");
            }

        }
    }
}

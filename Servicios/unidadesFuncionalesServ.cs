﻿using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class unidadesFuncionalesServ : IUnidadesServ
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
                UFmodel.Apellido = item.UnidadesFuncionales.Apellido;
                UFmodel.Nombre = item.UnidadesFuncionales.Nombre;
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

        public List<UnidadesFuncionales> ModificarUnidades(string idConsorcio, int idUF, string departamento, string numeroUF, string apellido, string nombre, decimal coeficiente) 
        {
            var UF = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == idConsorcio && x.ID == idUF).FirstOrDefault();
            UF.UF = numeroUF;
            UF.Departamento = departamento;
            UF.Apellido = apellido;
            UF.Nombre = nombre;
            UF.Coeficiente = coeficiente;
            context.SaveChanges();

            return GetUnidadesFuncionales(idConsorcio);
        }

        public List<UnidadesFuncionales> AgregarUnidad(string idConsorcio, string idUf, string departamento, string apellido, string nombre, decimal coeficiente)
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
            context.SaveChanges();

            return GetUnidadesFuncionales(idConsorcio);
        }
    }
}

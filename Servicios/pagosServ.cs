using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSistemmas.Common;

namespace Servicios
{
    public class pagosServ : IPagosServ
    {
        private ExpensasEntities _context;

        public pagosServ(ExpensasEntities context)
        {
            _context = context;
        }

        public void AddPagos(string consorcioId, UnidadesFuncionales unidadFuncional, string gastosExtraordinarios,string totalGastosOrdinarios, 
            int periodoNumerico, List<GastosEvOrd> gastosEvOrd, List<GastosFijos> expensaDetalle)
        {
            unidadesFuncionalesServ _unidadesFuncServ = new unidadesFuncionalesServ();
            var Coeficiente = unidadFuncional.Coeficiente.Value;
            //var GastosExtraordinarios = Constantes.GetDecimalFromCurrency(gastosExtraordinarios);
            //var ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            //var GastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios);  //CONTROLAR !!!!
            //var ImporteOrdinario = GastosOrdinarios * Coeficiente / 100;
            //var TotalVencimiento1 = ImporteOrdinario + ImporteExtraordinario;

            #region Add Pagos
            var pago = new Pagos()
            {
                ImporteGastoParticular = Convert.ToDecimal("0"),
                Coeficiente = Coeficiente,
                //ImportePago1 = TotalVencimiento1,
                //ImportePago2 = TotalVencimiento1 + 10,
                //ImporteExtraordinario = ImporteExtraordinario,
                Periodo = periodoNumerico,
                UnidadesFuncionales = unidadFuncional
            };

            _context.AddToPagos(pago);
            _context.SaveChanges();
            pago = _context.Pagos.OrderByDescending(x => x.ID).FirstOrDefault();
            #endregion

            _context.SaveChanges();
        }

        public void AddGastoParticularOrdinario(int idPago, string detalle, decimal importe)
        {
            var pago = _context.Pagos.Where(x => x.ID == idPago).FirstOrDefault();

            var gastosEvOrdUFDetalle = new GastosParticularesOrd()
            {
                Detalle = detalle,
                Importe = importe,
                Pagos = pago
            };

            _context.AddToGastosParticularesOrd(gastosEvOrdUFDetalle);
            _context.SaveChanges();
        }

        public void AddGastoParticularExtraordinario(int idPago, string detalle, decimal importe)
        {
            var pago = _context.Pagos.Where(x => x.ID == idPago).FirstOrDefault();

            var gastosEvExtUFDetalle = new GastosParticularesExt()
            {
                Detalle = detalle,
                Importe = importe,
                Pagos = pago
            };

            _context.AddToGastosParticularesExt(gastosEvExtUFDetalle);
            _context.SaveChanges();
        }

        public bool UpdatePagos(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios,
            string totalGastosOrdinarios, int periodoNumerico)
        {
            var pago = _context.Pagos.Where(x => x.UnidadesFuncionales.UF == item.UF && x.Periodo == periodoNumerico
                && x.UnidadesFuncionales.Consorcios.ID == consorcioId).FirstOrDefault();

            if (pago == null)
            {
                return false;
            }

            var Coeficiente = item.Coeficiente.Value;
            var GastosExtraordinarios = Constantes.GetDecimalFromCurrency(gastosExtraordinarios);
            var ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            var GastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios);  //CONTROLAR !!!!
            var ImporteOrdinario = GastosOrdinarios * Coeficiente / 100;
            var TotalVencimiento1 = GastosOrdinarios + GastosExtraordinarios;

            pago.ImportePago1 = TotalVencimiento1;
            pago.ImportePago2 = TotalVencimiento1 + 10;
            pago.ImporteExtraordinario = ImporteExtraordinario;

            _context.SaveChanges();

            return true;
        }

        public List<decimal> GetPagos(int periodo, string consorcioId)
        {
            return (from P in _context.Pagos
                        join UF in _context.UnidadesFuncionales
                        on P.UnidadesFuncionales.ID equals UF.ID
                        where P.Periodo == periodo && 
                              UF.Consorcios.ID == consorcioId 
                        select P.ID).ToList();            
        }

        public List<GastosParticularesOrd> GetGastosEvOrdinariosUF(int IdPago)
        {
            return _context.GastosParticularesOrd.Where(x => x.Pagos.ID == IdPago).ToList();
        }

        public List<GastosParticularesExt> GetGastosEvExtUF(int IdPago)
        {
            return _context.GastosParticularesExt.Where(x => x.Pagos.ID == IdPago).ToList();
        }

        public decimal GetTotalGastosEvOrdinariosUF(int IdPago)
        {
            return _context.GastosParticularesOrd.Where(x => x.Pagos.ID == IdPago).Sum(x => x.Importe) ?? 0;
        }

        public decimal GetTotalGastosEvExtUF(int IdPago)
        {
            return _context.GastosParticularesExt.Where(x => x.Pagos.ID == IdPago).Sum(x => x.Importe) ?? 0;
        }

        public void DeleteGastosEvOrdinariosUF(int IdGasto)
        {
            var gasto = _context.GastosParticularesOrd.Where(x => x.ID == IdGasto).FirstOrDefault();

            _context.DeleteObject(gasto);
            _context.SaveChanges();
        }

        public void DeleteGastosEvExtUF(int IdGasto)
        {
            var gasto = _context.GastosParticularesExt.Where(x => x.ID == IdGasto).FirstOrDefault();

            _context.DeleteObject(gasto);
            _context.SaveChanges();
        }
    }
}

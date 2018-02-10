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
            int periodoNumerico, List<GastosEvOrdinariosDetalle> gastosEvOrd, List<ExpensasDetalle> expensaDetalle)
        {
            unidadesFuncionalesServ _unidadesFuncServ = new unidadesFuncionalesServ();
            var Coeficiente = unidadFuncional.Coeficiente.Value;
            var GastosExtraordinarios = Constantes.GetDecimalFromCurrency(gastosExtraordinarios);
            var ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            var TotalGastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios);  //CONTROLAR !!!!
            var TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;
            GastosEvOrdinariosUFDetalle gastosEvOrdUFDetalle;
            ExpensasUFDetalle expensaDetalleUF;

            var pago = new Pagos()
            {
                ImporteGastoParticular = Convert.ToDecimal("0"),
                Coeficiente = Coeficiente,
                ImportePago1 = TotalVencimiento1,
                ImportePago2 = TotalVencimiento1 + 10,
                ImporteExtraordinario = ImporteExtraordinario,
                Periodo = periodoNumerico,
                UnidadesFuncionales = unidadFuncional
            };

            _context.AddToPagos(pago);
            _context.SaveChanges();
            pago = _context.Pagos.OrderByDescending(x => x.ID).FirstOrDefault();

            //Agregar Gastos Eventuales Ordinarios a la tabla GastosEvOrdinariosUF
            foreach (var item in gastosEvOrd)
            {
                gastosEvOrdUFDetalle = new GastosEvOrdinariosUFDetalle()
                {
                    Detalle = item.Detalle,
                    Importe = item.Importe,
                    Pagos = pago
                };

                _context.AddToGastosEvOrdinariosUFDetalle(gastosEvOrdUFDetalle);
            }

            //Agregar Gastos Fijos a la tabla ExpensasDetalleUF
            foreach (var item in expensaDetalle)
            {
                expensaDetalleUF = new ExpensasUFDetalle()
                {
                    Detalle = item.Detalle,
                    Importe = item.Importe,
                    TipoGasto_ID = item.TipoGasto_ID,
                    Sumar = item.Sumar,
                    Orden = item.Orden,
                    Gastos_ID = item.Gastos_ID,
                    Pagos = pago
                };

                _context.AddToExpensasUFDetalle(expensaDetalleUF);
            }

            _context.SaveChanges();
        }

        public void AddGastosEvOrdinariosEFDetalle(int idPago, string detalle, decimal importe)
        {
            var pago = _context.Pagos.Where(x => x.ID == idPago).FirstOrDefault();

            var gastosEvOrdUFDetalle = new GastosEvOrdinariosUFDetalle()
            {
                Detalle = detalle,
                Importe = importe,
                Pagos = pago
            };

            _context.AddToGastosEvOrdinariosUFDetalle(gastosEvOrdUFDetalle);
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
            var TotalGastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios) + pago.ImporteGastoParticular;
            var TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;

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

        public List<GastosEvOrdinariosUFDetalle> GetGastosEvOrdinariosUF(int IdPago)
        {
            return _context.GastosEvOrdinariosUFDetalle.Where(x => x.Pagos.ID == IdPago).ToList();
        }

    }
}

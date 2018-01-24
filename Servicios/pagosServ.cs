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

        public void AddPagos(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios,string totalGastosOrdinarios, 
            int periodoNumerico)
        {
            unidadesFuncionalesServ _unidadesFuncServ = new unidadesFuncionalesServ();
            var Coeficiente = item.Coeficiente.Value;
            var GastosExtraordinarios = Constantes.GetDecimalFromCurrency(gastosExtraordinarios);
            var ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            var TotalGastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios);  //CONTROLAR !!!!
            var TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;

            var pago = new Pagos()
            {
                ImporteGastoParticular = Convert.ToDecimal("0"),
                Coeficiente = Coeficiente,
                ImportePago1 = TotalVencimiento1,
                ImportePago2 = TotalVencimiento1 + 10,
                ImporteExtraordinario = ImporteExtraordinario,
                Periodo = periodoNumerico,
                UnidadesFuncionales = item
            };

            _context.AddToPagos(pago);
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

    }
}

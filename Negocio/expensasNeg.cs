using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Linq;
using WebSistemmas.Common;

namespace Negocio
{
    public class expensasNeg
    {
        private ExpensasEntities context = new ExpensasEntities();
        readonly IExpensasServ _expensasServ;
        readonly IUnidadesServ _unidadesFuncServ;
        readonly IPagosServ _pagosServ;

        public expensasNeg()
        {
            _expensasServ = new expensasServ();
            _unidadesFuncServ = new unidadesFuncionalesServ();
            _pagosServ = new pagosServ(context);
        }

        public void AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios)
        {
            try
            {
                var consorcioId = _expensasServ.GetConsorcio(expensaID);
                var expensa = context.Expensas.Where(x => x.ID == expensaID).FirstOrDefault();
                var consorcios = context.Consorcios.ToList();
                var unidadesFuncionales = context.UnidadesFuncionales.ToList();

                expensa.Estado = "Aceptado";

                //buscar las UF de consorcio
                foreach (var item in expensa.Consorcios.UnidadesFuncionales)
                {
                    //Buscar los pagos y los sobreescribe. Si no los encuentra los crea
                    if (!ModificarPago(item, gastosExtraordinarios, totalGastosOrdinarios, expensa, consorcioId))
                    {
                        CrearPago(consorcioId, item, gastosExtraordinarios, totalGastosOrdinarios, expensa.PeriodoNumerico.Value);
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ModificarPago(UnidadesFuncionales item, string gastosExtraordinarios, string totalGastosOrdinarios, Expensas expensa, string consorcioId)
        {
            //LLEVAR A pagosServ

            var pago = context.Pagos.Where(x => x.UnidadesFuncionales.UF == item.UF && x.Periodo == expensa.PeriodoNumerico 
                && x.UnidadesFuncionales.Consorcios.ID == consorcioId ).FirstOrDefault();

            if (pago == null)
            {
                return false;
            }

            var Coeficiente = item.Coeficiente.Value;
            var GastosExtraordinarios = Constantes.GetDecimalFromCurrency(gastosExtraordinarios);
            var ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            var TotalGastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios) + pago.ImporteGastoParticular;
            var TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;

            pago.Coeficiente = item.Coeficiente.Value;
            pago.ImportePago1 = TotalVencimiento1;
            pago.ImportePago2 = TotalVencimiento1 + 10;
            pago.ImporteExtraordinario = ImporteExtraordinario;
            pago.Periodo = expensa.PeriodoNumerico;

            context.SaveChanges();

            return true;
        }

        private void CrearPago(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios, string totalGastosOrdinarios, int periodoNumerico)
        {
            var unidadFuncional = _unidadesFuncServ.GetUnidadFuncional(consorcioId, item.UF);

            var Coeficiente = item.Coeficiente.Value;
            var GastosExtraordinarios = Constantes.GetDecimalFromCurrency(gastosExtraordinarios);
            var ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            var TotalGastosOrdinarios = Constantes.GetDecimalFromCurrency(totalGastosOrdinarios);  //CONTROLAR !!!!
            var TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;

            var pago = new Pagos()
            {
                ImporteGastoParticular = Convert.ToDecimal("0"),
                Coeficiente = item.Coeficiente.Value,
                ImportePago1 = TotalVencimiento1,
                ImportePago2 = TotalVencimiento1 + 10,
                ImporteExtraordinario = ImporteExtraordinario,
                Periodo = periodoNumerico,
                UnidadesFuncionales = context.UnidadesFuncionales.Where(x => x.Consorcios.ID == consorcioId && x.UF == item.UF).FirstOrDefault()
            };

            _pagosServ.AddPagos(pago);
        }
    }
}

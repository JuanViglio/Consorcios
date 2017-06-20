using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class expensasServ
    {
        private const int GastoTipoOrdinario = 1;
        private const int GastoTipoEventual = 2;
        private const int GastoTipoExtraordinario = 3;
        private ExpensasEntities context = new ExpensasEntities();

        public List<Expensas> GetExpensas(string IdConsorcio)
        {
            var expensas = context.Expensas.Where(x => x.Consorcios.ID == IdConsorcio).OrderByDescending(x => x.PeriodoNumerico).ToList();

            return expensas;
        }

        public decimal AgregarExpensa(string IdConsorcio)
        {
            if (context.Expensas.OrderByDescending(x => x.ID).FirstOrDefault().Estado == "Finalizado")
            {
                Expensas expensa = new Expensas();

                List<ExpensasDetalle> expensaDetalle;
                Consorcios consorcio = context.Consorcios.Where(x => x.ID == IdConsorcio).FirstOrDefault();

                expensa.Consorcios = consorcio;
                expensa.PeriodoNumerico = GetNuevoPeriodo();
                expensa.Periodo = GetDescripcionPeriodo(expensa.PeriodoNumerico.Value);
                expensa.Estado = "En Proceso";

                expensaDetalle = GetUltimoDetallePorTipo(GastoTipoOrdinario);
                foreach (var item in expensaDetalle)
                {
                    expensa.ExpensasDetalle.Add(new ExpensasDetalle { Detalle = item.Detalle, Importe = item.Importe, TipoGasto_ID = item.TipoGasto_ID });
                    expensa.Total_Gastos = +item.Importe;
                }

                context.AddToExpensas(expensa);

                context.SaveChanges();

                return context.Expensas.OrderByDescending(x => x.Periodo).FirstOrDefault().ID;
            }
            else
            {
                 return 0;
            }
        }

        private List<ExpensasDetalle> GetUltimoDetalle()
        {
            decimal idExpensa = Convert.ToDecimal(context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID.ToString());

            return context.ExpensasDetalle.Where(x => x.Expensas.ID == idExpensa).ToList();
        }

        private List<ExpensasDetalle> GetUltimoDetallePorTipo(int tipoGasto)
        {
            return GetUltimoDetalle().Where(x => x.TipoGasto_ID == tipoGasto).ToList();
        }

        private string GetDescripcionPeriodo(int Periodo)
        {
            switch (Periodo.ToString().Substring(4, 2))
            {
                case "01":
                    return "ENE " + Periodo.ToString().Substring(0, 4);

                case "02":
                    return "FEB " + Periodo.ToString().Substring(0, 4);

                case "03":
                    return "MAR " + Periodo.ToString().Substring(0, 4);

                case "04":
                    return "ABR " + Periodo.ToString().Substring(0, 4);

                case "05":
                    return "MAY " + Periodo.ToString().Substring(0, 4);

                case "06":
                    return "JUN " + Periodo.ToString().Substring(0, 4);

                case "07":
                    return "JUL " + Periodo.ToString().Substring(0, 4);

                case "08":
                    return "AGO " + Periodo.ToString().Substring(0, 4);

                case "09":
                    return "SEP " + Periodo.ToString().Substring(0, 4);

                case "10":
                    return "OCT " + Periodo.ToString().Substring(0, 4);

                case "11":
                    return "NOV " + Periodo.ToString().Substring(0, 4);

                case "12":
                    return "DIC " + Periodo.ToString().Substring(0, 4);

                default:
                    return "";

            }
        }

        private int GetNuevoPeriodo()
        {
            string periodoActual = context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().PeriodoNumerico.Value.ToString();

            if (periodoActual.Substring(4, 2) == "12")
                return (Convert.ToInt32(periodoActual.Substring(0, 4)) + 1) * 100 + 1;
            else
                return Convert.ToInt32(periodoActual) + 1;
        }

        public void AgregarExpensaDetalle(int IdExpensa, string Detalle, Decimal Importe, int TipoGasto)
        {
            ExpensasDetalle detalle = new ExpensasDetalle();

            detalle.Expensas = context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.TipoGasto_ID = TipoGasto;

            context.AddToExpensasDetalle(detalle);
            context.SaveChanges();
        }

        public void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, Decimal Importe)
        {
            ExpensasDetalle detalle = context.ExpensasDetalle.Where(x => x.ID == IdExpensaDetalle).FirstOrDefault();

            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            context.SaveChanges();
        }

        public void AgregarGastoExtraordinario(int IdExpensa, string Detalle, Decimal Importe)
        {
            GastosExtDetalle detalle = new GastosExtDetalle();

            detalle.Expensas = context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;

            context.AddToGastosExtDetalle(detalle);
            context.SaveChanges();
        }

        public List<ExpensasDetalle> GetGastosByTipo(int IdExpensa, int TipoGasto)
        {
            return context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID.Value == TipoGasto).ToList();
        }

        public List<ExpensasDetalle> GetGastosOrdinarios(int ExpensaID)
        {
            var detalle = GetGastosByTipo(ExpensaID, GastoTipoOrdinario);
            detalle.Add(new ExpensasDetalle { Detalle = "Total de Gastos Eventuales", Importe = GetTotalGastosEventuales(ExpensaID), TipoGasto_ID = GastoTipoEventual });

            return detalle;
        }

        public List<ExpensasDetalle> GetGastosEventuales(int IdExpensa)
        {
            return GetGastosByTipo(IdExpensa, GastoTipoEventual);
        }

        public List<GastosExtDetalle> GetGastosExtraordinarios(int IdExpensa)
        {
            return context.GastosExtDetalle.Where(x => x.Expensas.ID == IdExpensa).ToList();
        }

        public ExpensasDetalle GetTotalGastosExtraordinarios(int IdExpensa)
        {
            return GetGastosByTipo(IdExpensa, GastoTipoExtraordinario).FirstOrDefault();
        }

        public decimal GetUltimoTotal()
        {
            return context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().Total_Gastos.Value;
        }

        public Decimal GetTotalDetalle(int IdExpensa)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public Decimal GetTotalGastosEventuales(int IdExpensa)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa).Where(x => x.TipoGasto_ID.Value == 2).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public void GuardarUltimoTotal(int IdExpensa, Decimal Total)
        {
            var expensa = context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();

            expensa.Total_Gastos = Total;
            context.SaveChanges();
        }

        public void ActualizarGastosExtraordinario(int IdExpensa, Decimal Importe)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID == GastoTipoExtraordinario).FirstOrDefault();

            if (detalle == null)
            {

                ExpensasDetalle nuevoDetalle = new ExpensasDetalle();
                nuevoDetalle.Expensas = context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
                nuevoDetalle.Importe = Importe;
                nuevoDetalle.TipoGasto_ID = GastoTipoExtraordinario;
                nuevoDetalle.Detalle = "Prevision para gastos Extraordinarios";
                context.AddToExpensasDetalle(nuevoDetalle);
            }
            else
            {
                detalle.Importe = Importe;
            }

            context.SaveChanges();
        }

        public void CancelarExpensaUF()
        {

        }

        public void AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios)
        {
            Pagos pago;
            var consorcio = context.Consorcios.ToList();
            var uf = context.UnidadesFuncionales.ToList();

            var expensa = context.Expensas.Where(x => x.ID == expensaID).FirstOrDefault();
            expensa.Estado = "Aceptado";

            //buscar las UF de consorcio
            foreach (var item in expensa.Consorcios.UnidadesFuncionales)
            {
                //Buscar los pagos y sobreescribirlos. Si no los encuentra los creo
                pago = context.Pagos.Where(x => x.UnidadesFuncionales.UF == item.UF && x.Periodo == expensa.PeriodoNumerico).FirstOrDefault();

                if (pago == null)
                {
                    pago = new Pagos();
                    pago.UnidadesFuncionales = new UnidadesFuncionales();
                    pago.UnidadesFuncionales = context.UnidadesFuncionales.Where(x => x.UF == item.UF).FirstOrDefault();
                    pago.FechaPago1 = Convert.ToDateTime("10/06/17");
                    pago.FechaPago2 = Convert.ToDateTime("20/06/17");
                    pago.ImporteGastoParticular = Convert.ToDecimal("0");

                    Decimal Coeficiente = item.Coeficiente.Value;
                    Decimal GastosExtraordinarios = Convert.ToDecimal(gastosExtraordinarios);
                    Decimal ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
                    Decimal TotalGastosOrdinarios = Convert.ToDecimal(totalGastosOrdinarios) + pago.ImporteGastoParticular;
                    Decimal TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;

                    pago.Coeficiente = item.Coeficiente.Value;
                    pago.ImportePago1 = TotalVencimiento1;
                    pago.ImportePago2 = TotalVencimiento1 + 10;
                    pago.ImporteExtraordinario = ImporteExtraordinario;
                    pago.Periodo = expensa.PeriodoNumerico;

                    context.AddToPagos(pago);
                }
                else
                {
                    Decimal Coeficiente = item.Coeficiente.Value;
                    Decimal GastosExtraordinarios = Convert.ToDecimal(gastosExtraordinarios);
                    Decimal ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
                    Decimal TotalGastosOrdinarios = Convert.ToDecimal(totalGastosOrdinarios) + pago.ImporteGastoParticular;
                    Decimal TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;

                    pago.Coeficiente = item.Coeficiente.Value;
                    pago.ImportePago1 = TotalVencimiento1;
                    pago.ImportePago2 = TotalVencimiento1 + 10;
                    pago.ImporteExtraordinario = ImporteExtraordinario;
                    pago.Periodo = expensa.PeriodoNumerico;
                }
            }

            context.SaveChanges();
        }
    }
}

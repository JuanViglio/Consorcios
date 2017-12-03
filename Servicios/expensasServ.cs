using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Servicios.Mapper;
using WebSistemmas.Common;

namespace Servicios
{
    public class expensasServ : IExpensasServ
    {
        private const int GastoTipoOrdinario = 1;
        private const int GastoTipoEvOrdinario = 2;
        private const int GastoTipoEvExtraordinario = 3;

        private ExpensasEntities context = new ExpensasEntities();

        public List<Expensas> GetExpensas(string IdConsorcio)
        {
            var expensas = context.Expensas.Where(x => x.Consorcios.ID == IdConsorcio).OrderByDescending(x => x.PeriodoNumerico).ToList();

            return expensas;
        }

        public decimal AgregarExpensa(string IdConsorcio)
        {
            if (context.Expensas.Where(x => x.Consorcios.ID == IdConsorcio).Count() == 0)
            {
                Expensas expensa = new Expensas();
                Consorcios consorcio = context.Consorcios.Where(x => x.ID == IdConsorcio).FirstOrDefault();

                expensa.Consorcios = consorcio;
                expensa.PeriodoNumerico = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                expensa.Periodo = GetDescripcionPeriodo(expensa.PeriodoNumerico.Value);
                expensa.Estado = "En Proceso";

                AddNewExpensaDetalle(expensa);
                context.AddToExpensas(expensa);
                context.SaveChanges();

                return context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID;

            }
            else if (context.Expensas.OrderByDescending(x => x.ID).FirstOrDefault().Estado == "Finalizado")
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
                    expensa.ExpensasDetalle.Add(new ExpensasDetalle { Detalle = item.Detalle, Importe = item.Importe, TipoGasto_ID = item.TipoGasto_ID, Sumar = item.Sumar });
                    expensa.Total_Gastos = +item.Importe;
                }

                AddNewExpensaDetalle(expensa);
                context.AddToExpensas(expensa);
                context.SaveChanges();

                return context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID;
            }
            else
            {
                 return 0;
            }
        }

        public bool DeleteExpensa (int idExpensa)
        {
            try
            {
                var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == idExpensa).ToList();

                if (detalle != null)
                {
                    foreach (var item in detalle)
                    {
                        context.DeleteObject(item);
                    }                    
                }

                var extraordinarios = context.GastosExtDetalle.Where(x => x.Expensas.ID == idExpensa).ToList();

                if (extraordinarios != null)
                {
                    foreach (var item in extraordinarios)
                    {
                        context.DeleteObject(item);
                    }                    
                }

                var expensa = context.Expensas.Where(x => x.ID == idExpensa).FirstOrDefault();

                context.DeleteObject(expensa);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
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

            detalle.Expensas = context.Expensas.FirstOrDefault(x => x.ID == IdExpensa);
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.TipoGasto_ID = TipoGasto;
            detalle.Sumar = true;

            context.AddToExpensasDetalle(detalle);
            context.SaveChanges();
        }

        public void AgregarGastoEvOrdinario(int IdExpensa, string Detalle, decimal Importe, int TipoGasto)
        {
            GastosEvOrdinariosDetalle detalle = new GastosEvOrdinariosDetalle();

            detalle.Expensas = context.Expensas.FirstOrDefault(x => x.ID == IdExpensa);
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;

            context.AddToGastosEvOrdinariosDetalle(detalle);
            context.SaveChanges();
        }

        public void ModificarGastoEvOrdinario(int IdGasto, string Detalle, decimal Importe)
        {
            var gasto = context.GastosEvOrdinariosDetalle.Where(x => x.ID == IdGasto).FirstOrDefault();

            gasto.Detalle = Detalle;
            gasto.Importe = Importe;            
            context.SaveChanges();
        }

        public void ActualizarTotalGastosEvOrdinarios(decimal idExpensa)
        {
            //obtener la suma de los gastos ev. ordinarios
            var gastosEvOrdinarios = context.GastosEvOrdinariosDetalle.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe).GetValueOrDefault();

            //actualizar la tabla GastosDetalles
            var expensaDetalle = context.ExpensasDetalle.Where(x => x.Detalle == Constantes.TotalGastoEvOrdinarios && x.Expensas.ID == idExpensa).FirstOrDefault();
            expensaDetalle.Importe = gastosEvOrdinarios;

            context.SaveChanges();
        }

        public void ActualizarTotalGastosEvExtraordinarios(decimal idExpensa)
        {
            //obtener la suma de los gastos ev. ordinarios
            var gastosEvExtraordinarios = context.GastosExtDetalle.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe).GetValueOrDefault();

            //actualizar la tabla GastosDetalles
            var expensaDetalle = context.ExpensasDetalle.Where(x => x.Detalle == Constantes.TotalGastoEvExtraordinarios && x.Expensas.ID == idExpensa).FirstOrDefault();
            expensaDetalle.Importe = gastosEvExtraordinarios;

            context.SaveChanges();
        }

        public void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, decimal Importe)
        {
            ExpensasDetalle detalle = context.ExpensasDetalle.Where(x => x.ID == IdExpensaDetalle).FirstOrDefault();

            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            context.SaveChanges();
        }

        public void AgregarGastoExtraordinario(int IdExpensa, string Detalle, decimal Importe)
        {
            GastosExtDetalle detalle = new GastosExtDetalle();

            detalle.Expensas = context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;

            context.AddToGastosExtDetalle(detalle);
            context.SaveChanges();
        }

        public void ModificarGastoExtraordinario(int IdExpensaDetalle, string Detalle, decimal Importe)
        {
            var detalle = context.GastosExtDetalle.Where(x => x.ID == IdExpensaDetalle).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;

            context.SaveChanges();
        }

        public List<ExpensasDetalle> GetGastosByTipo(int IdExpensa, int TipoGasto)
        {
            return context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID.Value == TipoGasto).ToList();
        }

        public IEnumerable<GastosOrdinariosModel> GetGastosOrdinarios(int ExpensaID)
        {
            var expensasDetalles = GetGastosByTipo(ExpensaID, GastoTipoOrdinario);
            expensasDetalles.AddRange(GetGastosByTipo(ExpensaID, GastoTipoEvOrdinario));
            expensasDetalles.AddRange(GetGastosByTipo(ExpensaID, GastoTipoEvExtraordinario));

            var gastosOrdinarios = AutoMapper.MapToGastosOrdinarios(expensasDetalles);

            return gastosOrdinarios;
        }

        public List<GastosEvOrdinariosDetalle> GetGastosEvOrdinarios(int IdExpensa)
        {
            return context.GastosEvOrdinariosDetalle.Where(x => x.Expensas.ID == IdExpensa).ToList();
        }

        public List<GastosExtDetalle> GetGastosEvExtraordinarios(int IdExpensa)
        {
            return context.GastosExtDetalle.Where(x => x.Expensas.ID == IdExpensa).ToList();
        }

        public decimal GetTotalGastosExtraordinarios(int IdExpensa)
        {
            return context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa && x.Sumar == true && x.TipoGasto_ID == GastoTipoEvExtraordinario).Sum(x => x.Importe).GetValueOrDefault();
        }

        public decimal GetUltimoTotal()
        {
            return context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().Total_Gastos.Value;
        }

        public decimal GetTotalGastosOrdinarios(int idExpensa)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == idExpensa && x.Sumar == true && x.TipoGasto_ID != GastoTipoEvExtraordinario).Sum(x => x.Importe);

            return detalle ?? 0;
        }

        public decimal GetTotalGastosEvOrdinarios(int idExpensa)
        {
            var detalle = context.GastosEvOrdinariosDetalle.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public decimal GetTotalGastosEvExtraordinarios(int idExpensa)
        {
            var detalle = context.GastosExtDetalle.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public void GuardarUltimoTotal(int idExpensa, decimal total)
        {
            var expensa = context.Expensas.Where(x => x.ID == idExpensa).FirstOrDefault();

            expensa.Total_Gastos = total;
            context.SaveChanges();
        }

        public void ActualizarGastosExtraordinario(int IdExpensa, decimal Importe)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID == GastoTipoEvExtraordinario).FirstOrDefault();

            if (detalle == null)
            {
                ExpensasDetalle nuevoDetalle = new ExpensasDetalle();
                nuevoDetalle.Expensas = context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
                nuevoDetalle.Importe = Importe;
                nuevoDetalle.TipoGasto_ID = GastoTipoEvExtraordinario;
                nuevoDetalle.Detalle = Constantes.FondoPrevisionExtraordinario;
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
            try
            {
               /*var e = from exp in context.Expensas
                        select new
                        {
                            exp.Estado,
                            exp.Consorcios.UnidadesFuncionales
                        };*/

                Pagos pago;
                var expensa = context.Expensas.Where(x => x.ID == expensaID).FirstOrDefault();
                var consorcios = context.Consorcios.ToList();
                var unidadesFuncionales = context.UnidadesFuncionales.ToList();

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
                        //pago.FechaPago1 = new DateTime(17,10,06);
                        //pago.FechaPago2 = new DateTime(17,10,20);
                        pago.ImporteGastoParticular = Convert.ToDecimal("0");

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

                        context.AddToPagos(pago);
                    }
                    else
                    {
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
                    }
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar)
        {
            var detalle = context.ExpensasDetalle.FirstOrDefault(x => x.ID == idExpensaDetalle);

            if (detalle != null)
            {
                detalle.Sumar = sumar;
                context.SaveChanges();
                return true;
            }

            return false;
        }

        #region Private Methods
        private void AddNewExpensaDetalle(Expensas expensa)
        {
            //Agregar las filas de Totales a ExpensasDetalles
            expensa.ExpensasDetalle.Add(new ExpensasDetalle { Detalle = Constantes.TotalGastoEvOrdinarios, Importe = 0, TipoGasto_ID = GastoTipoEvOrdinario, Sumar = true, Orden = 1 });
            expensa.ExpensasDetalle.Add(new ExpensasDetalle { Detalle = Constantes.TotalGastoEvExtraordinarios, Importe = 0, TipoGasto_ID = GastoTipoEvExtraordinario, Sumar = true, Orden = 1 });
            expensa.ExpensasDetalle.Add(new ExpensasDetalle { Detalle = Constantes.FondoPrevisionOrdinario, Importe = 0, TipoGasto_ID = GastoTipoOrdinario, Sumar = true, Orden = 2 });
            expensa.ExpensasDetalle.Add(new ExpensasDetalle { Detalle = Constantes.FondoPrevisionExtraordinario, Importe = 0, TipoGasto_ID = GastoTipoEvExtraordinario, Sumar = true, Orden = 2 });
        }
        #endregion
    }
}

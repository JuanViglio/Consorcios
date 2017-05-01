using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class expensasServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public List<Expensas> GetExpensas(string ConsorcioId)
        {
            var expensas = context.Expensas.Where(x => x.Consorcios.ID == ConsorcioId).OrderByDescending(x => x.PeriodoNumerico).ToList();

            return expensas; 
        }

        public decimal AgregarExpensa(string ConsorcioID)
        {
            Expensas expensa = new Expensas();

            List<ExpensasDetalle> expensaDetalle ;
            Consorcio consorcio = context.Consorcios.Where(x => x.ID == ConsorcioID).FirstOrDefault();

            expensa.Consorcios = consorcio;
            expensa.Total_Gastos = GetUltimoTotal();
            expensa.PeriodoNumerico = GetNuevoPeriodo();
            expensa.Periodo = GetDescripcionPeriodo(expensa.PeriodoNumerico.Value);

            expensaDetalle = GetUltimoDetalle();
            foreach (var item in expensaDetalle)
            {
                expensa.ExpensasDetalle.Add(new ExpensasDetalle {Detalle = item.Detalle, Importe = item.Importe,TipoGasto_ID = item.TipoGasto_ID });                
            }

            context.AddToExpensas(expensa);

            context.SaveChanges();

            return context.Expensas.OrderByDescending(x => x.Periodo).FirstOrDefault().ID;
        }

        private List<ExpensasDetalle> GetUltimoDetalle()
        {
            decimal idExpensa = Convert.ToDecimal(context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID.ToString());

            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == idExpensa).ToList();

            return detalle;
        }

        private string GetDescripcionPeriodo(int Periodo)
        {
            switch (Periodo.ToString().Substring(4,2))
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

        public void AgregarExpensaDetalle(int ExpensaID, string Detalle, Decimal Importe, int TipoGasto)
        {
            ExpensasDetalle detalle = new ExpensasDetalle();

            detalle.Expensas = context.Expensas.Where(x => x.ID == ExpensaID).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.TipoGasto_ID = TipoGasto;

            context.AddToExpensasDetalle(detalle);
            context.SaveChanges();
        }
         
        public List<ExpensasDetalle> GetGastosOrdinarios(int ExpensaID)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).Where(x => x.TipoGasto_ID.Value == 1).ToList();
            detalle.Add(new ExpensasDetalle { Detalle = "Fondo de Prevision mensual", Importe = GetTotalGastosEventuales(ExpensaID), TipoGasto_ID = 2 });

            return detalle;
        }

        public List<ExpensasDetalle> GetGastosEventuales(int ExpensaID)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).Where(x => x.TipoGasto_ID.Value == 2).ToList();

            return detalle;
        }

        public decimal GetUltimoTotal()
        {
            return context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().Total_Gastos.Value;
        }

        public Decimal GetTotalDetalle(int ExpensaID)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public Decimal GetTotalGastosEventuales(int ExpensaID)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).Where(x => x.TipoGasto_ID.Value == 2).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public void GuardarUltimoTotal(int ExpensaID, Decimal Total)
        {
            var expensa = context.Expensas.Where(x => x.ID == ExpensaID).FirstOrDefault();

            expensa.Total_Gastos = Total;
            context.SaveChanges();
        }
    }
}

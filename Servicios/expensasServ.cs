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

        public void AgregarExpensa(string ConsorcioID)
        {
            Expensas expensa = new Expensas();
            Consorcio consorcio = context.Consorcios.Where(x => x.ID == ConsorcioID).FirstOrDefault();
            expensa.Consorcios = consorcio;

            expensa.PeriodoNumerico = GetNuevoPeriodo();
            expensa.Periodo = GetDescripcionPeriodo(expensa.PeriodoNumerico.Value);

            context.AddToExpensas(expensa);
            context.SaveChanges();
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

        public List<ExpensasDetalle> GetExpensaDetalle(int ExpensaID)
        {
            return context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).ToList();
        }

        public Decimal GetTotalDetalle(int ExpensaID)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).Sum(x => x.Importe);

            return detalle.Value;
        }

        public Decimal GetTotalGastosEventuales(int ExpensaID)
        {
            var detalle = context.ExpensasDetalle.Where(x => x.Expensas.ID == ExpensaID).Where(x => x.TipoGasto_ID.Value == 2).Sum(x => x.Importe);

            return detalle.Value;
        }

        public void GuardarUltimoTotal(int ExpensaID, Decimal Total)
        {
            var expensa = context.Expensas.Where(x => x.ID == ExpensaID).FirstOrDefault();

            expensa.Total_Gastos = Total;
            context.SaveChanges();
        }
    }
}

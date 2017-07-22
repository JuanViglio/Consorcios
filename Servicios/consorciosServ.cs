using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class consorciosServ : IConsorcios
    { 
        private ExpensasEntities  context = new ExpensasEntities();

        public List<Consorcios> GetConsorcios()
        {
            var consorcios = context.Consorcios.ToList();

            return consorcios;
        }

        public List<Consorcios> UpdateConsorcios(string id, string direccion, string vencimiento1, string vencimiento2, string interes)
        {
            var consorcio = context.Consorcios.Where(x => x.ID == id).FirstOrDefault();

            if (consorcio != null)
            {
                consorcio.Direccion = direccion;
                consorcio.PrimerVencimiento = Convert.ToInt32(vencimiento1);
                consorcio.SegundoVencimiento = Convert.ToInt32(vencimiento2);
                consorcio.InteresSegundoVencimiento = Convert.ToDecimal(interes);
                context.SaveChanges();
            }

            return GetConsorcios();
        }

        public List<Consorcios> AddConsorcio(string id, string direccion, string vencimiento1, string vencimiento2, string interes)
        {
            var consorcio = new Consorcios();

            try
            {
                consorcio.ID = id;
                consorcio.Direccion = direccion;
                consorcio.PrimerVencimiento = Convert.ToInt32(vencimiento1);
                consorcio.SegundoVencimiento = Convert.ToInt32(vencimiento2);
                consorcio.InteresSegundoVencimiento = Convert.ToDecimal(interes);
                context.AddToConsorcios(consorcio);
                context.SaveChanges();

                return GetConsorcios();
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

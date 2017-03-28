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

        public List<Expensas> GetExpensas(string consorcioId)
        {
            var expensas = context.Expensas.Where(x => x.Consorcios.ID == consorcioId).ToList();

            return expensas; 
        }
    }
}

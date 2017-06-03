using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class consorciosServ 
    { 
        private ExpensasEntities  context = new ExpensasEntities();

        public List<Consorcios> GetConsorcios()
        {
            var consorcios = context.Consorcios.ToList();

            return consorcios;
        }
    }
}

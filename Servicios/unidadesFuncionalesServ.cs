using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class unidadesFuncionalesServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public List<UnidadesFuncionales> GetUnidadesFuncionales(string consorciosID)
        {
            var unidadesFuncionales = context.UnidadesFuncionales.Where(x => x.Consorcios_ID == consorciosID).ToList();

            return unidadesFuncionales;
        }
    }
}

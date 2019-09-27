using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    public class ExpensasDbMock : ExpensasEntities
    {
        public List<Expensa> Expensas { get; set; }

        public ExpensasDbMock()
        {
            Expensas = new List<Expensa>();
            Expensas.Add(new DAO.Expensa { ID = 1, PeriodoNumerico = 201801 });
            Expensas.Add(new DAO.Expensa { ID = 2, PeriodoNumerico = 201802 }); 
        }
    }

    public class Expensa
    {
        public decimal ID { get; set; }

        public int? PeriodoNumerico { get; set; }

        public string Estado { get; set; }
    }
}

using DAO;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class dueñosServ
    {
        private ExpensasEntities _context;

        public dueñosServ()
        {
            _context = new ExpensasEntities();
        }

        public List<Dueños> GetDueños ()
        {
            return _context.Dueños.OrderBy(x => x.Apellido).ToList();
        }
    }
}

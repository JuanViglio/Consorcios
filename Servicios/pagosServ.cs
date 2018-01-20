using DAO;
using Servicios.Interfaces;

namespace Servicios
{
    public class pagosServ : IPagosServ
    {
        private ExpensasEntities _context;

        public pagosServ(ExpensasEntities context)
        {
            _context = context;
        }

        public void AddPagos(Pagos pago)
        {
            _context.AddToPagos(pago);
            _context.SaveChanges();
        }


    }
}

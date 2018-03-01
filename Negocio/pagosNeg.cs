using Negocio.Interfaces;
using Servicios.Interfaces;

namespace Negocio
{
    public class pagosNeg : IPagosNeg
    {
        readonly IPagosServ _pagosServ;

        public pagosNeg(IPagosServ pagosServ)
        {
            _pagosServ = pagosServ;
        }

        public void DeleteGastosEvOrdinariosUF(int IdGasto)
        {
            _pagosServ.DeleteGastosEvOrdinariosUF(IdGasto);
        }

        public void DeleteGastosEvExtUF(int IdGasto)
        {
            _pagosServ.DeleteGastosEvExtUF(IdGasto);
        }
    }
}

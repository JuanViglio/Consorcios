using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios.Interfaces
{
    public interface IGastosServ
    {
        void DeleteDetalle(decimal idExpensaDetalle);

        void DeleteGastoExtraordinario(decimal idGasto);
    }
}

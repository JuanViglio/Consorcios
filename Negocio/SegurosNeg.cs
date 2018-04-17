using DAO.Models;
using Negocio.Interfaces;
using Servicios.Interfaces;
using System.Collections.Generic;

namespace Negocio
{
    public class SegurosNeg : ISegurosNeg
    {
        readonly ISegurosServ _segurosServ;

        public SegurosNeg(ISegurosServ segurosServ)
        {
            _segurosServ = segurosServ;
        }

        public IEnumerable<SegurosModel> GetSeguros()
        {
            return _segurosServ.GetSeguros();
        }
    }
}

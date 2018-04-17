using DAO.Models;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface ISegurosNeg
    {
        IEnumerable<SegurosModel> GetSeguros();
    }
}

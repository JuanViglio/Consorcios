using DAO.Models;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface ISegurosServ
    {
        IEnumerable<SegurosModel> GetSeguros();
    }
}

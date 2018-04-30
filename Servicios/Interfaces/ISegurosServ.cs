using DAO;
using DAO.Models;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface ISegurosServ
    {
        IEnumerable<SegurosModel> GetSeguros();

        void GuardarSeguros(Seguros seguro);
    }
}

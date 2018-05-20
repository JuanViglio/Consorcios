using DAO;
using DAO.Models;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface ISegurosServ
    {
        IEnumerable<SegurosModel> GetSeguros();

        void GuardarSeguros(Seguros seguro);

        bool GetSeguroActivo(string idConsorcio, string tipo);

        SeguroActivoModel GetSeguroByConsorcio(string idConsorcio, int periodo, string tipo);
    }
}

using DAO;
using DAO.Models;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface ISegurosNeg
    {
        IEnumerable<SegurosModel> GetSeguros();

        void GuardarSeguro(SegurosModel seguroModelo, List<SeguroDetalleModel> segurosDetalleModelo);
    }
}

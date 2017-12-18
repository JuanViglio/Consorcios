using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IGastosServ
    {
        void DeleteDetalle(decimal idExpensaDetalle);

        void DeleteGastoEvOrdinario(decimal idGasto);

        void DeleteGastoEvExtraordinario(decimal idGasto);

        List<Gastos> GetDetalleGastos(int tipoGasto);

        List<GastosOrdinariosModel> GetDetalleGastosCombo(int tipoGasto);
    }
}

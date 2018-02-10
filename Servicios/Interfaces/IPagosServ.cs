using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IPagosServ
    {
        void AddPagos(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios,
            string totalGastosOrdinarios, int periodoNumerico, List<GastosEvOrdinariosDetalle> gastosEvOrd, List<ExpensasDetalle> expensaDetalle);

        bool UpdatePagos(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios,
            string totalGastosOrdinarios, int periodoNumerico);

        List<decimal> GetPagos(int periodo, string consorcioId);

        List<GastosEvOrdinariosUFDetalle> GetGastosEvOrdinariosUF(int IdPago);

        void AddGastosEvOrdinariosEFDetalle(int idPago, string detalle, decimal importe);
    }
}

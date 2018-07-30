using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IPagosServ
    {
        void AddPagos(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios,
            string totalGastosOrdinarios, int periodoNumerico, List<GastosEvOrd> gastosEvOrd, List<GastosFijos> expensaDetalle, string periodoDetalle);

        bool UpdatePagos(string consorcioId, UnidadesFuncionales item, string gastosExtraordinarios,
            string totalGastosOrdinarios, int periodoNumerico, string periodoDetalle);

        List<decimal> GetPagos(int periodo, string consorcioId);

        List<GastosParticularesOrd> GetGastosEvOrdinariosUF(int IdPago);

        List<GastosParticularesExt> GetGastosEvExtUF(int IdPago);

        void AddGastoParticularOrdinario(int idPago, string detalle, decimal importe);

        void AddGastoParticularExtraordinario(int idPago, string detalle, decimal importe);

        decimal GetTotalGastosEvOrdinariosUF(int IdPago);

        decimal GetTotalGastosEvExtUF(int IdPago);

        void DeleteGastosEvOrdinariosUF(int IdGasto);

        void DeleteGastosEvExtUF(int IdGasto);

        void ActualizarImportePago1(Pagos pagoNuevo);
    }
}

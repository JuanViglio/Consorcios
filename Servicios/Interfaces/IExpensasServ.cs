using DAO;
using System;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IExpensasServ
    {
        void AgregarExpensaDetalle(int IdExpensa, string Detalle, decimal Importe, int TipoGasto, decimal IdGasto);

        void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, decimal Importe);

        void AgregarGastoEvOrdinario(int IdExpensa, string Detalle, decimal Importe, int TipoGasto);

        void ModificarGastoEvOrdinario(int IdGasto, string Detalle, decimal Importe);

        void AgregarGastoExtraordinario(int IdExpensa, string Detalle, decimal Importe);

        void ModificarGastoExtraordinario(int IdExpensaDetalle, string Detalle, decimal Importe);

        void ActualizarTotalGastosEvOrdinarios(decimal idExpensa);

        void ActualizarTotalGastosEvExtraordinarios(decimal idExpensa);

        decimal GetTotalGastosOrdinarios(int idExpensa);

        IEnumerable<GastosOrdinariosModel> GetGastosOrdinarios(int ExpensaID, bool SoloSumarChequeada = false);

        decimal GetTotalGastosExtraordinarios(int IdExpensa);

        bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar);

        ExpensasDetalle GetExpensaDetalle(int ExpensaID, int GastoID);

        List<ExpensasDetalle> GetExpensaDetalle(int ExpensaID);

        Expensas GetUltimaExpensa(string IdConsorcio);

        void GuardarUltimoTotal(int idExpensa, decimal total);

        ExpensaModel GetDatosExpensa(decimal IdExpensa);

        bool CambiarEstadoExpensa(decimal expensaID, string estado);

        Expensas GetExpensa(decimal expensaID);

        List<UnidadesFuncionales> GetUnidadesFuncionales(string IdExpensa);

        int GetPeriodoNumerico(decimal expensaID);

        decimal AgregarExpensa(string IdConsorcio);

        List<GastosEvOrdinariosDetalle> GetGastosEvOrdinarios(int IdExpensa);

        void ActualizarTotalGastosEvOrdinariosUF(decimal idPago);

        IEnumerable<GastosOrdinariosModel> GetGastosOrdinariosUF(int PagoID);

        List<ExpensasUFDetalle> GetGastosByTipoUF(int IdPago, int TipoGasto);

        decimal GetTotalGastosOrdinariosUF(int idPago);
    }
}

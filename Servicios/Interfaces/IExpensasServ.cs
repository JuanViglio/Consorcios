using DAO;
using System;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IExpensasServ
    {
        void AgregarExpensaDetalle(int IdExpensa, string Detalle, decimal Importe, int TipoGasto, decimal IdGasto);

        void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, decimal Importe);

        void AgregarGastoEvOrdinario(int IdExpensa, string Detalle, decimal Importe);

        void ModificarGastoEvOrdinario(int IdGasto, string Detalle, decimal Importe);

        decimal AgregarGastoExtraordinario(int IdExpensa, string Detalle, decimal Importe, decimal ImporteCompra, decimal ProveedorId, decimal CtaCteId);

        void ModificarGastoExtraordinario(int IdExpensaDetalle, string Detalle, decimal Importe, decimal ImporteCompra);

        void ActualizarTotalGastosEvOrdinarios(decimal idExpensa);

        void ActualizarTotalGastosEvExtraordinarios(decimal idExpensa);

        decimal GetTotalGastosOrdinarios(int idExpensa);

        IEnumerable<GastosOrdinariosModel> GetGastosOrdinarios(int ExpensaID, bool SoloSumarChequeada = false);

        decimal GetTotalGastosExtraordinarios(int IdExpensa);

        bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar);

        GastosFijos GetExpensaDetalle(int ExpensaID, int GastoID);

        List<GastosFijos> GetExpensaDetalle(int ExpensaID);

        Expensas GetUltimaExpensa(string IdConsorcio);

        void GuardarUltimoTotal(int idExpensa, decimal total);

        ExpensaModel GetDatosExpensa(decimal IdExpensa);

        bool CambiarEstadoExpensa(decimal expensaID, string estado);

        Expensas GetExpensa(decimal expensaID);

        List<UnidadesFuncionales> GetUnidadesFuncionales(string IdExpensa);

        int GetPeriodoNumerico(decimal expensaID);

        decimal AgregarExpensa(string IdConsorcio);

        List<GastosEvOrd> GetGastosEvOrdinarios(int IdExpensa);

        decimal GetProveedorCtaCteId(string tipo, decimal GastoId);

    }
}

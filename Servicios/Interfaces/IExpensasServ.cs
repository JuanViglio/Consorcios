using DAO;
using System;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IExpensasServ
    {
        void AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios);

        void AgregarExpensaDetalle(int IdExpensa, string Detalle, Decimal Importe, int TipoGasto);

        void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, Decimal Importe);

        bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar);

        void AgregarGastoEvOrdinario(int IdExpensa, string Detalle, decimal Importe, int TipoGasto);

        void AgregarGastoExtraordinario(int IdExpensa, string Detalle, decimal Importe);

        void ActualizarTotalGastosEvOrdinarios(decimal idExpensa);

        void ActualizarTotalGastosEvExtraordinarios(decimal idExpensa);

        decimal GetTotalGastosOrdinarios(int idExpensa);

        IEnumerable<GastosOrdinariosModel> GetGastosOrdinarios(int ExpensaID);

        decimal GetTotalGastosExtraordinarios(int IdExpensa);
    }
}

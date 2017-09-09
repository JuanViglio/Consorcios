using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios.Interfaces
{
    public interface IExpensasServ
    {
        void AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios);

        void AgregarExpensaDetalle(int IdExpensa, string Detalle, Decimal Importe, int TipoGasto);

        void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, Decimal Importe);

        bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar);

        decimal GetTotalDetalle(int idExpensa);
    }
}

using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface IExpensasNeg
    {
        List<decimal> AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios);
    }
}

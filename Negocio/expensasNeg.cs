using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class expensasNeg
    {
        readonly IExpensasServ _expensasServ;
        readonly IPagosServ _pagosServ;
        private ExpensasEntities context = new ExpensasEntities();

        public expensasNeg(IExpensasServ expensasServ, IPagosServ pagosServ)
        {
            _expensasServ =  expensasServ;
            _pagosServ = pagosServ;
        }

        public List<decimal> AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios)
        {
            try
            {
                var consorcioId = _expensasServ.GetConsorcioId(expensaID);                
                var periodoNumerico = _expensasServ.GetPeriodoNumerico(expensaID);
                var unidadesFuncionales = _expensasServ.GetUnidadesFuncionales(consorcioId);

                //buscar los pagos del consorcio
                var cantPagos = _pagosServ.GetPagos(periodoNumerico, consorcioId).Count();

                foreach (var item in unidadesFuncionales)
                {
                    //Buscar los pagos y los sobreescribe. Si no los encuentra los crea

                    if (cantPagos == 0)
                        _pagosServ.AddPagos(consorcioId, item, gastosExtraordinarios, totalGastosOrdinarios, periodoNumerico);
                    else
                        _pagosServ.UpdatePagos(consorcioId, item, gastosExtraordinarios, totalGastosOrdinarios, periodoNumerico);

                }
                _expensasServ.AceptarExpensa(expensaID);

                return _pagosServ.GetPagos(periodoNumerico, consorcioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSistemmas.Common;

namespace Negocio
{
    public class expensasNeg
    {
        readonly IExpensasServ _expensasServ;
        readonly IPagosServ _pagosServ;

        public expensasNeg(IExpensasServ expensasServ, IPagosServ pagosServ)
        {
            _expensasServ =  expensasServ;
            _pagosServ = pagosServ;
        }

        public decimal AceptarExpensa(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios)
        {
            try
            {
                var cantUF = AddOrUpdatePagos(expensaID, gastosExtraordinarios, totalGastosOrdinarios);

                CambiarEstadoExpensa(expensaID, Constantes.EstadoAceptado);

                return cantUF;
            }
            catch (Exception ex)
            {
                throw ex;
            }                    
        }

        #region Metodos Privados
        private int AddOrUpdatePagos(int expensaID, string gastosExtraordinarios, string totalGastosOrdinarios)
        {
            var expensa = GetDatosExpensa(expensaID);
            var unidadesFuncionales = GetUnidadesFuncionales(expensa.ConsorcioId);
            var cantPagos = _pagosServ.GetPagos(expensa.PeriodoNumerico, expensa.ConsorcioId).Count();
            var gastosEvOrd = _expensasServ.GetGastosEvOrdinarios(expensaID);
            var gastosOrdinarios = _expensasServ.GetExpensaDetalle(expensaID);

            foreach (var item in unidadesFuncionales)
            {
                //Busca los pagos y los sobreescribe. Si no los encuentra los crea
                if (cantPagos == 0)
                    _pagosServ.AddPagos(expensa.ConsorcioId, item, gastosExtraordinarios, totalGastosOrdinarios, expensa.PeriodoNumerico, gastosEvOrd, 
                        gastosOrdinarios, expensa.PeriodoDetalle);
                else
                    _pagosServ.UpdatePagos(expensa.ConsorcioId, item, gastosExtraordinarios, totalGastosOrdinarios, expensa.PeriodoNumerico, expensa.PeriodoDetalle);
            }

            return unidadesFuncionales.Count();
        }

        private ExpensaModel GetDatosExpensa(int expensaID)
        {
            var expensa = _expensasServ.GetDatosExpensa(expensaID);

            if (expensa != null)
                return expensa;
            else
                throw new Exception("No se encontro la Expensa");
        }

        private List<UnidadesFuncionales> GetUnidadesFuncionales(string ConsorcioId)
        {
            var unidadesFuncionales = _expensasServ.GetUnidadesFuncionales(ConsorcioId);

            if (unidadesFuncionales != null)
                return unidadesFuncionales;
            else
                throw new Exception("No existen Unidades Funcionales para este Consorcio");
        }

        private void CambiarEstadoExpensa(decimal expensaID, string estado)
        {
            if (!_expensasServ.CambiarEstadoExpensa(expensaID, estado))
                throw new Exception("No se pudo actualizar el Estado de la expensa");
        }
        #endregion
    }
}

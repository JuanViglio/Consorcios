using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Negocio
{
    public class unidadesFuncionaesNeg
    {
        readonly IUnidadesServ _unidadesServ;
        readonly IPagosServ _pagosServ;
        readonly IExpensasServ _expensasServ;

        private void GuardarGastosParticulares(GridViewRow row, string importe, string detalle, string importePorUF, string tipoGasto)
        {
            var idPago = int.Parse(row.Cells[3].Text);
            var detalleTotal = " (Total $" + importe + ")";

            if (tipoGasto == "Eventual Ordinario")
                _pagosServ.AddGastoParticularOrdinario(idPago, detalle.ToUpper() + detalleTotal.ToUpper(), Convert.ToDecimal(importePorUF));
            else
                _pagosServ.AddGastoParticularExtraordinario(idPago, detalle.ToUpper() + detalleTotal.ToUpper(), Convert.ToDecimal(importePorUF));
        }

        public unidadesFuncionaesNeg (IUnidadesServ unidadesServ, IPagosServ pagosServ, IExpensasServ expensasServ)
        {
            _unidadesServ = unidadesServ;
            _pagosServ = pagosServ;
            _expensasServ = expensasServ;
        }

        public List<UnidadesFuncionalesModel> GetPagosConCochera(string idConsorcio, int periodoNumerico, bool agregarValorCochera)
        {
            return _unidadesServ.GetPagosConCochera(idConsorcio, periodoNumerico, agregarValorCochera);
        }        

        public void ActualizarGastosParticulares(GridViewRowCollection rows, string importe, string detalle, string importePorUF, string tipoGasto)
        {
            int col_Apllicar = 4;

            #region Validar
            if (detalle == "")
            {
                throw new Exception ("No se ingreso el Detalle");
            }
            else if (importe == "")
            {
                throw new Exception("No se ingreso el Importe");
            }
            else if (importePorUF == "0")
            {
                throw new Exception("No se Actualizo correctamente el Importe para las UF");
            }
            #endregion

            foreach (GridViewRow row in rows)
            {
                CheckBox chk = row.Cells[col_Apllicar].Controls[1] as CheckBox;
                if (chk != null && chk.Checked)
                {
                    GuardarGastosParticulares(row, importe, detalle, importePorUF, tipoGasto);
                }
            }
        }
    }
}

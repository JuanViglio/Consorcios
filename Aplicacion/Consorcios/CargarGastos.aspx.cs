using Servicios;
using Servicios.Interfaces;
using System;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios
{
    public partial class CargarGastos : System.Web.UI.Page
    {
        readonly IConsorciosServ _consorciosServ;
        readonly expensasServ _expensasServ;

        public CargarGastos()
        {
            _consorciosServ = new consorciosServ();
            _expensasServ = new expensasServ();
        }

        #region Private methods
        private void CargarComboConsorcios()
        {
            ddlConsorcios.DataSource = _consorciosServ.GetConsorcios();
            ddlConsorcios.DataTextField = "Direccion";
            ddlConsorcios.DataValueField = "Id";
            ddlConsorcios.DataBind();
        }

        private void CargarPeriodo()
        {
            var expensa = _expensasServ.GetUltimaExpensa(ddlConsorcios.SelectedValue);

            if (expensa != null)
            {
                lblPeriodo.Text = expensa.Periodo;
                Session["idExpensa"] = expensa.ID;
            }
            else
            {
                lblPeriodo.Text = "";
                Session["idExpensa"] = 0;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarComboConsorcios();
                txtGasto.Text = Session["NombreGasto"].ToString();
                CargarPeriodo();
            }
        }

        protected void ddlConsorcios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodo();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            #region Validar
            lblError.Text = "";

            if (lblPeriodo.Text == "")
            {
                lblError.Text = Constantes.ErrorFaltaPeriodo;
                return;
            }
            else if (!txtImporte.Text.IsNumeric())
            {
                lblError.Text = Constantes.ErrorFaltaImporte;
                return;
            }
            #endregion

            var idExpensa = Convert.ToInt32(Session["idExpensa"].ToString());
            _expensasServ.AgregarExpensaDetalle(idExpensa, txtGasto.Text.ToUpper(), Convert.ToDecimal(txtImporte.Text), Constantes.GastoTipoOrdinario);

            var total = _expensasServ.GetTotalGastosOrdinarios(idExpensa);
            _expensasServ.GuardarUltimoTotal(idExpensa, total);

            txtImporte.Text = "";
        }
    }
}
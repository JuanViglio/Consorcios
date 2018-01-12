using Servicios;
using Servicios.Interfaces;
using System;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios
{
    public partial class CargarGastos : System.Web.UI.Page
    {
        readonly IConsorciosServ _consorciosServ;
        readonly IExpensasServ _expensasServ;
        readonly IDetallesServ _detallesServ;

        public CargarGastos()
        {
            _consorciosServ = new consorciosServ();
            _expensasServ = new expensasServ();
            _detallesServ = new detallesServ();
        }

        #region Private methods
        private void CargarComboConsorcios()
        {
            ddlConsorcios.DataSource = _consorciosServ.GetConsorciosCombo();
            ddlConsorcios.DataTextField = "Direccion";
            ddlConsorcios.DataValueField = "Id";
            ddlConsorcios.DataBind();
        }

        private void CargarPeriodo()
        {
            var expensa = _expensasServ.GetUltimaExpensa(ddlConsorcios.SelectedValue);
            var idConsorcio = ddlConsorcios.SelectedValue;
            var idGasto = Convert.ToInt32(Session["IdGasto"].ToString());

            if (expensa != null)
            {
                lblPeriodo.Text = expensa.Periodo;
                Session["idExpensa"] = expensa.ID;
                txtDetalle.Text = _detallesServ.GetDetalle(idConsorcio, idGasto);

                //Get Importe de gasto ordinario previamente guardado
                var expensaDetalle = _expensasServ.GetExpensaDetalle(int.Parse(expensa.ID.ToString()),idGasto);

                if (expensaDetalle != null)
                    txtImporte.Text = expensaDetalle.Importe.Value.ToString();
                else
                    txtImporte.Text = "";
            }
            else
            {
                lblPeriodo.Text = "";
                txtDetalle.Text = "";
                Session["idExpensa"] = 0;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarComboConsorcios();
                lblGasto.Text = Session["NombreGasto"].ToString();
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
            else if (txtImporte.Text == "0")
            {
                lblError.Text = Constantes.ErrorImporteCero;
                return;
            }
            #endregion

            var idExpensa = Convert.ToInt32(Session["idExpensa"].ToString());
            var gastoDetalle = lblGasto.Text.ToUpper() + " " + txtDetalle.Text.ToUpper();
            var idGasto = Convert.ToInt32(Session["IdGasto"].ToString());

            //Consultar si el gasto esta ya guardado
            var expensaDetalle = _expensasServ.GetExpensaDetalle(idExpensa, idGasto);
            decimal importe = decimal.Parse(txtImporte.Text);

            if (expensaDetalle != null && expensaDetalle.Importe.Value != 0)
            {
                //si esta guardado actualizar el importe
                string detalle = string.Concat(lblGasto.Text, " ", txtDetalle.Text);                
                int expensaDetalleId = int.Parse(expensaDetalle.ID.ToString());

                _expensasServ.ModificarExpensaDetalle(expensaDetalleId, detalle, importe);
            }
            else
            {
                //si no esta guardado agregar un detalle nuevo
                _expensasServ.AgregarExpensaDetalle(idExpensa, gastoDetalle, importe, Constantes.GastoTipoOrdinario, idGasto);
            }

            var total = _expensasServ.GetTotalGastosOrdinarios(idExpensa);
            _expensasServ.GuardarUltimoTotal(idExpensa, total);

            #region LimpiarPantalla
            lblPeriodo.Text = "";
            txtImporte.Text = "";
            txtDetalle.Text = "";
            ddlConsorcios.SelectedIndex = 0;
            #endregion

            ClientScript.RegisterStartupScript(GetType(), "Atencion", "alert('El Gasto se guardo correctamente')", true);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gastos.aspx#gastos");
        }
    }
}
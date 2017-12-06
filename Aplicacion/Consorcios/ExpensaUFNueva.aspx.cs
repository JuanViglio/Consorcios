using Servicios;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaUFNueva : Page
    {
        private int col_ID_Expensa = 2;
        private expensasServ _expensasServ;

        public ExpensaUFNueva()
        {
            _expensasServ = new expensasServ();
        }

        private void CargarGrillaGastosOrdinarios()
        {
            gastosServ gastosServ = new gastosServ();

            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosOrdinarios.DataSource = _expensasServ.GetGastosOrdinarios(expensaID);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaID).ToString();
        }

        private void CargarGrillaGastosEventuales()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = _expensasServ.GetGastosEvOrdinarios(expensaID);
            grdGastosEventuales.DataBind();
        }

        private void CargarGrillaGastosExtraordinarios()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosExtraordinarios.DataSource = _expensasServ.GetGastosEvExtraordinarios(expensaID);
            grdGastosExtraordinarios.DataBind();
        }

        private void CalcularTotales()
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

            var PagoId = Session["PagoId"].ToString();
            var Pago = serv.GetPago(PagoId);
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            if (Pago.ImporteGastoParticular.ToString().IsNumeric())
            {
                txtImporteGastoParticular.Text = Pago.ImporteGastoParticular.ToString();
                txtDetalleGastoParticular.Text = string.IsNullOrEmpty(Pago.DetalleGastoParticular) ? "" : Pago.DetalleGastoParticular.ToString();
                lblTotalGastosOrdinarios.Text = (Convert.ToDecimal(lblTotalGastosOrdinarios.Text) + Pago.ImporteGastoParticular).ToString();                    
            }

            decimal Coeficiente = Pago.Coeficiente;
            txtCoeficiente.Text = Coeficiente.ToString();
            decimal GastosExtraordinarios = _expensasServ.GetTotalGastosExtraordinarios(expensaID);
            decimal ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            txtImporteExtraordinario.Text = ImporteExtraordinario.ToString("#.##");
            decimal TotalGastosOrdinarios = Convert.ToDecimal(lblTotalGastosOrdinarios.Text);
            decimal TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;
            txtVencimiento1.Text = TotalVencimiento1.ToString("#.##");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divError.Visible = false;

                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEventuales();
                CargarGrillaGastosExtraordinarios();

                CalcularTotales();

                if (Session["Estado"].ToString() == "Finalizado")
                {
                    txtDetalleGastoParticular.Enabled = false;
                    txtImporteGastoParticular.Enabled = false;
                    btnActualizar.Visible = false;
                }
                else
                {
                    txtDetalleGastoParticular.Enabled = true;
                    txtImporteGastoParticular.Enabled = true;
                    btnActualizar.Visible = true;
                }
            }        
        }

        protected void grdGastosOrdinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdGastosOrdinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }

        protected void grdGastosEventuales_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdGastosEventuales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdGastosExtraordinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios");
        }

        protected void btnVolver_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios");
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {            
            if (txtImporteGastoParticular.Text.IsNumeric() == false)
            {
                divError.Visible = true;
                lblError.Text = "No se ingreso un Importe correcto";
                txtImporteExtraordinario.Text = "0";
            }
            else
            {
                lblError.Text = "";
                divError.Visible = false;

                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
                int PagoId = Convert.ToInt32(Session["PagoId"].ToString());
                decimal importe = Convert.ToDecimal(txtImporteGastoParticular.Text);
                serv.GuardarGastoParticular(PagoId, importe, txtDetalleGastoParticular.Text.ToUpper());

                CargarGrillaGastosOrdinarios();
                CalcularTotales();
            }
        }
    }
}
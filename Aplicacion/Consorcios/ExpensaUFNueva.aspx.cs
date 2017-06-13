using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaUFNueva : System.Web.UI.Page
    {
        private int col_ID_Expensa = 2;

        private void CargarGrillaGastosOrdinarios()
        {
            expensasServ expensasServ = new expensasServ();
            gastosServ gastosServ = new gastosServ();

            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosOrdinarios.DataSource = expensasServ.GetGastosOrdinarios(expensaID);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = expensasServ.GetTotalDetalle(expensaID).ToString();
        }

        private void CargarGrillaGastosEventuales()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = expensasServ.GetGastosEventuales(expensaID);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = expensasServ.GetTotalGastosEventuales(expensaID).ToString();
        }

        private void CargarGrillaGastosExtraordinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            var totalGastosExtraordinarios = expensasServ.GetTotalGastosExtraordinarios(expensaID);
            txtGastosExtraordinarios.Text = totalGastosExtraordinarios == null ? "0" : totalGastosExtraordinarios.Importe.ToString();

            grdGastosExtraordinarios.DataSource = expensasServ.GetGastosExtraordinarios(expensaID);
            grdGastosExtraordinarios.DataBind();
        }

        private void CalcularTotales()
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

            var PagoId = Session["PagoId"].ToString();
            var Pago = serv.GetPago(PagoId);

            
            if (Pago.ImporteGastoParticular.ToString().IsNumeric())
            {
                txtImporteGastoParticular.Text = Pago.ImporteGastoParticular.ToString();
                txtDetalleGastoParticular.Text = string.IsNullOrEmpty(Pago.DetalleGastoParticular) ? "" : Pago.DetalleGastoParticular.ToString();
                lblTotalGastosOrdinarios.Text = (Convert.ToDecimal(lblTotalGastosOrdinarios.Text) + Pago.ImporteGastoParticular).ToString();                    
            }

            Decimal Coeficiente = Pago.Coeficiente;
            txtCoeficiente.Text = Coeficiente.ToString();
            Decimal GastosExtraordinarios = Convert.ToDecimal(txtGastosExtraordinarios.Text);
            Decimal ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            txtImporteExtraordinario.Text = ImporteExtraordinario.ToString("#.##");
            Decimal TotalGastosOrdinarios = Convert.ToDecimal(lblTotalGastosOrdinarios.Text);
            Decimal TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;
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
                serv.GuardarGastoParticular(PagoId, importe, txtDetalleGastoParticular.Text);

                CargarGrillaGastosOrdinarios();
                CalcularTotales();
            }
        }
    }
}
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

            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            grdGastosOrdinarios.DataSource = expensasServ.GetGastosOrdinarios(expensaID);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = expensasServ.GetTotalDetalle(expensaID).ToString();
        }

        private void CargarGrillaGastosEventuales()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            grdGastosEventuales.DataSource = expensasServ.GetGastosEventuales(expensaID);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = expensasServ.GetTotalGastosEventuales(expensaID).ToString();
        }

        private void CargarGrillaGastosExtraordinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            var totalGastosExtraordinarios = expensasServ.GetTotalGastosExtraordinarios(expensaID);
            txtGastosExtraordinarios.Text = totalGastosExtraordinarios == null ? "0" : totalGastosExtraordinarios.Importe.ToString();

            grdGastosExtraordinarios.DataSource = expensasServ.GetGastosExtraordinarios(expensaID);
            grdGastosExtraordinarios.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEventuales();
                CargarGrillaGastosExtraordinarios();

                Single Coeficiente = Session["Coeficiente"] == null ? 0 : Convert.ToSingle(Session["Coeficiente"]);
                txtCoeficiente.Text = Coeficiente.ToString();
                Single GastosExtraordinarios = Convert.ToSingle(txtGastosExtraordinarios.Text);
                Single ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
                txtImporteExtraordinario.Text = ImporteExtraordinario.ToString("#.##");
                Single TotalGastosOrdinarios = Convert.ToSingle(lblTotalGastosOrdinarios.Text);
                Single TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;
                txtVencimiento1.Text = TotalVencimiento1.ToString("#.##");
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
    }
}
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Web.Script.Services;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaNueva : System.Web.UI.Page
    {
        private int col_Detalle = 0;
        private int col_Importe = 1;
        private int col_ID_ExpensaDetalle = 2;

        #region Funciones Privadas

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> OnSubmit(string tipoGastoID)
        {
            gastosServ gastosServ = new Servicios.gastosServ();

            var gastos = gastosServ.GetGastos(Convert.ToInt32(tipoGastoID)).ToList();

            return gastos.Select(i => i.Detalle).ToList();
        }

        private void GuardarUltimoTotal(int expensaID, decimal total)
        {
            expensasServ expensasServ = new expensasServ();

            expensasServ.GuardarUltimoTotal(expensaID, total);
        }

        private void CargarComboTipos()
        {
            var serv = new gastosServ();

            ddlTipoGastos.DataSource = serv.GetTipoGastos();
            ddlTipoGastos.DataTextField = "Detalle";
            ddlTipoGastos.DataValueField = "Id";
            ddlTipoGastos.DataBind();
        }

        private void CargarGrillaGastosOrdinarios()
        {
            expensasServ expensasServ = new expensasServ();

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnCancelarGastoOrdinario.Enabled = false;
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEventuales();
                CargarComboTipos();                
            }
        }

        protected void grdExpensas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            if (btnAgregarGastoOrdinario.Text=="Agregar")
            {
                serv.AgregarExpensaDetalle(expensaID, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text), Convert.ToInt32(ddlTipoGastos.SelectedValue));

                CargarGrillaGastosOrdinarios();

                GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));                 
            }


            txtDetalle.Text = ""; 
            txtImporte.Text = "";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx", false);
        }

        protected void ddlTipoGastos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdGastosOrdinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;
            gastosServ gastosServ = new Servicios.gastosServ();

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;

                    decimal idExpensaDetalle; 
                    string Tipo = e.CommandName.ToUpper();
                    //lblError.Text = "";

                    switch (Tipo)
                    {
                        case "ELIMINAR":
                            idExpensaDetalle = Convert.ToDecimal(GridViewrow.Cells[col_ID_ExpensaDetalle].Text);
                            int expensaID = Convert.ToInt32(Session["idExpensa"]);

                            gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
                            break;

                        case "MODIFICAR":
                            txtDetalle.Text = GridViewrow.Cells[col_Detalle].Text;
                            txtImporte.Text = GridViewrow.Cells[col_Importe].Text;
                            btnCancelarGastoOrdinario.Enabled = true;
                            btnAgregarGastoOrdinario.Text = "Modificar";
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
            }

        }

        protected void grdGastosOrdinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_ExpensaDetalle].Visible = false;
        }

        protected void grdGastosEventuales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_ExpensaDetalle].Visible = false;
        }

        protected void grdGastosEventuales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;
            gastosServ gastosServ = new Servicios.gastosServ();

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;

                    decimal idExpensaDetalle;
                    string Tipo = e.CommandName.ToUpper();
                    //lblError.Text = "";

                    switch (Tipo)
                    {
                        case "ELIMINAR":
                            idExpensaDetalle = Convert.ToDecimal(GridViewrow.Cells[col_ID_ExpensaDetalle].Text);
                            int expensaID = Convert.ToInt32(Session["idExpensa"]);

                            gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEventuales();
                            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
                            break;
                        
                        case "MODIFICAR":
                            txtDetalleGastoEventual.Text = GridViewrow.Cells[col_Detalle].Text;
                            txtImporteGastoEventual.Text = GridViewrow.Cells[col_Importe].Text;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
            }
        }

        protected void btnAgregarGastoEventual_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.AgregarExpensaDetalle(expensaID, txtDetalleGastoEventual.Text, Convert.ToDecimal(txtImporteGastoEventual.Text), 2);

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEventuales();

            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));

            txtDetalleGastoEventual.Text = "";
            txtImporteGastoEventual.Text = "";
        }

        protected void btnCancelarGastoOrdinario_Click(object sender, EventArgs e)
        {
            btnAgregarGastoOrdinario.Text = "Agregar";
            btnCancelarGastoOrdinario.Enabled = false;

            txtDetalle.Text = "";
            txtImporte.Text = "";
        }

    }
}
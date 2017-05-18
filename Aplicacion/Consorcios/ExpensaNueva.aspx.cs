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
        private int col_ID_ExpensaDetalle = 2;
        private const int GastoTipoOrdinario = 1;
        private const int GastoTipoEventual = 2;
        private const int GastoTipoExtraordinario = 3;

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEventuales();
                CargarGrillaGastosExtraordinarios();

                if (Session["TipoGasto"] == null)
                    Session["TipoGasto"] = "Ordinario";

                if (Session["TipoGasto"].ToString() == "Eventual")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Acordion", "abrirAcordion(1)", true);
                }
                else if (Session["TipoGasto"].ToString() == "Extraordinario")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Acordion", "abrirAcordion(2)", true);
                }

            }
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

        protected void btnAgregarGastoExtraordinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.ActualizarGastosExtraordinario(expensaID, Convert.ToDecimal(txtGastosExtraordinarios.Text));
            lblTotalGastosOrdinarios.Text = serv.GetTotalDetalle(expensaID).ToString();

            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
        }

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
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

                            gastosServ.DeleteGastoExtraordinario(idExpensaDetalle);
                            CargarGrillaGastosExtraordinarios();
                            break;

                        case "MODIFICAR":
                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            { }
        }

        protected void grdGastosExtraordinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_ExpensaDetalle].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();

            serv.AceptarExpensa(Convert.ToInt32(Session["idExpensa"]));
            Response.Redirect("Expensas.aspx#consorcios");
        }

    }
}
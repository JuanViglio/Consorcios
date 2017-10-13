using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using Servicios.Interfaces;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaNueva : Page
    {
        private const int ColDetalle = 0;
        private const int ColImporte = 1;
        private const int ColIdExpensaDetalle = 2;
        private const string EstadoAceptado = "Aceptado";
        readonly IExpensasServ _expensasServ;
        readonly IGastosServ _gastosServ;

        #region Funciones Privadas
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> OnSubmit(string tipoGastoID)
        {
            gastosServ gastosServ = new gastosServ();

            var gastos = gastosServ.GetGastos(Convert.ToInt32(tipoGastoID)).ToList();

            return gastos.Select(i => i.Detalle).ToList();
        }

        private void GuardarUltimoTotal(int expensaId, decimal total)
        {
            expensasServ expensasServ = new expensasServ();

            expensasServ.GuardarUltimoTotal(expensaId, total);

            //si el estado es "Aceptado" se recalcula el total para cada UF
            if (btnAceptar.Enabled == false)
            {
                expensasServ serv = new expensasServ();

                serv.AceptarExpensa(Convert.ToInt32(Session["ExpensaId"]), txtGastosExtraordinarios.Text, lblTotalGastosOrdinarios.Text);
            }
        }

        private void CargarGrillaGastosOrdinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosOrdinarios.DataSource = expensasServ.GetGastosOrdinarios(expensaId);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = expensasServ.GetTotalDetalle(expensaId).ToString();
        }

        private void CargarGrillaGastosEventuales()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = expensasServ.GetGastosEventuales(expensaId);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = expensasServ.GetTotalGastosEventuales(expensaId).ToString();
        }

        private void CargarGrillaGastosExtraordinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            var totalGastosExtraordinarios = expensasServ.GetTotalGastosExtraordinarios(expensaId);
            txtGastosExtraordinarios.Text = totalGastosExtraordinarios == null ? "0" : totalGastosExtraordinarios.Importe.ToString();

            grdGastosExtraordinarios.DataSource = expensasServ.GetGastosExtraordinarios(expensaId);
            grdGastosExtraordinarios.DataBind();
        }

        #endregion

        public ExpensaNueva()
        {
            _expensasServ = new expensasServ();
            _gastosServ = new gastosServ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblError.Text = "";
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEventuales();
                CargarGrillaGastosExtraordinarios();
                ClientScript.RegisterStartupScript(GetType(), "TipoGastos", "cambioTipoGastos()", true);

                if (Session["Periodo"] != null)                
                    lblTitulo.Text = "Expensa de " + Session["direccionConsorcio"] + " del Periodo " + Session["Periodo"].ToString();

                if (Session["Estado"].ToString() == EstadoAceptado)
                    btnAceptar.Enabled = false;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "TipoGastos", "cambioTipoGastos();", true);
            }
        }

        protected void grdGastosOrdinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gridViewrow;

            lblError.Text = "";

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();
                    int expensaId;
                    divError.Visible = false;

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[ColIdExpensaDetalle].Text);
                            expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            GuardarUltimoTotal(expensaId, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
                            break;

                        case "MODIFICAR":
                            string detalle = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);

                            if (detalle == "Total de Gastos Eventuales")
                            {
                                divError.Visible = true;
                                lblError.Text = "No se puede Modificar el item 'Total de Gastos Eventuales'";
                            }
                            else
                            {
                                Session["idExpensaDetalle"] = gridViewrow.Cells[ColIdExpensaDetalle].Text;
                                txtDetalle.Text = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);
                                txtImporte.Text = gridViewrow.Cells[ColImporte].Text;
                                btnAgregarGastoOrdinario.Text = "Modificar";
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void grdGastosOrdinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[ColIdExpensaDetalle].Visible = false;
        }

        protected void grdGastosEventuales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[ColIdExpensaDetalle].Visible = false;
        }

        protected void grdGastosEventuales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gastosServ gastosServ = new gastosServ();

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    var gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();
                    //lblError.Text = "";

                    switch (tipo)
                    {
                        case "ELIMINAR":
                        {
                            var idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[ColIdExpensaDetalle].Text);
                            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEventuales();
                            GuardarUltimoTotal(expensaId, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
                            break;
                        }

                        case "MODIFICAR":
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnAgregarGastoExtraordinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            serv.ActualizarGastosExtraordinario(expensaId, Convert.ToDecimal(txtGastosExtraordinarios.Text));
            lblTotalGastosOrdinarios.Text = serv.GetTotalDetalle(expensaId).ToString();

            GuardarUltimoTotal(expensaId, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
        }

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gridViewrow;

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    decimal idExpensaDetalle;
                    string tipo = e.CommandName.ToUpper();
                    //lblError.Text = "";

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[ColIdExpensaDetalle].Text);

                            _gastosServ.DeleteGastoExtraordinario(idExpensaDetalle);
                            CargarGrillaGastosExtraordinarios();
                            break;

                        case "MODIFICAR":
                            break;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        protected void grdGastosExtraordinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[ColIdExpensaDetalle].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                try
                {
                    _expensasServ.AceptarExpensa(Convert.ToInt32(Session["ExpensaId"]), txtGastosExtraordinarios.Text, lblTotalGastosOrdinarios.Text);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.InnerException.Message;
                    return;
                }

                Response.Redirect("Expensas.aspx#consorcios");
            }
        }

        protected void btnAgregarGastoEventual_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            serv.AgregarExpensaDetalle(expensaId, txtDetalleGastoEventual.Text, Convert.ToDecimal(txtImporteGastoEventual.Text), 2);

            txtDetalleGastoEventual.Text = "";
            txtImporteGastoEventual.Text = "";

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEventuales();
            GuardarUltimoTotal(expensaId, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));

        }

        protected void btnAgregarGastoExt_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            serv.AgregarGastoExtraordinario(expensaId, txtDetalleGastoExtraordinario.Text, Convert.ToDecimal(txtImporteGastoExtraordinario.Text));

            txtDetalleGastoExtraordinario.Text = "";
            txtImporteGastoExtraordinario.Text = "";

            CargarGrillaGastosExtraordinarios();
        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            divError.Visible = false;

            try
            {
                int idExpensa = Convert.ToInt32(Session["ExpensaId"]);

                if (txtDetalle.Text == "")
                {
                    divError.Visible = true;
                    lblError.Text = "No se ingreso el Detalle";
                    return;
                }
                else if (!txtImporte.Text.IsNumeric())
                {
                    divError.Visible = true;
                    lblError.Text = "No se ingreso un Importe correcto";
                    return;
                }


                if (btnAgregarGastoOrdinario.Text == "Agregar")
                {
                    _expensasServ.AgregarExpensaDetalle(idExpensa, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text), 1);
                }
                else
                {
                    int idExpensaDetalle = Convert.ToInt32(Session["idExpensaDetalle"].ToString());
                    _expensasServ.ModificarExpensaDetalle(idExpensaDetalle, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text));
                    btnAgregarGastoOrdinario.Text = "Agregar";
                }

                txtDetalle.Text = "";
                txtImporte.Text = "";
                CargarGrillaGastosOrdinarios();
                GuardarUltimoTotal(expensaId, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));

            }
            catch
            {
                divError.Visible = true;
                lblError.Text = "No se pudo guardar los cambios";
            }
        }

        protected void btnCancelarGastoOrdinario_Click(object sender, EventArgs e)
        {
            txtDetalle.Text = "";
            txtImporte.Text = "";
            btnAgregarGastoOrdinario.Text = "Agregar";
            divError.Visible = false;
        }

        protected void chkSumar_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
            CheckBox chkSumar = (CheckBox)gr.FindControl("chkSumar");
            var index = gr.RowIndex;
            var idDetalle = Convert.ToInt32(grdGastosOrdinarios.Rows[index].Cells[ColIdExpensaDetalle].Text);

            _expensasServ.ActualizarCheckSumar(idDetalle, chkSumar.Checked);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalDetalle(expensaId).ToString();

            GuardarUltimoTotal(expensaId, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
        }
    }
}
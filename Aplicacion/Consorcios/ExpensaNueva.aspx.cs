﻿using Servicios;
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
using Servicios.Interfaces;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaNueva : System.Web.UI.Page
    {
        private const int col_Detalle = 0;
        private const int col_Importe = 1;
        private const int col_ID_ExpensaDetalle = 2;
        private const int GastoTipoOrdinario = 1;
        private const int GastoTipoEventual = 2;
        private const int GastoTipoExtraordinario = 3;
        private const string EstadoAceptado = "Aceptado";
        IExpensasServ _expensasServ;
        IGastosServ _gastosServ;

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

                if (Session["Estado"].ToString() == EstadoAceptado)
                {
                    btnAceptar.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TipoGastos", "cambioTipoGastos();", true);
            }

        }

        protected void grdGastosOrdinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;

            lblError.Text = "";

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;

                    decimal idExpensaDetalle;
                    string Tipo = e.CommandName.ToUpper();
                    int expensaID;
                    divError.Visible = false;

                    switch (Tipo)
                    {
                        case "ELIMINAR":
                            idExpensaDetalle = Convert.ToDecimal(GridViewrow.Cells[col_ID_ExpensaDetalle].Text);
                            expensaID = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
                            break;

                        case "MODIFICAR":
                            string Detalle = Server.HtmlDecode(GridViewrow.Cells[col_Detalle].Text);

                            if (Detalle == "Total de Gastos Eventuales")
                            {
                                divError.Visible = true;
                                lblError.Text = "No se puede Modificar el item 'Total de Gastos Eventuales'";
                            }
                            else
                            {
                                Session["idExpensaDetalle"] = GridViewrow.Cells[col_ID_ExpensaDetalle].Text;
                                txtDetalle.Text = Server.HtmlDecode(GridViewrow.Cells[col_Detalle].Text);
                                txtImporte.Text = GridViewrow.Cells[col_Importe].Text;
                                btnAgregarGastoOrdinario.Text = "Modificar";
                            }

                            break;

                        default:
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
                            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

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
                lblError.Text = ex.Message;
            }
        }

        protected void btnAgregarGastoExtraordinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            serv.ActualizarGastosExtraordinario(expensaID, Convert.ToDecimal(txtGastosExtraordinarios.Text));
            lblTotalGastosOrdinarios.Text = serv.GetTotalDetalle(expensaID).ToString();

            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));
        }

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;

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
                            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteGastoExtraordinario(idExpensaDetalle);
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

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                try
                {
                    _expensasServ.AceptarExpensa(Convert.ToInt32(Session["ExpensaId"]), txtGastosExtraordinarios.Text, lblTotalGastosOrdinarios.Text);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    return;
                }

                Response.Redirect("Expensas.aspx#consorcios");
            }
        }

        protected void btnAgregarGastoEventual_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            serv.AgregarExpensaDetalle(expensaID, txtDetalleGastoEventual.Text, Convert.ToDecimal(txtImporteGastoEventual.Text), 2);

            txtDetalleGastoEventual.Text = "";
            txtImporteGastoEventual.Text = "";

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEventuales();
            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));

        }

        protected void btnAgregarGastoExt_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            serv.AgregarGastoExtraordinario(expensaID, txtDetalleGastoExtraordinario.Text, Convert.ToDecimal(txtImporteGastoExtraordinario.Text));

            txtDetalleGastoExtraordinario.Text = "";
            txtImporteGastoExtraordinario.Text = "";

            CargarGrillaGastosExtraordinarios();
        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);
            divError.Visible = false;

            try
            {
                int IdExpensa = Convert.ToInt32(Session["ExpensaId"]);

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
                    _expensasServ.AgregarExpensaDetalle(IdExpensa, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text), 1);
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
                GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));

            }
            catch (Exception ex)
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
    }
}
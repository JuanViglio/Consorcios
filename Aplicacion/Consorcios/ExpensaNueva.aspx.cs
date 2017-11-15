using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using Servicios.Interfaces;
using System.Globalization;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaNueva : Page
    {
        private const int GastoTipoOrdinario = 1;
        private const int GastoTipoEvOrdinario = 2;
        private const int GastoTipoEvExtraordinario = 3;
        private const int ColDetalle = 0;
        private const int ColImporte = 1;
        private const int ColIdExpensaDetalle = 2;
        private const int ColModificar = 4;
        private const int ColEliminar = 5;
        private const string EstadoAceptado = "Aceptado";
        private const string ErrorFaltaDetalle = "No se ingreso el Detalle";
        private const string ErrorFaltaImporte = "No se ingreso el Importe correctamente";
        readonly IExpensasServ _expensasServ;
        readonly IGastosServ _gastosServ;

        #region Funciones Privadas
        private void GuardarUltimoTotal(int expensaId, decimal total)
        {
            expensasServ expensasServ = new expensasServ();

            expensasServ.GuardarUltimoTotal(expensaId, total);

            //si el estado es "Aceptado" se recalcula el total para cada UF
            //if (btnAceptar.Enabled == false)
            //{
            //    expensasServ serv = new expensasServ();

            //    serv.AceptarExpensa(Convert.ToInt32(Session["ExpensaId"]), txtGastosExtraordinarios.Text, lblTotalGastosOrdinarios.Text);
            //}
        }

        private void CargarGrillaGastosOrdinarios()
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosOrdinarios.DataSource = _expensasServ.GetGastosOrdinarios(expensaId);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarGrillaGastosEvOrdinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = expensasServ.GetGastosEvOrdinarios(expensaId);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = expensasServ.GetTotalGastosEvOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarGrillaGastosEvExtraordinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosExtraordinarios.DataSource = expensasServ.GetGastosEvExtraordinarios(expensaId);
            grdGastosExtraordinarios.DataBind();
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarTotalGastos()
        {
            lblTotalGastos.Text = (Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text) + Constantes.GetDecimalFromCurrency(lblTotalGastosExtraordinarios.Text)).ToString("C", new CultureInfo("en-US"));
        }
        #endregion

        public ExpensaNueva()
        {
            _expensasServ = new expensasServ();
            _gastosServ = new gastosServ();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> OnSubmit(string tipoGastoID)
        {
            gastosServ gastosServ = new gastosServ();

            var gastos = gastosServ.GetDetalleGastos(Convert.ToInt32(tipoGastoID)).ToList();

            return gastos.Select(i => i.Detalle).ToList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblError.Text = "";
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEvOrdinarios();
                CargarGrillaGastosEvExtraordinarios();
                CargarTotalGastos();
                ClientScript.RegisterStartupScript(GetType(), "TipoGastos", "cambioTipoGastos()", true);

                if (Session["Periodo"] != null)                
                    lblTitulo.Text = "Expensa de " + Session["direccionConsorcio"] + " del Periodo " + Session["Periodo"].ToString();

                if (Session["Estado"] != null && Session["Estado"].ToString() == EstadoAceptado)
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
                            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
                            CargarTotalGastos();
                            break;

                        case "MODIFICAR":
                            string detalle = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);

                            Session["idExpensaDetalle"] = gridViewrow.Cells[ColIdExpensaDetalle].Text;
                            txtDetalle.Text = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);
                            txtImporte.Text = gridViewrow.Cells[ColImporte].Text;
                            btnAgregarGastoOrdinario.Text = "Modificar";
                            CargarTotalGastos();
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

            if (e.Row.Cells[0].Text == Constantes.TotalGastoEvOrdinarios || e.Row.Cells[0].Text == Constantes.TotalGastoEvExtraordinarios)
            {
                e.Row.Cells[ColModificar].Visible = false;
                e.Row.Cells[ColEliminar].Visible = false;
            }

            if (e.Row.Cells[0].Text == Constantes.FondoPrevisionOrdinario || e.Row.Cells[0].Text == Constantes.FondoPrevisionExtraordinario)
            {
                e.Row.Cells[ColEliminar].Visible = false;
            }

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINAR"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGastoOrdinario();");

        }    

        protected void grdGastosEventuales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[ColIdExpensaDetalle].Visible = false;

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINARGASTOEVORDINARIO"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGastoOrdinario();");

        }

        protected void grdGastosEventuales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    var gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();
                    lblError.Text = "";

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[ColIdExpensaDetalle].Text);
                            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteGastoEvOrdinario(idExpensaDetalle);
                            _expensasServ.ActualizarTotalGastosEvOrdinarios(expensaId);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEvOrdinarios();
                            CargarTotalGastos();
                            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
                            break;

                        case "MODIFICAR":
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "No se pudo realizar la operacion";
            }
        }

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    var gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();
                    lblError.Text = "";

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[ColIdExpensaDetalle].Text);
                            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteGastoEvExtraordinario(idExpensaDetalle);
                            _expensasServ.ActualizarTotalGastosEvExtraordinarios(expensaId);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEvExtraordinarios();
                            CargarTotalGastos();
                            break;

                        case "MODIFICAR":
                            break;
                    }
                }
            }
            catch
            {
                lblError.Text = "No se pudo realizar la operacion";
            }
        }

        protected void grdGastosExtraordinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[ColIdExpensaDetalle].Visible = false;

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINARGASTOEVEXTRAORDINARIO"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGastoOrdinario();");

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
                    //_expensasServ.AceptarExpensa(Convert.ToInt32(Session["ExpensaId"]), txtGastosExtraordinarios.Text, lblTotalGastosOrdinarios.Text);
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
            if (txtDetalleGastoEventual.Text == "")
            {
                divError.Visible = true;
                lblError.Text = ErrorFaltaDetalle;
            }
            else if (!txtImporteGastoEventual.Text.IsNumeric())
            {
                divError.Visible = true;
                lblError.Text = ErrorFaltaImporte;
            }
            else
            {
                divError.Visible = false;
                lblError.Text = "";

                int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                _expensasServ.AgregarGastoEvOrdinario(expensaId, txtDetalleGastoEventual.Text.ToUpper(), Convert.ToDecimal(txtImporteGastoEventual.Text), GastoTipoEvOrdinario);
                _expensasServ.ActualizarTotalGastosEvOrdinarios(expensaId);

                txtDetalleGastoEventual.Text = "";
                txtImporteGastoEventual.Text = "";

                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEvOrdinarios();
                CargarTotalGastos();
                GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
            }
        }

        protected void btnAgregarGastoExt_Click(object sender, EventArgs e)
        {
            if (txtDetalleGastoExtraordinario.Text == "")
            {
                divError.Visible = true;
                lblError.Text = ErrorFaltaDetalle;
            }
            else if (!txtImporteGastoExtraordinario.Text.IsNumeric())
            {
                divError.Visible = true;
                lblError.Text = ErrorFaltaImporte;
            }
            else
            {
                divError.Visible = false;
                lblError.Text = "";

                int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                _expensasServ.AgregarGastoExtraordinario(expensaId, txtDetalleGastoExtraordinario.Text.ToUpper(), Convert.ToDecimal(txtImporteGastoExtraordinario.Text));
                _expensasServ.ActualizarTotalGastosEvExtraordinarios(expensaId);

                txtDetalleGastoExtraordinario.Text = "";
                txtImporteGastoExtraordinario.Text = "";

                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEvExtraordinarios();
                CargarTotalGastos();
            }
        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            divError.Visible = false;

            try
            {
                int idExpensa = Convert.ToInt32(Session["ExpensaId"]);

                #region Validar
                if (txtDetalle.Text == "")
                {
                    divError.Visible = true;
                    lblError.Text = ErrorFaltaDetalle;
                    return;
                }
                else if (!txtImporte.Text.IsNumeric())
                {
                    divError.Visible = true;
                    lblError.Text = ErrorFaltaImporte;
                    return;
                }
                #endregion

                if (btnAgregarGastoOrdinario.Text == "Agregar")
                {
                    _expensasServ.AgregarExpensaDetalle(idExpensa, txtDetalle.Text.ToUpper(), Convert.ToDecimal(txtImporte.Text), GastoTipoOrdinario);
                }
                else
                {
                    int idExpensaDetalle = Convert.ToInt32(Session["idExpensaDetalle"].ToString());
                    _expensasServ.ModificarExpensaDetalle(idExpensaDetalle, txtDetalle.Text.ToUpper(), Convert.ToDecimal(txtImporte.Text));
                    btnAgregarGastoOrdinario.Text = "Agregar";
                    CargarGrillaGastosEvOrdinarios();
                }

                txtDetalle.Text = "";
                txtImporte.Text = "";
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEvExtraordinarios();
                CargarTotalGastos();
                GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));

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
            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            CargarTotalGastos();
            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
        }
    }
}
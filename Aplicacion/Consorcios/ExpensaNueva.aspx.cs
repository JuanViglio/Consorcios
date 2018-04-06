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
using DAO;
using Negocio;
using Negocio.Interfaces;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaNueva : Page
    {
        private const int ColDetalle = 0;
        private const int ColImporte = 1;
        private const int ColImporteCompra = 1;
        private const int ColImporteVenta = 2;
        private const int GrdOrd_ColIdExpensaDetalle = 2;
        private const int GrdEvOrd_ColIdExpensaDetalle = 2;
        private const int GrdEvExt_ColIdExpensaDetalle = 3;
        private const int ColIdGasto = 3;
        private const int ColModificar = 5;
        private const int ColEliminar = 6;

        readonly IExpensasServ _expensasServ;
        readonly IGastosServ _gastosServ;
        readonly IDetallesServ _detallesServ;
        readonly expensasNeg _expensaNeg;
        readonly IUnidadesServ _unidadesServ;
        readonly IPagosServ _pagosServ;
        readonly IConsorciosServ _consorciosServ;
        readonly IProveedoresServ _proveedoresServ;
        readonly IProveedoresNeg _proveedoresNeg;
        private ExpensasEntities context = new ExpensasEntities();

        public ExpensaNueva()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _pagosServ = new pagosServ(context);
            _gastosServ = new gastosServ(context);
            _detallesServ = new detallesServ();
            _unidadesServ = new unidadesFuncionalesServ();
            _consorciosServ = new consorciosServ();
            _proveedoresServ = new proveedoresServ(context);

            _expensaNeg = new expensasNeg(_expensasServ, _pagosServ);
            _proveedoresNeg = new proveedoresNeg(_proveedoresServ);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> OnSubmit(string tipoGastoID)
        {
            ExpensasEntities context = new ExpensasEntities();
            gastosServ gastosServ = new gastosServ(context);

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
                CargarComboGastosOrdinarios();
                CargarComboProveedores();
                ClientScript.RegisterStartupScript(GetType(), "TipoGastos", "cambioTipoGastos()", true);

                if (Session["Periodo"] != null)                
                    lblTitulo.Text = "Expensa de " + Session["direccionConsorcio"] + " del Periodo " + Session["Periodo"];

                if (Session["Estado"] != null && Session["Estado"].ToString() == Constantes.EstadoAceptado)
                    btnAceptar.Enabled = false;

                divGastoOrdnarioNuevo.Visible = false;
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
                            var idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[GrdOrd_ColIdExpensaDetalle].Text);
                            expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
                            CargarTotalGastos();
                            break;

                        case "MODIFICAR":
                            string detalle = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);

                            Session["idExpensaDetalle"] = gridViewrow.Cells[GrdOrd_ColIdExpensaDetalle].Text;
                            txtGasto.Text = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);
                            txtImporte.Text = gridViewrow.Cells[ColImporte].Text;
                            btnAgregarGastoOrdinario.Text = "Modificar";
                            var gastoId = gridViewrow.Cells[ColIdGasto].Text;

                            if (gastoId == "0")
                            {
                                btnNuevo.Checked = true;
                                btnGuardado.Checked = false;
                                divGastoOrdnarioGuardado.Visible = false;
                                divGastoOrdnarioNuevo.Visible = true;
                            }
                            else
                            {
                                btnGuardado.Checked = true;
                                btnNuevo.Checked = false;
                                divGastoOrdnarioGuardado.Visible = true;
                                divGastoOrdnarioNuevo.Visible = false;
                                ddlGastos.SelectedValue = gastoId;

                                var idConsorcio = Session["idConsorcio"].ToString();
                                txtDetalle.Text = _detallesServ.GetDetalle(idConsorcio, decimal.Parse(gastoId));
                            }

                            btnNuevo.Enabled = false;
                            btnGuardado.Enabled = false;
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
            e.Row.Cells[GrdOrd_ColIdExpensaDetalle].Visible = false;
            e.Row.Cells[ColIdGasto].Visible = false;

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
            e.Row.Cells[GrdEvOrd_ColIdExpensaDetalle].Visible = false;

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
                            var idGasto = Convert.ToDecimal(gridViewrow.Cells[GrdEvOrd_ColIdExpensaDetalle].Text);
                            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteGastoEvOrdinario(idGasto);
                            _expensasServ.ActualizarTotalGastosEvOrdinarios(expensaId);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEvOrdinarios();
                            CargarTotalGastos();
                            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
                            break;

                        case "MODIFICAR":
                            Session["gastoEvOrdinarioId"] = Convert.ToDecimal(gridViewrow.Cells[GrdEvOrd_ColIdExpensaDetalle].Text);
                            txtDetalleGastoEventual.Text = gridViewrow.Cells[ColDetalle].Text;
                            txtImporteGastoEventual.Text = gridViewrow.Cells[ColImporte].Text;
                            btnAgregarGastoEventual.Text = "Modificar";
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
                            var idGasto = Convert.ToDecimal(gridViewrow.Cells[GrdEvExt_ColIdExpensaDetalle].Text);
                            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteGastoEvExtraordinario(idGasto);
                            _expensasServ.ActualizarTotalGastosEvExtraordinarios(expensaId);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEvExtraordinarios();
                            CargarTotalGastos();
                            break;

                        case "MODIFICAR":
                            Session["gastoEvExtraordinarioId"] = Convert.ToDecimal(gridViewrow.Cells[GrdEvExt_ColIdExpensaDetalle].Text);
                            txtDetalleGastoExtraordinario.Text = gridViewrow.Cells[ColDetalle].Text;
                            txtImporteCompraGastoExt.Text = gridViewrow.Cells[ColImporteCompra].Text;
                            txtImporteGastoExtraordinario.Text = gridViewrow.Cells[ColImporteVenta].Text;
                            btnAgregarGastoExt.Text = "Modificar";
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
            e.Row.Cells[GrdEvExt_ColIdExpensaDetalle].Visible = false;

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
                    _expensaNeg.AceptarExpensa(Convert.ToInt32(Session["ExpensaId"]), lblTotalGastosExtraordinarios.Text, lblTotalGastosOrdinarios.Text);
                }
                catch (Exception ex)
                {
                    divError.Visible = true;
                    lblError.Text = ex.Message;
                    return;
                }

                Response.Redirect("Expensas.aspx#consorcios");
            }
        }

        protected void btnAgregarGastoEventual_Click(object sender, EventArgs e)
        {
            #region Validar
            if (txtDetalleGastoEventual.Text == "")
            {
                divError.Visible = true;
                lblError.Text = Constantes.ErrorFaltaDetalle;
                return;
            }
            else if (!txtImporteGastoEventual.Text.IsNumeric())
            {
                divError.Visible = true;
                lblError.Text = Constantes.ErrorFaltaImporte;
                return;
            }
            #endregion

            divError.Visible = false;
            lblError.Text = "";

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            int gastoEvOrdinarioId = Convert.ToInt32(Session["gastoEvOrdinarioId"]);
            decimal importe = Convert.ToDecimal(txtImporteGastoEventual.Text);

            if (btnAgregarGastoEventual.Text == "Agregar")
            {
                _expensasServ.AgregarGastoEvOrdinario(expensaId, txtDetalleGastoEventual.Text.ToUpper(), importe, Constantes.GastoTipoEvOrdinario);
            }
            else
            {
                _expensasServ.ModificarGastoEvOrdinario(gastoEvOrdinarioId, txtDetalleGastoEventual.Text.ToUpper(), Convert.ToDecimal(txtImporteGastoEventual.Text));
                btnAgregarGastoEventual.Text = "Agregar";
            }

            _expensasServ.ActualizarTotalGastosEvOrdinarios(expensaId);

            txtDetalleGastoEventual.Text = "";
            txtImporteGastoEventual.Text = "";

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEvOrdinarios();
            CargarTotalGastos();
            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
            
        }

        protected void btnAgregarGastoExt_Click(object sender, EventArgs e)
        {
            #region Validar
            if (txtDetalleGastoExtraordinario.Text == "")
            {
                divError.Visible = true;
                lblError.Text = Constantes.ErrorFaltaDetalle;
                return;
            }
            else if (!txtImporteGastoExtraordinario.Text.IsNumeric())
            {
                divError.Visible = true;
                lblError.Text = Constantes.ErrorFaltaImporte;
                return;
            }
            #endregion

            divError.Visible = false;
            lblError.Text = "";

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            int gastoEvExtraordinarioId = Convert.ToInt32(Session["gastoEvExtraordinarioId"]);
            decimal importeVenta = Convert.ToDecimal(txtImporteGastoExtraordinario.Text);
            decimal importeCompra = 0;
            decimal proveedorId = Convert.ToInt32(ddlProveedores.SelectedValue);
            string detalle = txtDetalleGastoExtraordinario.Text + " - " + Session["direccionConsorcio"] + " - " + Session["Periodo"];

            if (btnAgregarGastoExt.Text == "Agregar")
            {
                if (txtImporteCompraGastoExt.Enabled == true)
                    importeCompra = Convert.ToDecimal(txtImporteCompraGastoExt.Text);
                else if (txtImporteCompraGastoExt.Text == "")
                    importeCompra = importeVenta;

                var idGasto = _expensasServ.AgregarGastoExtraordinario(expensaId, txtDetalleGastoExtraordinario.Text.ToUpper(), importeVenta, importeCompra, proveedorId);
               _proveedoresNeg.AddHaber(importeCompra, proveedorId, idGasto, "EvExt", detalle);
            }
            else
            {
                _expensasServ.ModificarGastoExtraordinario(gastoEvExtraordinarioId, txtDetalleGastoExtraordinario.Text.ToUpper(), Convert.ToDecimal(txtImporteGastoExtraordinario.Text));
                btnAgregarGastoExt.Text = "Agregar";
            }

            _expensasServ.ActualizarTotalGastosEvExtraordinarios(expensaId);
            txtDetalleGastoExtraordinario.Text = "";
            txtImporteGastoExtraordinario.Text = "";
            txtImporteCompraGastoExt.Text = "";

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEvExtraordinarios();
            CargarTotalGastos();            
        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            divError.Visible = false;

            try
            {
                int idExpensa = Convert.ToInt32(Session["ExpensaId"]);

                #region Validar
                if (btnNuevo.Checked && txtGasto.Text == "")
                {
                    divError.Visible = true;
                    lblError.Text = Constantes.ErrorFaltaDetalle;
                    return;
                }
                else if (!txtImporte.Text.IsNumeric())
                {
                    divError.Visible = true;
                    lblError.Text = Constantes.ErrorFaltaImporte;
                    return;
                }
                else if (btnGuardado.Checked && ddlGastos.SelectedValue == "0")
                {
                    divError.Visible = true;
                    lblError.Text = Constantes.ErrorFaltaGasto;
                    return;
                }
                #endregion

                if (btnAgregarGastoOrdinario.Text == "Agregar")
                {
                    AgregarGastoOrdinario(idExpensa);
                }
                else
                {
                    ModificarGastoOrdinario();
                }

                #region Limpiar Pantalla
                txtGasto.Text = "";
                txtImporte.Text = "";
                ddlGastos.SelectedIndex = 0;
                txtDetalle.Text = "";
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEvExtraordinarios();
                CargarTotalGastos();
                GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
                btnNuevo.Enabled = true;
                btnGuardado.Enabled = true;
                #endregion
            }
            catch
            {
                divError.Visible = true;
                lblError.Text = "No se pudo guardar los cambios";
            }
        }

        protected void chkSumar_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
            CheckBox chkSumar = (CheckBox)gr.FindControl("chkSumar");
            var index = gr.RowIndex;
            var idDetalle = Convert.ToInt32(grdGastosOrdinarios.Rows[index].Cells[GrdOrd_ColIdExpensaDetalle].Text);

            _expensasServ.ActualizarCheckSumar(idDetalle, chkSumar.Checked);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            CargarTotalGastos();
            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
        }

        protected void btnCancelarGastoOrdinario_Click(object sender, EventArgs e)
        {
            txtGasto.Text = "";
            txtImporte.Text = "";
            txtDetalle.Text = "";
            ddlGastos.SelectedValue  = "0";
            btnAgregarGastoOrdinario.Text = "Agregar";
            divError.Visible = false;
            btnNuevo.Enabled = true;
            btnGuardado.Enabled = true;
        }

        protected void btnCancelarGastoEvOrdinario_Click(object sender, EventArgs e)
        {
            txtDetalleGastoEventual.Text = "";
            txtImporteGastoEventual.Text = "";
            btnAgregarGastoEventual.Text = "Agregar";
            divError.Visible = false;
        }

        protected void btnCancelarGastoEvExt_Click(object sender, EventArgs e)
        {
            txtDetalleGastoExtraordinario.Text = "";
            txtImporteGastoExtraordinario.Text = "";
            txtImporteCompraGastoExt.Text = "";
            ddlProveedores.SelectedIndex = 0;
            btnAgregarGastoExt.Text = "Agregar";
            divError.Visible = false;
        }

        protected void ddlGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idConsorcio = Session["idConsorcio"].ToString();
            txtDetalle.Text = _detallesServ.GetDetalle(idConsorcio, Convert.ToDecimal(ddlGastos.SelectedValue.ToString()));
        }

        protected void btnGuardado_CheckedChanged(object sender, EventArgs e)
        {
            divGastoOrdnarioGuardado.Visible = true;
            divGastoOrdnarioNuevo.Visible = false;
        }

        protected void btnNuevo_CheckedChanged(object sender, EventArgs e)
        {
            divGastoOrdnarioGuardado.Visible = false;
            divGastoOrdnarioNuevo.Visible = true;
        }

        #region Metodos Privados
        private void GuardarUltimoTotal(int expensaId, decimal total)
        {
            expensasServ expensasServ = new expensasServ(context);

            expensasServ.GuardarUltimoTotal(expensaId, total);
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
            expensasServ expensasServ = new expensasServ(context);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = expensasServ.GetGastosEvOrdinarios(expensaId);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = expensasServ.GetTotalGastosEvOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarGrillaGastosEvExtraordinarios()
        {
            expensasServ expensasServ = new expensasServ(context);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosExtraordinarios.DataSource = expensasServ.GetGastosEvExtraordinarios(expensaId);
            grdGastosExtraordinarios.DataBind();
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarTotalGastos()
        {
            lblTotalGastos.Text = (Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text) + Constantes.GetDecimalFromCurrency(lblTotalGastosExtraordinarios.Text)).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarComboGastosOrdinarios()
        {
            ddlGastos.DataSource = _gastosServ.GetDetalleGastosCombo(Constantes.GastoTipoOrdinario);
            ddlGastos.DataTextField = "Detalle";
            ddlGastos.DataValueField = "Id";
            ddlGastos.DataBind();
        }

        private void CargarComboProveedores()
        {
            ddlProveedores.DataSource = _proveedoresNeg.GetProveedores(true);
            ddlProveedores.DataTextField = "Nombre";
            ddlProveedores.DataValueField = "Codigo";
            ddlProveedores.DataBind();
        }

        private void AgregarGastoOrdinario(int idExpensa)
        {
            string detalle;
            decimal idGasto;

            if (btnGuardado.Checked)
            {
                detalle = ddlGastos.SelectedItem.ToString() + " " + txtDetalle.Text;
                idGasto = decimal.Parse(ddlGastos.SelectedValue);
            }
            else
            {
                detalle = txtGasto.Text;
                idGasto = 0;
                //cargar el gasto en la tabla Gastos
                _gastosServ.AddGasto(Constantes.GastoTipoOrdinario, txtGasto.Text.ToUpper());
                CargarComboGastosOrdinarios();
            }

            _expensasServ.AgregarExpensaDetalle(idExpensa, detalle.ToUpper(), Convert.ToDecimal(txtImporte.Text), Constantes.GastoTipoOrdinario, idGasto);
        }

        private void ModificarGastoOrdinario()
        {
            int idExpensaDetalle = Convert.ToInt32(Session["idExpensaDetalle"].ToString());
            if (btnNuevo.Checked)
            {
                //Modificar el Gasto Nuevo
                _expensasServ.ModificarExpensaDetalle(idExpensaDetalle, txtGasto.Text.ToUpper(), Convert.ToDecimal(txtImporte.Text));
            }
            else
            {
                //Modificar el Gasto Gardado
                var detalle = ddlGastos.SelectedItem.ToString() + " " + txtDetalle.Text;
                _expensasServ.ModificarExpensaDetalle(idExpensaDetalle, detalle.ToUpper(), Convert.ToDecimal(txtImporte.Text));
            }
            btnAgregarGastoOrdinario.Text = "Agregar";
            CargarGrillaGastosEvOrdinarios();
        }

        private void GetTipoProveedor()
        {
            var tipo = _proveedoresNeg.GetTipo(decimal.Parse(ddlProveedores.SelectedValue));

            switch (tipo)
            {
                case Constantes.PrecioComprayVentaDistintos:
                    txtImporteCompraGastoExt.Text = "";
                    txtImporteCompraGastoExt.Enabled = true;
                    break;

                case Constantes.PrecioComprayVentaIguales:
                    txtImporteCompraGastoExt.Text = "";
                    txtImporteCompraGastoExt.Enabled = false;
                    break;

                default:
                    break;
            }
        }
        #endregion

        protected void ddlProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProveedores.SelectedValue == "0")
            {
                txtImporteCompraGastoExt.Text = "";
                txtImporteCompraGastoExt.Enabled = true;
            }
            else
            {
                GetTipoProveedor();
            }
        }
    }
}
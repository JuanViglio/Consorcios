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
        #region Constantes
        private const int ColDetalle = 0;
        private const int ColImporte = 1;
        private const int ColImporteCompra = 1;
        private const int ColImporteVenta = 2;
        private const int GrdOrd_ColIdExpensaDetalle = 2;
        private const int GrdEvOrd_ColIdExpensaDetalle = 3;
        private const int GrdEvExt_ColIdExpensaDetalle = 3;
        private const int ColIdGasto = 3;
        private const int ColModificar = 5;
        private const int ColEliminar = 6;
        #endregion

        #region Interfaces y Context
        readonly IExpensasServ _expensasServ;
        readonly IGastosServ _gastosServ;
        readonly IDetallesServ _detallesServ;
        readonly expensasNeg _expensaNeg;
        readonly IUnidadesServ _unidadesServ;
        readonly IPagosServ _pagosServ;
        readonly IConsorciosServ _consorciosServ;
        readonly IProveedoresServ _proveedoresServ;
        readonly IProveedoresNeg _proveedoresNeg;
        readonly ISegurosServ _segurosServ;
        readonly ISegurosNeg _segurosNeg;
        private ExpensasEntities context = new ExpensasEntities();
        #endregion

        #region Metodos Privados
        private void GuardarUltimoTotal(int expensaId, decimal total)
        {
            expensasServ expensasServ = new expensasServ(context);

            expensasServ.GuardarUltimoTotal(expensaId, total);
        }

        private void CargarGrillaGastosOrdinarios()
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosFijos.DataSource = _expensasServ.GetGastosOrdinarios(expensaId);
            grdGastosFijos.DataBind();

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

        private void CargarCombosProveedores()
        {
            ddlProveedoresEvExt.DataSource = _proveedoresNeg.GetProveedores(true);
            ddlProveedoresEvExt.DataTextField = "Nombre";
            ddlProveedoresEvExt.DataValueField = "Codigo";
            ddlProveedoresEvExt.DataBind();

            ddlProveedoresEvOrd.DataSource = _proveedoresNeg.GetProveedores(true);
            ddlProveedoresEvOrd.DataTextField = "Nombre";
            ddlProveedoresEvOrd.DataValueField = "Codigo";
            ddlProveedoresEvOrd.DataBind();
        }

        private void AgregarGastoOrdinario(int idExpensa)
        {
            string detalle;
            decimal idGasto;

            if (btnGuardado.Checked)
            {
                detalle = ddlGastos.SelectedItem.ToString() + " " + txtDetalleGastoFijo.Text;
                idGasto = decimal.Parse(ddlGastos.SelectedValue);
            }
            else
            {
                detalle = txtGastoFijo.Text;
                idGasto = 0;
                //cargar el gasto en la tabla Gastos
                _gastosServ.AddGasto(Constantes.GastoTipoOrdinario, txtGastoFijo.Text.ToUpper());
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
                _expensasServ.ModificarExpensaDetalle(idExpensaDetalle, txtGastoFijo.Text.ToUpper(), Convert.ToDecimal(txtImporte.Text));
            }
            else
            {
                //Modificar el Gasto Gardado
                var detalle = ddlGastos.SelectedItem.ToString() + " " + txtDetalleGastoFijo.Text;
                _expensasServ.ModificarExpensaDetalle(idExpensaDetalle, detalle.ToUpper(), Convert.ToDecimal(txtImporte.Text));
            }
            btnAgregarGastoOrdinario.Text = "Agregar";
            CargarGrillaGastosEvOrdinarios();
        }

        private void GetTipoProveedorEvExt()
        {
            var tipo = _proveedoresNeg.GetTipo(decimal.Parse(ddlProveedoresEvExt.SelectedValue));

            switch (tipo)
            {
                case Constantes.PrecioComprayVentaDistintos:
                    txtImporteCompraGastoEvExt.Text = "";
                    txtImporteCompraGastoEvExt.Enabled = true;
                    break;

                case Constantes.PrecioComprayVentaIguales:
                    txtImporteCompraGastoEvExt.Text = "";
                    txtImporteCompraGastoEvExt.Enabled = false;
                    break;

                default:
                    break;
            }
        }

        private void GetTipoProveedorEvOrd()
        {
            var tipo = _proveedoresNeg.GetTipo(decimal.Parse(ddlProveedoresEvOrd.SelectedValue));

            switch (tipo)
            {
                case Constantes.PrecioComprayVentaDistintos:
                    txtImporteCompraGastoEvOrd.Text = "";
                    txtImporteCompraGastoEvOrd.Enabled = true;
                    break;

                case Constantes.PrecioComprayVentaIguales:
                    txtImporteCompraGastoEvOrd.Text = "";
                    txtImporteCompraGastoEvOrd.Enabled = false;
                    break;

                default:
                    break;
            }
        }

        private void VisualizarGastoFijo(GridViewRow gridViewrow)
        {
            string detalle = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);

            Session["idExpensaDetalle"] = gridViewrow.Cells[GrdOrd_ColIdExpensaDetalle].Text;
            txtGastoFijo.Text = Server.HtmlDecode(gridViewrow.Cells[ColDetalle].Text);
            if (txtGastoFijo.Text.Contains("FONDO"))
            {
                txtGastoFijo.Enabled = false;
            }
            txtImporte.Text = gridViewrow.Cells[ColImporte].Text;
            btnAgregarGastoOrdinario.Text = "Modificar";
            var gastoId = gridViewrow.Cells[ColIdGasto].Text;

            if (!gridViewrow.Cells[3].Text.IsNumeric() || gastoId == "0")
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
                txtDetalleGastoFijo.Text = _detallesServ.GetDetalle(idConsorcio, decimal.Parse(gastoId));
            }

            btnNuevo.Enabled = false;
            btnGuardado.Enabled = false;
        }

        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> OnSubmit(string tipoGastoID)
        {
            ExpensasEntities context = new ExpensasEntities();
            gastosServ gastosServ = new gastosServ(context);

            var gastos = gastosServ.GetDetalleGastos(Convert.ToInt32(tipoGastoID)).ToList();

            return gastos.Select(i => i.Detalle).ToList();
        }

        #region Constructor y Page_Load
        public ExpensaNueva()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _pagosServ = new pagosServ(context);
            _gastosServ = new gastosServ(context);
            _detallesServ = new detallesServ();
            _unidadesServ = new unidadesFuncionalesServ();
            _consorciosServ = new consorciosServ(context);
            _proveedoresServ = new proveedoresServ(context);
            _segurosServ = new segurosServ(context);

            _segurosNeg = new segurosNeg(_segurosServ,_consorciosServ, _expensasServ);
            _expensaNeg = new expensasNeg(_expensasServ, _pagosServ);
            _proveedoresNeg = new proveedoresNeg(_proveedoresServ);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConstantesWeb.MostrarError(string.Empty, this.Page);
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEvOrdinarios();
                CargarGrillaGastosEvExtraordinarios();
                CargarTotalGastos();
                CargarComboGastosOrdinarios();
                CargarCombosProveedores();
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
        #endregion

        #region Gastos Fijos
        protected void grdGastosFijos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gridViewrow;

            ConstantesWeb.MostrarError(string.Empty, this.Page);

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();
                    int expensaId;

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
                            VisualizarGastoFijo(gridViewrow);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ConstantesWeb.MostrarError(ex.Message, this.Page);
            }
        }

        protected void grdGastosFijos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void btnNuevo_CheckedChanged(object sender, EventArgs e)
        {
            divGastoOrdnarioGuardado.Visible = false;
            divGastoOrdnarioNuevo.Visible = true;
        }

        protected void btnAgregarGastoFijo_Click(object sender, EventArgs e)
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            ConstantesWeb.MostrarError(string.Empty, this.Page);

            try
            {
                int idExpensa = Convert.ToInt32(Session["ExpensaId"]);

                #region Validar
                if (btnNuevo.Checked && txtGastoFijo.Text == "")
                {
                    ConstantesWeb.MostrarError(Constantes.ErrorFaltaDetalle, this.Page);
                    return;
                }
                else if (!txtImporte.Text.IsNumeric())
                {
                    ConstantesWeb.MostrarError(Constantes.ErrorFaltaImporte, this.Page);
                    return;
                }
                else if (btnGuardado.Checked && ddlGastos.SelectedValue == "0")
                {
                    ConstantesWeb.MostrarError(Constantes.ErrorFaltaGasto, this.Page);
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
                txtGastoFijo.Text = "";
                txtImporte.Text = "";
                ddlGastos.SelectedIndex = 0;
                txtDetalleGastoFijo.Text = "";
                txtGastoFijo.Enabled = true;
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
                ConstantesWeb.MostrarError("No se pudo guardar los cambios", this.Page);
            }
        }

        protected void btnCancelarGastoFijo_Click(object sender, EventArgs e)
        {
            txtGastoFijo.Text = "";
            txtGastoFijo.Enabled = true;
            txtImporte.Text = "";
            txtDetalleGastoFijo.Text = "";
            ddlGastos.SelectedValue = "0";
            btnAgregarGastoOrdinario.Text = "Agregar";
            ConstantesWeb.MostrarError(string.Empty, this.Page);
            btnNuevo.Enabled = true;
            btnGuardado.Enabled = true;
        }

        protected void chkSumar_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
            CheckBox chkSumar = (CheckBox)gr.FindControl("chkSumar");
            var index = gr.RowIndex;
            var idDetalle = Convert.ToInt32(grdGastosFijos.Rows[index].Cells[GrdOrd_ColIdExpensaDetalle].Text);

            _expensasServ.ActualizarCheckSumar(idDetalle, chkSumar.Checked);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            CargarTotalGastos();
            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
        }

        protected void ddlGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idConsorcio = Session["idConsorcio"].ToString();

            if (ddlGastos.SelectedItem.ToString() == "SEGURO AP" || ddlGastos.SelectedItem.ToString() == "SEGURO IC")
            {
                var seguroModel = _segurosNeg.GetSeguroByConsorcio(int.Parse(Session["ExpensaId"].ToString()), idConsorcio, ddlGastos.SelectedItem.ToString());
                txtDetalleGastoFijo.Text = seguroModel != null ? "Cuota " + seguroModel.Cuota + " de " + seguroModel.CantCuota : string.Empty;
                txtImporte.Text = seguroModel != null ? seguroModel.Importe.ToString() : string.Empty;
            }
            else
            {
                txtDetalleGastoFijo.Text = _detallesServ.GetDetalle(idConsorcio, Convert.ToDecimal(ddlGastos.SelectedValue.ToString()));
            }
        }

        protected void btnGuardado_CheckedChanged(object sender, EventArgs e)
        {
            divGastoOrdnarioGuardado.Visible = true;
            divGastoOrdnarioNuevo.Visible = false;
        }
        #endregion

        #region Gastos Ev. Ordinarios
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
                    ConstantesWeb.MostrarError(string.Empty, this.Page);

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
                            txtDetalleGastoEvOrd.Text = gridViewrow.Cells[ColDetalle].Text;
                            txtImporteCompraGastoEvOrd.Text = gridViewrow.Cells[ColImporteCompra].Text;
                            txtImporteEvOrd.Text = gridViewrow.Cells[ColImporteVenta].Text;
                            btnAgregarGastoEvOrd.Text = "Modificar";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ConstantesWeb.MostrarError("No se pudo realizar la operacion", this.Page);
            }
        }

        protected void btnAgregarGastoEvOrd_Click(object sender, EventArgs e)
        {
            #region Validar
            if (txtDetalleGastoEvOrd.Text == "")
            {

                ConstantesWeb.MostrarError(Constantes.ErrorFaltaDetalle, this.Page);
                return;
            }
            else if (!txtImporteEvOrd.Text.IsNumeric())
            {
                ConstantesWeb.MostrarError(Constantes.ErrorFaltaImporte, this.Page);
                return;
            }
            #endregion

            ConstantesWeb.MostrarError(string.Empty, this.Page);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            int gastoEvOrdinarioId = Convert.ToInt32(Session["gastoEvOrdinarioId"]);
            decimal importeVenta = Convert.ToDecimal(txtImporteEvOrd.Text);
            decimal importeCompra = 0;
            decimal proveedorId = Convert.ToInt32(ddlProveedoresEvOrd.SelectedValue);
            string detalle = txtDetalleGastoEvOrd.Text + " - " + Session["direccionConsorcio"] + " - " + Session["Periodo"];

            if (txtImporteCompraGastoEvOrd.Enabled == true)
                importeCompra = Convert.ToDecimal(txtImporteCompraGastoEvOrd.Text);
            else if (txtImporteCompraGastoEvOrd.Text == "")
                importeCompra = importeVenta;

            if (btnAgregarGastoEvOrd.Text == "Agregar")
            {
                try
                {
                    var ctaCteId = _proveedoresNeg.AddHaber(importeCompra, proveedorId, Constantes.GastoEvExt, detalle);
                    _expensasServ.AgregarGastoEvOrdinario(expensaId, txtDetalleGastoEvOrd.Text.ToUpper(), importeVenta, importeCompra, proveedorId, ctaCteId);
                }
                catch (Exception)
                {
                    ConstantesWeb.MostrarError("No se agrego el gasto en la Cta Cte del Proveedor", this.Page);
                }
            }
            else
            {
                _expensasServ.ModificarGastoEvOrdinario(gastoEvOrdinarioId, txtDetalleGastoEvOrd.Text.ToUpper(), Convert.ToDecimal(txtImporteEvOrd.Text), importeCompra);

                //get ProveedorCtaCte_id
                //delete ProveedoreCtaCte
                //agregar Haber

                btnAgregarGastoEvOrd.Text = "Agregar";
            }

            _expensasServ.ActualizarTotalGastosEvOrdinarios(expensaId);
            txtDetalleGastoEvOrd.Text = "";
            txtImporteEvOrd.Text = "";
            txtImporteCompraGastoEvOrd.Text = "";

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEvOrdinarios();
            CargarTotalGastos();
        }

        protected void btnCancelarGastoEvOrdinario_Click(object sender, EventArgs e)
        {
            txtDetalleGastoEvOrd.Text = "";
            txtImporteEvOrd.Text = "";
            btnAgregarGastoEvOrd.Text = "Agregar";
            ConstantesWeb.MostrarError(string.Empty, this.Page);
        }

        protected void ddlProveedoresEvOrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProveedoresEvOrd.SelectedValue == "0")
            {
                txtImporteCompraGastoEvOrd.Text = "";
                txtImporteCompraGastoEvOrd.Enabled = true;
            }
            else
            {
                GetTipoProveedorEvOrd();
            }
        }

        #endregion

        #region Gaastos Ev. Extraordinarios

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    var gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();                    
                    ConstantesWeb.MostrarError(string.Empty, this.Page);

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idGasto = Convert.ToDecimal(gridViewrow.Cells[GrdEvExt_ColIdExpensaDetalle].Text);
                            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _proveedoresNeg.DeleteHaber(idGasto);
                            _gastosServ.DeleteGastoEvExtraordinario(idGasto);
                            _expensasServ.ActualizarTotalGastosEvExtraordinarios(expensaId);
                            CargarGrillaGastosOrdinarios();
                            CargarGrillaGastosEvExtraordinarios();
                            CargarTotalGastos();
                            break;

                        case "MODIFICAR":
                            Session["gastoEvExtraordinarioId"] = Convert.ToDecimal(gridViewrow.Cells[GrdEvExt_ColIdExpensaDetalle].Text);
                            txtDetalleGastoEvExt.Text = gridViewrow.Cells[ColDetalle].Text;
                            txtImporteCompraGastoEvExt.Text = gridViewrow.Cells[ColImporteCompra].Text;
                            txtImporteGastoExtraordinario.Text = gridViewrow.Cells[ColImporteVenta].Text;
                            btnAgregarGastoEvExt.Text = "Modificar";
                            break;
                    }
                }
            }
            catch
            {
                ConstantesWeb.MostrarError("No se pudo realizar la operacion", this.Page);
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

        protected void btnAgregarGastoExt_Click(object sender, EventArgs e)
        {
            #region Validar
            if (txtDetalleGastoEvExt.Text == "")
            {
                ConstantesWeb.MostrarError(Constantes.ErrorFaltaDetalle, this.Page);
                return;
            }
            else if (!txtImporteGastoExtraordinario.Text.IsNumeric())
            {
                ConstantesWeb.MostrarError(Constantes.ErrorFaltaImporte, this.Page);
                return;
            }
            #endregion

            ConstantesWeb.MostrarError(string.Empty, this.Page);

            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            int gastoEvExtraordinarioId = Convert.ToInt32(Session["gastoEvExtraordinarioId"]);
            decimal importeVenta = Convert.ToDecimal(txtImporteGastoExtraordinario.Text);
            decimal importeCompra = 0;
            decimal proveedorId = Convert.ToInt32(ddlProveedoresEvExt.SelectedValue);
            string detalle = txtDetalleGastoEvExt.Text + " - " + Session["direccionConsorcio"] + " - " + Session["Periodo"];

            if (txtImporteCompraGastoEvExt.Enabled == true)
                importeCompra = Convert.ToDecimal(txtImporteCompraGastoEvExt.Text);
            else if (txtImporteCompraGastoEvExt.Text == "")
                importeCompra = importeVenta;

            if (btnAgregarGastoEvExt.Text == "Agregar")
            {
                var ctaCteId = proveedorId != 0 ? _proveedoresNeg.AddHaber(importeCompra, proveedorId, Constantes.GastoEvExt, detalle) : 0;
                _expensasServ.AgregarGastoExtraordinario(expensaId, txtDetalleGastoEvExt.Text.ToUpper(), importeVenta, importeCompra, proveedorId, ctaCteId);
            }
            else
            {
                _expensasServ.ModificarGastoExtraordinario(gastoEvExtraordinarioId, txtDetalleGastoEvExt.Text.ToUpper(), importeVenta, importeCompra);

                decimal ProveedorCtaCteId = _expensasServ.GetProveedorCtaCteId(Constantes.GastoEvExt, gastoEvExtraordinarioId);

                //delete ProveedoreCtaCte
                //agregar Haber

                btnAgregarGastoEvExt.Text = "Agregar";
            }

            _expensasServ.ActualizarTotalGastosEvExtraordinarios(expensaId);

            txtDetalleGastoEvExt.Text = "";
            txtImporteCompraGastoEvExt.Text = "";
            txtImporteGastoExtraordinario.Text = "";

            CargarGrillaGastosOrdinarios();
            CargarGrillaGastosEvExtraordinarios();
            CargarTotalGastos();
            GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
            
        }

        protected void btnCancelarGastoEvExt_Click(object sender, EventArgs e)
        {
            txtDetalleGastoEvExt.Text = "";
            txtImporteGastoExtraordinario.Text = "";
            txtImporteCompraGastoEvExt.Text = "";
            ddlProveedoresEvExt.SelectedIndex = 0;
            btnAgregarGastoEvExt.Text = "Agregar";
            ConstantesWeb.MostrarError(string.Empty, this.Page);
        }

        protected void ddlProveedoresEvExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProveedoresEvExt.SelectedValue == "0")
            {
                txtImporteCompraGastoEvExt.Text = "";
                txtImporteCompraGastoEvExt.Enabled = true;
            }
            else
            {
                GetTipoProveedorEvExt();
            }
        }
        #endregion


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
                    ConstantesWeb.MostrarError(ex.Message, this.Page);
                    return;
                }
                 
                Response.Redirect("Expensas.aspx#consorcios");
            }
        }
    }
}
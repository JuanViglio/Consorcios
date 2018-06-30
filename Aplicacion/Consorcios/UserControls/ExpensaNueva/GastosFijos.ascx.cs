using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios.UserControls.ExpensaNueva
{
    public partial class GastosFijos : System.Web.UI.UserControl
    {
        private const int ColDetalle = 0;
        private const int ColImporte = 1;
        private const int ColIdGasto = 3;
        private const int GrdOrd_ColIdExpensaDetalle = 2;
        private ExpensasEntities context = new ExpensasEntities();
        readonly IExpensasServ _expensasServ;
        readonly IGastosServ _gastosServ;
        readonly IDetallesServ _detallesServ;

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
                //CargarComboGastosOrdinarios();
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
        }
        private void CargarTotalGastos()
        {
            //lblTotalGastos.Text = (Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text) + Constantes.GetDecimalFromCurrency(lblTotalGastosExtraordinarios.Text)).ToString("C", new CultureInfo("en-US"));
        }
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

            //lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divGastoOrdnarioNuevo.Visible = false;
            }
        }

        protected void grdGastosOrdinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gridViewrow;

            //lblError.Text = "";

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    gridViewrow = (GridViewRow)imgButton.NamingContainer;

                    string tipo = e.CommandName.ToUpper();
                    int expensaId;
                    //divError.Visible = false;

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idExpensaDetalle = Convert.ToDecimal(gridViewrow.Cells[GrdOrd_ColIdExpensaDetalle].Text);
                            expensaId = Convert.ToInt32(Session["ExpensaId"]);

                            _gastosServ.DeleteDetalle(idExpensaDetalle);
                            CargarGrillaGastosOrdinarios();
                            //GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
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
                //lblError.Text = ex.Message;
            }
        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);
            //divError.Visible = false;

            try
            {
                int idExpensa = Convert.ToInt32(Session["ExpensaId"]);

                #region Validar
                if (btnNuevo.Checked && txtDetalle.Text == "")
                {
                    //divError.Visible = true;
                    //lblError.Text = Constantes.ErrorFaltaDetalle;
                    return;
                }
                else if (!txtImporte.Text.IsNumeric())
                {
                    //divError.Visible = true;
                    //lblError.Text = Constantes.ErrorFaltaImporte;
                    return;
                }
                else if (btnGuardado.Checked && ddlGastos.SelectedValue == "0")
                {
                    //divError.Visible = true;
                    //lblError.Text = Constantes.ErrorFaltaGasto;
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
                //CargarGrillaGastosOrdinarios();
                //CargarGrillaGastosEvExtraordinarios();
                CargarTotalGastos();
                //GuardarUltimoTotal(expensaId, Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text));
                btnNuevo.Enabled = true;
                btnGuardado.Enabled = true;
                #endregion
            }
            catch
            {
                //divError.Visible = true;
                //lblError.Text = "No se pudo guardar los cambios";
            }
        }

    }
}
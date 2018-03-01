using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaUFNueva : Page
    {
        private int col_ID_Expensa = 2;
        private int col_ID_GastoParticular = 2;
        private expensasServ _expensasServ;
        private gastosServ _gastosServ;
        private IPagosServ _pagosServ;
        private unidadesFuncionalesServ _unidadesFuncServ;
        private IPagosNeg _pagosNeg;

        #region Metodos Privados

        private void MostrarError(string error)
        {
            divError.Visible = (error != string.Empty);
            lblError.Text = error;
        }
        
        private void CargarGrillaGastosFijos()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);
            bool soloSumaChequeada = true;

            grdGastosOrdinarios.DataSource = _expensasServ.GetGastosOrdinarios(expensaID, soloSumaChequeada);
            grdGastosOrdinarios.DataBind();
        }

        private void CargaGrillaGastosParticularesOrd()
        {
            int pagoID = Convert.ToInt32(Session["PagoId"]);

            grdGastosParticularesOrd.DataSource = _pagosServ.GetGastosEvOrdinariosUF(pagoID);
            grdGastosParticularesOrd.DataBind();
        }

        private void CargaGrillaGastosParticularesExt()
        {
            int pagoID = Convert.ToInt32(Session["PagoId"]);

            grdGastosParticularesExt.DataSource = _pagosServ.GetGastosEvExtUF(pagoID);
            grdGastosParticularesExt.DataBind();
        }

        private void CargarGrillaGastosEvOrdinarios()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = _expensasServ.GetGastosEvOrdinarios(expensaID);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = _expensasServ.GetTotalGastosEvOrdinarios(expensaID).ToString("C", new CultureInfo("en-US"));
        }

        private void CargarGrillaGastosEvExtraordinarios()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosExtraordinarios.DataSource = _expensasServ.GetGastosEvExtraordinarios(expensaID);
            grdGastosExtraordinarios.DataBind();
        }

        private void CargarTotalesGastos()
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastos.Text = (Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text) + Constantes.GetDecimalFromCurrency(lblTotalGastosExtraordinarios.Text)).ToString("C", new CultureInfo("en-US"));
        }

        private void CalcularTotales()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);
            var PagoId = Session["PagoId"].ToString();
            var Pago = _unidadesFuncServ.GetPago(PagoId);
            decimal coeficiente = Pago.Coeficiente;
            decimal gastosOrdinarios = _expensasServ.GetTotalGastosOrdinarios(expensaID);
            decimal gastosExtraordinarios = _expensasServ.GetTotalGastosExtraordinarios(expensaID);
            decimal subtotalGastoOrdinario = gastosOrdinarios * coeficiente /100;
            decimal subtotalGastoExtraordinario = gastosExtraordinarios * coeficiente / 100;
            decimal subtotalGastoCocheraOrd = _pagosServ.GetTotalGastosEvOrdinariosUF(int.Parse(PagoId));
            decimal subtotalGastoCocheraExt = _pagosServ.GetTotalGastosEvExtUF(int.Parse(PagoId));
            decimal importeGastoParticular = Pago.ImporteGastoParticular;

            lblCoeficiente.Text = coeficiente.ToString();
            lblSubtotalGastoOrdinario.Text = subtotalGastoOrdinario.ToString("0.00");
            lblSubtotalGastoExt.Text = subtotalGastoExtraordinario.ToString("0.00");
            lblSubtotalGastoCocherarOrd.Text = subtotalGastoCocheraOrd.ToString("0.00");
            lblSubtotalGastoCocheraExt.Text = subtotalGastoCocheraExt.ToString("0.00");
            lblSubtotalGastoParicular.Text = importeGastoParticular.ToString("0.00");

            txtImporteGastoParticular.Text = importeGastoParticular.ToString("0.00");
            txtDetalleGastoParticular.Text = Pago.DetalleGastoParticular;

            lblVencimiento1.Text = (subtotalGastoOrdinario + subtotalGastoExtraordinario + subtotalGastoCocheraOrd + subtotalGastoCocheraExt + importeGastoParticular).ToString("0.00");
        }

        private void CargaInicial()
        {
            divError.Visible = false;

            CargarGrillaGastosFijos();
            CargarGrillaGastosEvOrdinarios();
            CargarGrillaGastosEvExtraordinarios();
            CargaGrillaGastosParticularesOrd();
            CargaGrillaGastosParticularesExt();
            CargarTotalesGastos();
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

        #endregion

        public ExpensaUFNueva()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _gastosServ = new gastosServ(context);
            _pagosServ = new pagosServ(context);
            _unidadesFuncServ = new unidadesFuncionalesServ();
            _pagosNeg = new pagosNeg(_pagosServ);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblNombreUF.Text = Session["NobreUF"].ToString();
                CargaInicial();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {            
            if (txtImporteGastoParticular.Text.IsNumeric() == false)
            {
                MostrarError("No se ingreso un Importe correcto");
                lblSubtotalGastoExt.Text = "0";
            }
            else
            {
                MostrarError(string.Empty);

                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
                int PagoId = Convert.ToInt32(Session["PagoId"].ToString());
                decimal importe = Convert.ToDecimal(txtImporteGastoParticular.Text);
                serv.GuardarGastoParticular(PagoId, importe, txtDetalleGastoParticular.Text.ToUpper());

                CalcularTotales();
            }
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary <decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
            string pagoID = Session["PagoId"].ToString();
            var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
            MostrarError(string.Empty);

            key++;

            if (key <= map.Count)
            {
                var pago = map.FirstOrDefault(x => x.Key == key).Value;
                Session["PagoId"] = pago.PagoId;
                lblNombreUF.Text = pago.Apellido + pago.Nombre;
                CargaInicial();
            }
            else
            {
                MostrarError("No existen mas Unidades Funcionales para mostrar");
            }            
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary<decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
            string pagoID = Session["PagoId"].ToString();
            var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
            MostrarError(string.Empty);

            key--;

            if (key > 0)
            {
                var pago = map.FirstOrDefault(x => x.Key == key).Value;
                Session["PagoId"] = pago.PagoId;
                lblNombreUF.Text = pago.Apellido + pago.Nombre;
                CargaInicial();
            }
            else
            {
                MostrarError("No existen Unidades Funcionales previas para mostrar");
            }
        }

        protected void grdGastosParticularesOrd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gridViewrow;
            MostrarError(string.Empty);

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    gridViewrow = (GridViewRow)imgButton.NamingContainer;
                    string tipo = e.CommandName.ToUpper();

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idGasto = int.Parse(gridViewrow.Cells[col_ID_GastoParticular].Text);

                            _pagosNeg.DeleteGastosEvOrdinariosUF(idGasto);

                            CargaGrillaGastosParticularesOrd();
                            CalcularTotales();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo Eliminar el Gasto Particular");
            }
        }

        protected void grdGastosParticularesOrd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("EliminarGastoOrd"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGastoOrdinario();");

        }

        protected void grdGastosParticularesExt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gridViewrow;
            MostrarError(string.Empty);

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton imgButton = (ImageButton)e.CommandSource;
                    gridViewrow = (GridViewRow)imgButton.NamingContainer;
                    string tipo = e.CommandName.ToUpper();

                    switch (tipo)
                    {
                        case "ELIMINAR":
                            var idGasto = int.Parse(gridViewrow.Cells[col_ID_GastoParticular].Text);

                            _pagosNeg.DeleteGastosEvExtUF(idGasto);

                            CargaGrillaGastosParticularesExt();
                            CalcularTotales();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MostrarError("No se pudo Eliminar el Gasto");
            }
        }

        protected void grdGastosParticularesExt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("EliminarGastoExt"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGastoOrdinario();");

        }
    }
}
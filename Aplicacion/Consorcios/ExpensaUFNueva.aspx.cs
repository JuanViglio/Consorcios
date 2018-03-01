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

        private void CargaInicial()
        {
            divError.Visible = false;

            CargaGrillaGastosParticularesOrd();
            CargaGrillaGastosParticularesExt();
            var pago = _unidadesFuncServ.GetPago(Session["PagoId"].ToString());

            totalesUF.CalcularTotales(pago);
            txtImporteGastoParticular.Text = pago.ImporteGastoParticular.ToString("0.00");
            txtDetalleGastoParticular.Text = pago.DetalleGastoParticular;


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

        protected void grdGastosExtraordinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdGastosExtraordinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {            
            if (txtImporteGastoParticular.Text.IsNumeric() == false)
            {
                MostrarError("No se ingreso un Importe correcto");
            }
            else
            {
                MostrarError(string.Empty);

                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
                int PagoId = Convert.ToInt32(Session["PagoId"].ToString());
                decimal importe = Convert.ToDecimal(txtImporteGastoParticular.Text);
                serv.GuardarGastoParticular(PagoId, importe, txtDetalleGastoParticular.Text.ToUpper());
                var pago = _unidadesFuncServ.GetPago(PagoId.ToString());

                totalesUF.CalcularTotales(pago);
                txtImporteGastoParticular.Text = pago.ImporteGastoParticular.ToString("0.00");
                txtDetalleGastoParticular.Text = pago.DetalleGastoParticular;

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
                            var pago = _unidadesFuncServ.GetPago(Session["PagoId"].ToString());

                            totalesUF.CalcularTotales(pago);
                            txtImporteGastoParticular.Text = pago.ImporteGastoParticular.ToString("0.00");
                            txtDetalleGastoParticular.Text = pago.DetalleGastoParticular;

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
                            var pago = _unidadesFuncServ.GetPago(Session["PagoId"].ToString());

                            totalesUF.CalcularTotales(pago);
                            txtImporteGastoParticular.Text = pago.ImporteGastoParticular.ToString("0.00");
                            txtDetalleGastoParticular.Text = pago.DetalleGastoParticular;
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
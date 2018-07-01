using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;
using WebSistemmas.Consorcios.UserControls;

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
            bool MostrarDatosParticulares;
            ConstantesWeb.MostrarError(string.Empty, this.Page);

            CargaGrillaGastosParticularesOrd();
            CargaGrillaGastosParticularesExt();
            var pago = _unidadesFuncServ.GetPago(Session["PagoId"].ToString());

            totalesUF.CalcularTotales(pago);
            txtImporteGastoParticular.Text = pago.ImporteGastoParticular.ToString("0.00");
            txtDetalleGastoParticular.Text = pago.DetalleGastoParticular;


            MostrarDatosParticulares = Session["Estado"].ToString() != "Finalizado";

            txtDetalleGastoParticular.Enabled = MostrarDatosParticulares;
            txtImporteGastoParticular.Enabled = MostrarDatosParticulares;
            btnActualizar.Visible = MostrarDatosParticulares;
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
                tituloPaginaID.CargarTitulo("Expensa de la Unidad Funcional de " + Session["NobreUF"].ToString());
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
                ConstantesWeb.MostrarError("No se ingreso un Importe correcto", this.Page);
            }
            else
            {
                ConstantesWeb.MostrarError(string.Empty, this.Page);

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
            try
            {
                Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary <decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
                string pagoID = Session["PagoId"].ToString();
                var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
                ConstantesWeb.MostrarError(string.Empty, this.Page);

                key++;

                if (key <= map.Count)
                {
                    var pago = map.FirstOrDefault(x => x.Key == key).Value;
                    Session["PagoId"] = pago.PagoId;
                    tituloPaginaID.CargarTitulo("Expensa de la Unidad Funcional de " + pago.Apellido + pago.Nombre);
                    CargaInicial();
                }
                else
                {
                    ConstantesWeb.MostrarError("No existen mas Unidades Funcionales para mostrar", this.Page);
                }
            }
            catch (Exception)
            {
                ConstantesWeb.MostrarError("No existen mas Unidades Funcionales para mostrar", this.Page);
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary<decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
                string pagoID = Session["PagoId"].ToString();
                var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
                ConstantesWeb.MostrarError(string.Empty, this.Page);

                key--;

                if (key > 0)
                {
                    var pago = map.FirstOrDefault(x => x.Key == key).Value;
                    Session["PagoId"] = pago.PagoId;
                    tituloPaginaID.CargarTitulo("Expensa de la Unidad Funcional de " + pago.Apellido + pago.Nombre);
                    CargaInicial();
                }
                else
                {
                    ConstantesWeb.MostrarError("No existen Unidades Funcionales previas para mostrar", this.Page);
                }
            }
            catch (Exception)
            {
                ConstantesWeb.MostrarError("No existen Unidades Funcionales previas para mostrar", this.Page);
            }
        }

        protected void grdGastosParticularesOrd_RowCommand(object sender, GridViewCommandEventArgs e)
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
                ConstantesWeb.MostrarError("No se pudo Eliminar el Gasto Particular", this.Page);
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
            ConstantesWeb.MostrarError(string.Empty, this.Page);

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
                ConstantesWeb.MostrarError("No se pudo Eliminar el Gasto", this.Page);
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
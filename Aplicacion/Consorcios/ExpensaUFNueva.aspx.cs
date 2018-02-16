using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaUFNueva : Page
    {
        private int col_ID_Expensa = 2;
        private expensasServ _expensasServ;
        private gastosServ _gastosServ;
        private IPagosServ _pagosServ;

        public ExpensaUFNueva()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
            _gastosServ = new gastosServ(context);
            _pagosServ = new pagosServ(context);
        }

        private void CargarGrillaGastosFijos()
        {

            int pagoID = Convert.ToInt32(Session["PagoId"]);

            grdGastosOrdinarios.DataSource = _expensasServ.GetGastosOrdinariosUF(pagoID);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinariosUF(pagoID).ToString();
        }

        private void CargarGrillaGastosEvOrdinarios()
        {
            var PagoId = int.Parse(Session["PagoId"].ToString());

            grdGastosEventuales.DataSource = _pagosServ.GetGastosEvOrdinariosUF(PagoId);
            grdGastosEventuales.DataBind();
        }

        private void CargarGrillaGastosEvExtraordinarios()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosExtraordinarios.DataSource = _expensasServ.GetGastosEvExtraordinarios(expensaID);
            grdGastosExtraordinarios.DataBind();
        }

        private void CalcularTotales()
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

            var PagoId = Session["PagoId"].ToString();
            var Pago = serv.GetPago(PagoId);
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            if (Pago.ImporteGastoParticular.ToString().IsNumeric())
            {
                txtImporteGastoParticular.Text = Pago.ImporteGastoParticular.ToString();
                txtDetalleGastoParticular.Text = string.IsNullOrEmpty(Pago.DetalleGastoParticular) ? "" : Pago.DetalleGastoParticular.ToString();
                lblTotalGastosOrdinarios.Text = (Convert.ToDecimal(lblTotalGastosOrdinarios.Text) + Pago.ImporteGastoParticular).ToString();                    
            }

            decimal Coeficiente = Pago.Coeficiente;
            txtCoeficiente.Text = Coeficiente.ToString();
            decimal GastosExtraordinarios = _expensasServ.GetTotalGastosExtraordinarios(expensaID);
            decimal ImporteExtraordinario = GastosExtraordinarios * Coeficiente / 100;
            txtImporteExtraordinario.Text = ImporteExtraordinario.ToString("#.##");
            decimal TotalGastosOrdinarios = Convert.ToDecimal(lblTotalGastosOrdinarios.Text);
            decimal TotalVencimiento1 = ((TotalGastosOrdinarios - GastosExtraordinarios) * Coeficiente / 100) + ImporteExtraordinario;
            txtVencimiento1.Text = TotalVencimiento1.ToString("#.##");
        }

        private void CargaInicial()
        {
            divError.Visible = false;

            CargarGrillaGastosFijos();
            CargarGrillaGastosEvOrdinarios();
            CargarGrillaGastosEvExtraordinarios();

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
                divError.Visible = true;
                lblError.Text = "No se ingreso un Importe correcto";
                txtImporteExtraordinario.Text = "0";
            }
            else
            {
                lblError.Text = "";
                divError.Visible = false;

                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
                int PagoId = Convert.ToInt32(Session["PagoId"].ToString());
                decimal importe = Convert.ToDecimal(txtImporteGastoParticular.Text);
                serv.GuardarGastoParticular(PagoId, importe, txtDetalleGastoParticular.Text.ToUpper());

                CargarGrillaGastosFijos();
                CalcularTotales();
            }
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary <decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
            string pagoID = Session["PagoId"].ToString();
            var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
            divError.Visible = false;
            lblError.Text = "";
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
                divError.Visible = true;
                lblError.Text = "No existen mas UF para mostrar";
            }            
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary<decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
            string pagoID = Session["PagoId"].ToString();
            var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
            divError.Visible = false;
            lblError.Text = "";
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
                divError.Visible = true;
                lblError.Text = "No existen mas UF para mostrar";
            }
        }
    }
}
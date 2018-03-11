using DAO;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.ExpensasUF
{
    public partial class GastosParticulares : System.Web.UI.UserControl
    {
        private unidadesFuncionalesServ _unidadesFuncServ;
        private TotalesUF totalUfUC;

        public GastosParticulares()
        {
            _unidadesFuncServ = new unidadesFuncionalesServ();
        }

        private void CargaInicial()
        {
            var pago = _unidadesFuncServ.GetPago(Session["PagoId"].ToString());

            // Find UserControl1 user control. 
            Control control = Page.FindControl("totalesUfUC");
            totalUfUC = (TotalesUF)control;
            totalUfUC.CalcularTotales(pago);

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaInicial();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {

        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary<decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];
            string pagoID = Session["PagoId"].ToString();
            var key = map.FirstOrDefault(x => x.Value.PagoId == pagoID).Key;
            //MostrarError(string.Empty);

            key++;

            if (key <= map.Count)
            {
                var pago = map.FirstOrDefault(x => x.Key == key).Value;
                Session["PagoId"] = pago.PagoId;
                //lblNombreUF.Text = pago.Apellido + pago.Nombre;
                CargaInicial();
            }
            else
            {
                //MostrarError("No existen mas Unidades Funcionales para mostrar");
            }
        }
    }
}
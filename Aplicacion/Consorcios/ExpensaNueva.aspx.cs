using Servicios;
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

namespace WebSistemmas.Consorcios
{
    public partial class ExpensaNueva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaGastosOrdinarios();
                CargarGrillaGastosEventuales();
                CargarComboTipos();                
            }
        }


        protected void grdExpensas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.AgregarExpensaDetalle(expensaID, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text), Convert.ToInt32(ddlTipoGastos.SelectedValue));

            CargarGrillaGastosOrdinarios();

            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text)); 

            txtDetalle.Text = ""; 
            txtImporte.Text = "";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }


        #region Funciones Privadas

        private void GuardarUltimoTotal(int expensaID, decimal total)
        {
            expensasServ expensasServ = new expensasServ();

            expensasServ.GuardarUltimoTotal(expensaID, total);
        }

        private void CargarComboTipos()
        {
            var serv = new gastosServ();
            
            ddlTipoGastos.DataSource = serv.GetTipoGastos();
            ddlTipoGastos.DataTextField = "Detalle";
            ddlTipoGastos.DataValueField = "Id";            
            ddlTipoGastos.DataBind();
        }

        private void CargarGrillaGastosOrdinarios()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            grdGastosOrdinarios.DataSource = expensasServ.GetExpensaDetalle(expensaID).Where(x => x.TipoGasto_ID.Value != 2);
            grdGastosOrdinarios.DataBind();

            lblTotalGastosOrdinarios.Text = expensasServ.GetTotalDetalle(expensaID).ToString();
        }
         
        private void CargarGrillaGastosEventuales()
        {
            expensasServ expensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            grdGastosEventuales.DataSource = expensasServ.GetExpensaDetalle(expensaID).Where(x => x.TipoGasto_ID.Value == 2);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = expensasServ.GetTotalGastosEventuales(expensaID).ToString();
        }

        #endregion

        protected void ddlTipoGastos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] 
        public static List<string> OnSubmit(string tipoGastoID)
        {
            gastosServ gastosServ = new Servicios.gastosServ();

            var gastos = gastosServ.GetGastos(Convert.ToInt32(tipoGastoID)).ToList();

            return gastos.Select(i => i.Detalle).ToList();
        }

        protected void grdGastosOrdinarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAgregarGastoEventual_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.AgregarExpensaDetalle(expensaID, txtDetalleGastoEventual.Text, Convert.ToDecimal(txtImporteGastoEventual.Text), 2);

            CargarGrillaGastosEventuales();

            GuardarUltimoTotal(expensaID, Convert.ToDecimal(lblTotalGastosOrdinarios.Text));

            txtDetalleGastoEventual.Text = "";
            txtImporteGastoEventual.Text = "";
        }


    }
}
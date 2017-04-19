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
                CargarGrilla();
                CargarComboTipos();
            }
        }


        protected void grdExpensas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAgregarExpensa_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.AgregarExpensaDetalle(expensaID, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text));

            CargarGrilla();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }


        #region Funciones Privadas
       
        private void CargarComboTipos()
        {
            var serv = new gastosServ();
            
            ddlTipoGastos.DataSource = serv.GetTipoGastos();
            ddlTipoGastos.DataTextField = "Detalle";
            ddlTipoGastos.DataValueField = "Id";
            
            ddlTipoGastos.DataBind();
        }

        private void CargarGrilla()
        {
            expensasServ espensasServ = new expensasServ();

            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            grdExpensas.DataSource = espensasServ.GetExpensaDetalle(expensaID);
            grdExpensas.DataBind();
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

    }
}
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Gastos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                CargarComboTipos();
                
                CargarGrillaTipos(ddlTipoGastos.SelectedValue);
            }
        }

        private void CargarComboTipos()
        {
            var serv = new gastosServ();

            ddlTipoGastos.DataSource = serv.GetTipoGastos();
            ddlTipoGastos.DataTextField = "Detalle";
            ddlTipoGastos.DataValueField = "Id";
            ddlTipoGastos.DataBind();
        }

        protected void grdGastos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        private void CargarGrillaTipos(string idTipoGasto)
        {
            var serv = new gastosServ();

            grdGastos.DataSource = serv.GetGastos(Convert.ToInt32(idTipoGasto));
            grdGastos.DataBind();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarGrillaTipos(ddlTipoGastos.SelectedValue);
        }
    }
}
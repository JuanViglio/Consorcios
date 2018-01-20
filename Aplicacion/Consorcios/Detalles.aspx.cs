using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios
{
    public partial class Detalles : System.Web.UI.Page
    {
        readonly IGastosServ _gastosServ;
        readonly IDetallesServ _detallesServ;

        public Detalles()
        {
            ExpensasEntities context = new ExpensasEntities();
            _gastosServ = new gastosServ(context);
            _detallesServ = new detallesServ();
        }

        private void CargarComboGastos()
        {
            ddlGastos.DataSource = _gastosServ.GetDetalleGastosCombo(Constantes.GastoTipoOrdinario);
            ddlGastos.DataTextField = "Detalle";
            ddlGastos.DataValueField = "Id";
            ddlGastos.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarComboGastos();
                lblConsorcio.Text = Session["addressConsorcio"].ToString();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Consorcios.aspx#consorcios", false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var idConsorcio = Session["idConsorcio"].ToString();
            _detallesServ.GuardarDetalle(txtDetalle.Text, idConsorcio, Convert.ToDecimal(ddlGastos.SelectedValue));
            txtDetalle.Text = "";
            ddlGastos.SelectedIndex = 0;

            ClientScript.RegisterStartupScript(GetType(), "Atencion", "alert('El Detalle se guardo correctamente')", true);
        }

        protected void ddlGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idConsorcio = Session["idConsorcio"].ToString();
            txtDetalle.Text = _detallesServ.GetDetalle(idConsorcio, Convert.ToDecimal(ddlGastos.SelectedValue.ToString()));
        }
    }
}
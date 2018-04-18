using System;

namespace WebSistemmas.Consorcios
{
    public partial class SeguroNuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.TituloPagina = "Seguro Nuevo";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seguros.aspx#seguros");
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {

        }
    }
}
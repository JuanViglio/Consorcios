using System;

namespace WebSistemmas.Consorcios
{
    public partial class Seguros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.TituloPagina = "Seguros";
            }
        }

        protected void NuevoSeguro_Click(object sender, EventArgs e)
        {
            Response.Redirect("SeguroNuevo.aspx#seguros");
        }
    }
}
using System;

namespace WebSistemmas.Consorcios
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.TituloPagina = "Proveedores";
            }
        }

        protected void btnNuevoProveedor_Click(object sender, EventArgs e)
        {

        }
    }
}
using System;

namespace WebSistemmas.Consorcios
{
    public partial class CtaCteProveedor : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.TituloPagina = "Cuenta Corriente del Proveedor " + Session["NombreProveedor"].ToString() ?? "" ;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedores.aspx#proveedores");
        }
    }
}
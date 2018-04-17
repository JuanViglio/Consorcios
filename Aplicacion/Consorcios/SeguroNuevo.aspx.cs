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
    }
}
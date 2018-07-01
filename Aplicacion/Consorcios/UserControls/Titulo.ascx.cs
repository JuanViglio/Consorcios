using System;

namespace WebSistemmas.Consorcios.UserControls
{
    public partial class Titulo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void CargarTitulo (string titulo)
        {
            lblTitulo.Text = titulo;
        }
    }
}
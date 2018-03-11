using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls
{
    public partial class Titulo : System.Web.UI.UserControl
    {
        public string TituloPagina { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitulo.Text = TituloPagina;
            }
        }
    }
}
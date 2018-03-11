using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls
{
    public partial class Error : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divError.Visible = false;
            }
        }

        public void MostrarError(string error)
        {
            divError.Visible = (error != string.Empty);
            lblError.Text = error;
        }
    }
}
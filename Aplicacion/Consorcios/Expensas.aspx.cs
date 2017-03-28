using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Expensas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                expensasServ serv = new expensasServ();

                grdExpensas.DataSource = serv.GetExpensas("1");
                grdExpensas.DataBind();
            }
        }

        protected void grdExpensas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
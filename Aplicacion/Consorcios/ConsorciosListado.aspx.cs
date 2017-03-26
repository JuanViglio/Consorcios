using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class ConsorciosListado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["Usuario"] == null)
                //{
                //    Response.Redirect("LoginConsorcios.aspx");
                //    return;
                //}

                consorciosServ serv = new consorciosServ();

                grdConsorcios.DataSource = serv.GetConsorcios();
                grdConsorcios.DataBind();
            }
        }

        protected void grdConsorcios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
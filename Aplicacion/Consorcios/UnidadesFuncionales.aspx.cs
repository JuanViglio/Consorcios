using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class UnidadesFuncionales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

                grdUnidades.DataSource = serv.GetUnidadesFuncionales("1");
                grdUnidades.DataBind();
            }
        }


        protected void grdUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        protected void btnNuevaUnidad_Click(object sender, EventArgs e)
        {

        }
    }
}
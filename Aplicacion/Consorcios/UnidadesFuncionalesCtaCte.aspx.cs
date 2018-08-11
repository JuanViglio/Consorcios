using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class UnidadesFuncionalesCtaCte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.CargarTitulo("Cuenta Corriente de la Unidad Funcional " + Session["numeroUF"].ToString() + " de " + Session["dueñoUF"].ToString());
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("UnidadesFuncionales.aspx#consorcios");
        }

        protected void btnVerDeuda_Click(object sender, EventArgs e)
        {
            Response.Redirect("UnidadesFuncionalesDeuda.aspx#consorcios");
        }
    }
}
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class GastosExtraordinario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregarGastoextraordinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.AgregarGastoExtraordinario(expensaID, txtDetalle.Text , Convert.ToDecimal(txtImporte.Text));

            Session["TipoGasto"] = "Extraordinario";
            Response.Redirect("ExpensaNueva.aspx#consorcios");
        }

        protected void btnCancelarGastoExtraordinario_Click(object sender, EventArgs e)
        {
            Session["TipoGasto"] = "Extraordinario";
            Response.Redirect("ExpensaNueva.aspx#consorcios");
        }
    }
}
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class GastoEventual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregarGastoEventual_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            serv.AgregarExpensaDetalle(expensaID, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text), 2);

            Session["TipoGasto"] = "Eventual";
            Response.Redirect("ExpensaNueva.aspx#consorcios");
        }

        protected void btnCancelarGastoEventual_Click(object sender, EventArgs e)
        {
            Session["TipoGasto"] = "Eventual";
            Response.Redirect("ExpensaNueva.aspx#consorcios");
        }
    }
}
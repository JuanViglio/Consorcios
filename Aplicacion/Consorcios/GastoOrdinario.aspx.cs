using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class GastoOrdinario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterStartupScript(GetType(), "TipoGastos", "cambioTipoGastos()", true);
            }
        }

        protected void btnAgregarGastoOrdinario_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();
            int expensaID = Convert.ToInt32(Session["idExpensa"]);

            if (btnAgregarGastoOrdinario.Text == "Agregar")
            {
                serv.AgregarExpensaDetalle(expensaID, txtDetalle.Text, Convert.ToDecimal(txtImporte.Text), 1);
            }

            Session["TipoGasto"] = "Ordinario";
            Response.Redirect("ExpensaNueva.aspx#consorcios");
        }

        protected void btnCancelarGastoOrdinario_Click(object sender, EventArgs e)
        {
            Session["TipoGasto"] = "Ordinario";
            Response.Redirect("ExpensaNueva.aspx#consorcios");
        }
    }
}
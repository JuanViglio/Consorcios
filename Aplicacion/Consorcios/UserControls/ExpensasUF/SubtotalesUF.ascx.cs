using DAO;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios.UserControls.ExpensasUF
{
    public partial class SubtotalesUF : System.Web.UI.UserControl
    {
        private expensasServ _expensasServ;

        private void CargarTotalesGastos()
        {
            int expensaId = Convert.ToInt32(Session["ExpensaId"]);

            lblTotalGastosOrdinarios.Text = _expensasServ.GetTotalGastosOrdinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastosExtraordinarios.Text = _expensasServ.GetTotalGastosExtraordinarios(expensaId).ToString("C", new CultureInfo("en-US"));
            lblTotalGastos.Text = (Constantes.GetDecimalFromCurrency(lblTotalGastosOrdinarios.Text) + Constantes.GetDecimalFromCurrency(lblTotalGastosExtraordinarios.Text)).ToString("C", new CultureInfo("en-US"));
        }

        public SubtotalesUF()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTotalesGastos();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expensas.aspx#consorcios");
        }
    }
}
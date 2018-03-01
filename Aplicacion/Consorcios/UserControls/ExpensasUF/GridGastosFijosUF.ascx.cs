using DAO;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.PaginasParciales
{
    public partial class GridGastosFijosUF : System.Web.UI.UserControl
    {
        private expensasServ _expensasServ;
        private int col_ID_Expensa = 2;

        private void CargarGrillaGastosFijos()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);
            bool soloSumaChequeada = true;

            grdGastosOrdinarios.DataSource = _expensasServ.GetGastosOrdinarios(expensaID, soloSumaChequeada);
            grdGastosOrdinarios.DataBind();
        }

        public GridGastosFijosUF()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaGastosFijos();
            }
        }

        protected void grdGastosOrdinarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }
    }
}
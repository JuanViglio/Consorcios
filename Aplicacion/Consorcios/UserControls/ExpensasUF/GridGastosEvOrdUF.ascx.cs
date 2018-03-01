using DAO;
using Servicios;
using System;
using System.Globalization;

namespace WebSistemmas.Consorcios.UserControls.ExpensasUF
{
    public partial class GridGastosEvOrdUF : System.Web.UI.UserControl
    {
        private expensasServ _expensasServ;
        private int col_ID_Expensa = 2;

        private void CargarGrillaGastosEvOrd()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosEventuales.DataSource = _expensasServ.GetGastosEvOrdinarios(expensaID);
            grdGastosEventuales.DataBind();

            lblTotalGastosEventuales.Text = _expensasServ.GetTotalGastosEvOrdinarios(expensaID).ToString("C", new CultureInfo("en-US"));
        }

        public GridGastosEvOrdUF()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaGastosEvOrd();
            }
        }

        protected void grdGastosEventuales_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }
    }
}
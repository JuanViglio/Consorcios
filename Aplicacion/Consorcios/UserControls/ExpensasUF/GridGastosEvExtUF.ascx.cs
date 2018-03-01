using DAO;
using Servicios;
using System;

namespace WebSistemmas.Consorcios.UserControls.ExpensasUF
{
    public partial class GridGastosEvExtUF : System.Web.UI.UserControl
    {
        private expensasServ _expensasServ;
        private int col_ID_Expensa = 2;

        private void CargarGrillaGastosEvExt()
        {
            int expensaID = Convert.ToInt32(Session["ExpensaId"]);

            grdGastosExtraordinarios.DataSource = _expensasServ.GetGastosEvExtraordinarios(expensaID);
            grdGastosExtraordinarios.DataBind();
        }

        public GridGastosEvExtUF()
        {
            ExpensasEntities context = new ExpensasEntities();
            _expensasServ = new expensasServ(context);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaGastosEvExt();
            }
        }


        protected void grdGastosExtraordinarios_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }
    }
}
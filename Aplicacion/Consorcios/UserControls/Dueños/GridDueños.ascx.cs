using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.Dueños
{
    public partial class GridDueños : System.Web.UI.UserControl
    {
        private dueñosNeg _dueñosNeg;
        private int col_ID = 0;

        #region Metodos Privados

        public GridDueños()
        {
            _dueñosNeg = new dueñosNeg();
        }

        public void LlenarGrillaDueños()
        {
            grdDueños.DataSource = _dueñosNeg.GetDueños();
            grdDueños.DataBind();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrillaDueños();
            }
        }

        protected void grdDueños_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdDueños_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID].Visible = false;
        }
    }
}
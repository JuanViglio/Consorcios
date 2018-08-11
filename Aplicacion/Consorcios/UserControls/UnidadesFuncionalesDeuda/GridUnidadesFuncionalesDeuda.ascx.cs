using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.UnidadesFuncionalesDeuda
{
    public partial class GridUnidadesFuncionalesDeuda : System.Web.UI.UserControl
    {
        private IPagosServ _pagosServ;

        public GridUnidadesFuncionalesDeuda()
        {
            ExpensasEntities context = new ExpensasEntities();
            _pagosServ = new pagosServ(context);
        }

        #region Metodos Privados
        private void LlenarGrillaUnidadesFuncionalesDeuda()
        {
            var idUF = decimal.Parse(Session["idUF"].ToString());
            grdUnidadesFuncionalesDeuda.DataSource = _pagosServ.GetPagosAdeudados(idUF);
            grdUnidadesFuncionalesDeuda.DataBind();
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrillaUnidadesFuncionalesDeuda();
            }
        }
    }
}
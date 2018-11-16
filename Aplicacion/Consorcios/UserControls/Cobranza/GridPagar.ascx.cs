using DAO;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WebSistemmas.Consorcios.UserControls.Cobranza
{
    public partial class GridPagar : System.Web.UI.UserControl
    {

        public void CargarGrillaCobrar()
        {
            List<UnidadesFuncionalesModel> ufModel = new List<UnidadesFuncionalesModel>();

            ufModel = (List<UnidadesFuncionalesModel>)Session["ufModel"];

            grdPagar.DataSource = ufModel;
            grdPagar.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCobrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divPagar.Visible = false;
        }
    }
}
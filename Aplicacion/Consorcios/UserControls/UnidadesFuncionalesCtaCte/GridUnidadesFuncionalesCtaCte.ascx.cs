using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;

namespace WebSistemmas.Consorcios.UserControls.UnidadesFuncionalesCtaCte
{
    public partial class GridUnidadesFuncionalesCtaCte : System.Web.UI.UserControl
    {
        private IUnidadesFuncionalesNeg _unidadesFuncionalesNeg;
        private IUnidadesServ _unidadesFuncionalesServ;
        private IPagosServ _pagosServ;
        private IExpensasServ _expensasServ;

        public GridUnidadesFuncionalesCtaCte()
        {
            ExpensasEntities context = new ExpensasEntities();
            _unidadesFuncionalesServ = new unidadesFuncionalesServ();
            _pagosServ = new pagosServ(context);
            _expensasServ = new expensasServ(context);

            _unidadesFuncionalesNeg = new unidadesFuncionaesNeg(_unidadesFuncionalesServ, _pagosServ, _expensasServ);
        }

        #region Metodos Privados
        private void LlenarGrillaUnidadesFuncionalesCtaCte()
        {
            var idUF = decimal.Parse(Session["idUF"].ToString()); 
            grdUnidadesFuncionalesCtaCte.DataSource = _unidadesFuncionalesNeg.GetCtaCte(idUF);
            grdUnidadesFuncionalesCtaCte.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrillaUnidadesFuncionalesCtaCte();
            }
        }
    }
}
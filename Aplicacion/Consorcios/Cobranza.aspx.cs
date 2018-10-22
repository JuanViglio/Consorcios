using DAO;
using Servicios;
using Servicios.Interfaces;
using System;

namespace WebSistemmas.Consorcios
{
    public partial class Cobranza : System.Web.UI.Page
    {
        private IConsorciosServ _consorciosServ;
        private IUnidadesServ _unidadesServ;
        private dueñosServ _propietariosServ;
        private ExpensasEntities context = new ExpensasEntities();

        public Cobranza()
        {
            _consorciosServ = new consorciosServ(context);
            _unidadesServ = new unidadesFuncionalesServ();
            _propietariosServ = new dueñosServ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.CargarTitulo("Cobranza");
            }
        }

        #region Botones

        protected void btnCobrar_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }

}
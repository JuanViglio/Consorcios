
using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;

namespace WebSistemmas.Consorcios.UserControls.CtaCteProveedor
{
    public partial class GridCtaCteProveedor : System.Web.UI.UserControl
    {
        private IProveedoresNeg _proveedoresNeg;
        private IProveedoresServ _proveedresServ;

        public GridCtaCteProveedor()
        {
            ExpensasEntities context = new ExpensasEntities();
            _proveedresServ = new proveedoresServ(context);
            _proveedoresNeg = new proveedoresNeg(_proveedresServ);
        }

        public void LlenarGrillaCtaCteProveedor()
        {
            var idProveedor = decimal.Parse(Session["ProveedorId"].ToString());
            grdCtaCteProveedores.DataSource = _proveedoresNeg.GetCtaCte(idProveedor);
            grdCtaCteProveedores.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrillaCtaCteProveedor();
            }
        }
    }
}
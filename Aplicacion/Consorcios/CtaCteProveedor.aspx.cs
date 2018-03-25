using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class CtaCteProveedor : System.Web.UI.Page
    {
        private IProveedoresNeg _proveedoresNeg;
        private IProveedoresServ _proveedresServ;

        private void LlenarGrillaCtaCteProveedor()
        {
            var idProveedor = decimal.Parse(Session["ProveedorId"].ToString());
            grdCtaCteProveedores.DataSource = _proveedoresNeg.GetCtaCte(idProveedor);
            grdCtaCteProveedores.DataBind();
        }

        public CtaCteProveedor()
        {
            ExpensasEntities context = new ExpensasEntities();
            _proveedresServ = new proveedoresServ(context);
            _proveedoresNeg = new proveedoresNeg(_proveedresServ);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPaginaID.TituloPagina = "Cuenta Corriente del Proveedor " + Session["NombreProveedor"].ToString() ?? "" ;
                LlenarGrillaCtaCteProveedor();
            }
        }
    }
}
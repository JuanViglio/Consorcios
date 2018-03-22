using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios.UserControls.Proveedores
{
    public partial class AgregarProveedores : System.Web.UI.UserControl
    {
        private IProveedoresNeg _proveedoresNeg;
        private IProveedoresServ _proveedresServ;

        #region Metodos Privados
        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        private void LlenarGrillaProveedores()
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlGridProveedoresID");
            GridProveedores gridProveedoresUc = (GridProveedores)control;

            gridProveedoresUc.LlenarGrillaProveedores();
        }

        private void LlenarComboTipo()
        {
            ddlTipoNuevo.Items.Add(Constantes.PrecioComprayVentaDistintos);
            ddlTipoNuevo.Items.Add(Constantes.PrecioComprayVentaIguales);
            ddlTipoNuevo.Items.Add(Constantes.PrecioCompraEs0);
        }
        #endregion

        public AgregarProveedores()
        {
            ExpensasEntities context = new ExpensasEntities();
            _proveedresServ = new proveedoresServ(context);
            _proveedoresNeg = new proveedoresNeg(_proveedresServ);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarComboTipo();
            }
        }

        protected void btnAceptarNuevoProveedor_Click(object sender, EventArgs e)
        {
            MostrarError(string.Empty);
            try
            {
               _proveedoresNeg.AgregarProveedor(txtNombreNuevo.Text, txtDireccionNuevo.Text.ToUpper(), txtMail.Text, ddlTipoNuevo.SelectedItem.Text);
                LlenarGrillaProveedores();

                txtNombreNuevo.Text = "";
                txtDireccionNuevo.Text = "";
                txtMail.Text = "";
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
    }
}
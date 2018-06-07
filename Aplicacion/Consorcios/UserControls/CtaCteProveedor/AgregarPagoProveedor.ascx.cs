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

namespace WebSistemmas.Consorcios.UserControls.CtaCteProveedor
{
    public partial class AgregarPagoProveedor : System.Web.UI.UserControl
    {
        readonly IProveedoresServ _proveedoresServ;
        readonly IProveedoresNeg _proveedoresNeg;

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
            Control control = placeHolder.FindControl("gridCtaCteProveedoresID");
            GridCtaCteProveedor gridProveedoresUc = (GridCtaCteProveedor)control;

            gridProveedoresUc.LlenarGrillaCtaCteProveedor();
        }
        #endregion

        public AgregarPagoProveedor()
        {
            ExpensasEntities context = new ExpensasEntities();
            _proveedoresServ = new proveedoresServ(context);
            _proveedoresNeg = new proveedoresNeg(_proveedoresServ);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgrgarPago_Click(object sender, EventArgs e)
        {
            try
            {
                decimal idProveedor = decimal.Parse(Session["ProveedorId"].ToString());

                _proveedoresNeg.AddDebe(txtFecha.Text, txtImporte.Text, idProveedor, txtOrdenDePago.Text, txtDetalle.Text);

                LlenarGrillaProveedores();

                txtFecha.Text = "";
                txtImporte.Text = "";
                txtOrdenDePago.Text = "";
                txtDetalle.Text = "";

                MostrarError(string.Empty);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
    }
}
using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.Proveedores
{
    public partial class GridProveedores : System.Web.UI.UserControl
    {
        private IProveedoresNeg _proveedoresNeg;
        private IProveedoresServ _proveedresServ;
        private const int col_IdProveedor = 0;

        #region Metodos Privados
        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        public void LlenarGrillaProveedores()
        {
            grdProveedores.DataSource = _proveedoresNeg.GetProveedores();
            grdProveedores.DataBind();
        }
        #endregion

        public GridProveedores()
        {
            ExpensasEntities context = new ExpensasEntities();
            _proveedresServ = new proveedoresServ(context);
            _proveedoresNeg = new proveedoresNeg(_proveedresServ);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrillaProveedores();
            }
        }

        protected void grdProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;
            MostrarError(string.Empty);

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;
                    string Tipo = e.CommandName.ToUpper();

                    switch (Tipo)
                    {
                        case "ELIMINAR":
                            var idGasto = Convert.ToInt32(GridViewrow.Cells[col_IdProveedor].Text);
                            _proveedoresNeg.EliminarProveedor(idGasto);
                            grdProveedores.DataSource = _proveedoresNeg.GetProveedores();
                            grdProveedores.DataBind();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

        }

        protected void grdProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINAR"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaProveedor();");

        }
    }
}
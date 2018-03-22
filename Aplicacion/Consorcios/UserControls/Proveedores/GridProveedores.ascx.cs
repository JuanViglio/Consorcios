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
        private const int col_Nombre = 1;
        private const int col_Direccion = 2;
        private const int col_Mail = 3;
        private const int col_Telefono = 4;
        private const int col_Tipo = 5;

        #region Metodos Privados
        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        private void EliminarProveedor(GridViewRow row)
        {
            var idGasto = Convert.ToInt32(row.Cells[col_IdProveedor].Text);
            _proveedoresNeg.EliminarProveedor(idGasto);
            LlenarGrillaProveedores();
        }

        private void ModificarProveedor(GridViewRow row)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showDiv", "$('#divProveedorModificar').slideDown();", true);

            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlModificarProveedoresID");
            ModificarProveedores errorUc = (ModificarProveedores)control;

            string codigo = row.Cells[col_IdProveedor].Text;
            string nombre = row.Cells[col_Nombre].Text;
            string direccion = row.Cells[col_Direccion].Text;
            string mail = row.Cells[col_Mail].Text;
            string telefono = row.Cells[col_Telefono].Text;
            string tipo = row.Cells[col_Tipo].Text;

            errorUc.MostrarDatosParaModificar(codigo, nombre, direccion, mail, telefono, tipo);

        }
        #endregion

        public void LlenarGrillaProveedores()
        {
            grdProveedores.DataSource = _proveedoresNeg.GetProveedores();
            grdProveedores.DataBind();
        }

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
                            EliminarProveedor(GridViewrow);
                            break;

                        case "MODIFICAR":
                            ModificarProveedor(GridViewrow);
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
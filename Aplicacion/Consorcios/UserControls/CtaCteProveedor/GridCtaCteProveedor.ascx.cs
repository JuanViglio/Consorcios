
using DAO;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios.UserControls.CtaCteProveedor
{
    public partial class GridCtaCteProveedor : System.Web.UI.UserControl
    {
        private IProveedoresNeg _proveedoresNeg;
        private IProveedoresServ _proveedresServ;
        private const int col_IdCtaCte = 0;
        private const int col_Debe = 2;
        private const int col_Eliminar = 6;

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
            var idGasto = Convert.ToInt32(row.Cells[col_IdCtaCte].Text);
            _proveedoresNeg.EliminarProveedorCtaCte(idGasto); 
            LlenarGrillaCtaCteProveedor();
        }

        #endregion

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

        protected void grdCtaCteProveedores_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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

        protected void grdCtaCteProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.Cells[col_Debe].Text != "Debe" && !e.Row.Cells[col_Debe].Text.IsNumeric())
            {
                e.Row.Cells[col_Eliminar].Visible = false;
            }

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINAR"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaProveedorCtaCte();");

        }
    }
}
using Negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios.UserControls.Dueños
{
    public partial class GridDueños : System.Web.UI.UserControl
    {
        private dueñosNeg _dueñosNeg;
        private const int col_IdDueño = 0;

        #region Metodos Privados

        private void EliminarDueño(GridViewRow row)
        {
            var idDueño = Convert.ToInt32(row.Cells[col_IdDueño].Text);
            _dueñosNeg.EliminarDueño(idDueño);
            LlenarGrillaDueños();
        }

        private void ModificarDueño(GridViewRow row)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showDiv", "$('#divDueñoModificar').slideDown();", true);

            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlModificarDueñosID");
            ModificarDueños errorUc = (ModificarDueños)control;

            string codigo = row.Cells[col_IdDueño].Text;

            errorUc.MostrarDatosParaModificar(codigo);
        }

        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        public GridDueños()
        {
            _dueñosNeg = new dueñosNeg();
        }

        public void LlenarGrillaDueños()
        {
            grdDueños.DataSource = _dueñosNeg.GetDueños();
            grdDueños.DataBind();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrillaDueños();
            }
        }

        protected void grdDueños_RowCommand(object sender, GridViewCommandEventArgs e)
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
                            EliminarDueño(GridViewrow);
                            break;

                        case "MODIFICAR":
                            ModificarDueño(GridViewrow);
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

        protected void grdDueños_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_IdDueño].Visible = false;

            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINAR"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaDueño();");

        }
    }
}
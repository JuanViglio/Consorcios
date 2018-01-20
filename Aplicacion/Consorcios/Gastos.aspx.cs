using DAO;
using Servicios;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Gastos : Page
    {
        private gastosServ _gastosServ;
        private const int col_NombreGasto = 0;
        private const int col_IdGasto = 1;

        public Gastos()
        {
            ExpensasEntities context = new ExpensasEntities();
            _gastosServ = new gastosServ(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                CargarComboTipos();                
                CargarGrillaTipos(ddlTipoGastos.SelectedValue);
            }
        }

        private void CargarComboTipos()
        {            
            ddlTipoGastos.DataSource = _gastosServ.GetTipoGastos();
            ddlTipoGastos.DataTextField = "Detalle";
            ddlTipoGastos.DataValueField = "Id";
            ddlTipoGastos.DataBind();
        }

        protected void grdGastos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;
            lblError.Text = "";

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;

                    string Tipo = e.CommandName.ToUpper();
                    lblError.Text = "";

                    switch (Tipo)
                    {
                        case "ELIMINAR":
                            var idGasto = Convert.ToInt32(GridViewrow.Cells[col_IdGasto].Text);
                            grdGastos.DataSource = _gastosServ.DeleteGasto(idGasto, Convert.ToInt32(ddlTipoGastos.SelectedValue));
                            grdGastos.DataBind();
                            break;

                        case "CARGARGASTO":
                            Session["IdGasto"] = Convert.ToInt32(GridViewrow.Cells[col_IdGasto].Text);
                            Session["NombreGasto"] = GridViewrow.Cells[col_NombreGasto].Text;
                            Response.Redirect("CargarGastos.aspx#gastos");
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void CargarGrillaTipos(string idTipoGasto)
        {
            grdGastos.DataSource = _gastosServ.GetDetalleGastos(Convert.ToInt32(idTipoGasto));
            grdGastos.DataBind();
        }

        protected void ddlTipoGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaTipos(ddlTipoGastos.SelectedValue);
        }

        protected void btnNuevoGasto_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptarNuevoGasto_Click(object sender, EventArgs e)
        {
            grdGastos.DataSource = _gastosServ.AddGasto(Convert.ToInt32(ddlTipoGastos.SelectedValue), txtDetalleGasto.Text.ToUpper());
            grdGastos.DataBind();
        }

        protected void grdGastos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINAR"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGasto();");

            e.Row.Cells[col_IdGasto].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Consorcios.aspx#consorcios");
        }
    }
}
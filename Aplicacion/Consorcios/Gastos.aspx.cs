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
                CargarGrillaGastos();
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
                        case "MODIFICAR":
                            ClientScript.RegisterStartupScript(GetType(), "showDiv", "$('#divGastoModificar').slideDown()", true);

                            Session["IdGasto"] = GridViewrow.Cells[col_IdGasto].Text;
                            txtGastoModificar.Text = GridViewrow.Cells[col_NombreGasto].Text;
                            break;

                        case "ELIMINAR":
                            var idGasto = Convert.ToInt32(GridViewrow.Cells[col_IdGasto].Text);
                            grdGastos.DataSource = _gastosServ.DeleteGasto(idGasto, Convert.ToInt32(ddlTipoGastos.SelectedValue));
                            grdGastos.DataBind();
                            txtDetalleBuscar.Text = string.Empty;
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

        private void CargarGrillaGastos()
        {
            grdGastos.DataSource = _gastosServ.GetDetalleGastos(Convert.ToInt32(ddlTipoGastos.SelectedValue), txtDetalleBuscar.Text);
            grdGastos.DataBind();
            lblPagina.Text = "Pagina " + (grdGastos.PageIndex + 1);
        }

        protected void ddlTipoGastos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaGastos();
        }

        protected void btnNuevoGasto_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptarNuevoGasto_Click(object sender, EventArgs e)
        {
            grdGastos.DataSource = _gastosServ.AddGasto(Convert.ToInt32(ddlTipoGastos.SelectedValue), txtDetalleGasto.Text.ToUpper());
            grdGastos.DataBind();
            txtDetalleGasto.Text = string.Empty;
            txtDetalleBuscar.Text = string.Empty;
        }

        protected void grdGastos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ImageButton imgBorrar;
            imgBorrar = (ImageButton)(e.Row.FindControl("ELIMINAR"));

            if (imgBorrar != null)
                imgBorrar.Attributes.Add("OnClick", "JavaScript:return ConfirmarBajaGasto();");

            if (e.Row.Cells.Count > 1)
                e.Row.Cells[col_IdGasto].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Consorcios.aspx#consorcios");
        }

        protected void grdGastos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGastos.PageIndex = e.NewPageIndex;
            CargarGrillaGastos();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaGastos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDetalleBuscar.Text = string.Empty;
            CargarGrillaGastos();
        }

        protected void btnModificarGasto_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            grdGastos.DataSource = _gastosServ.UpdateGasto(Session["IdGasto"].ToString().ToDecimal(), txtGastoModificar.Text, Convert.ToInt32(ddlTipoGastos.SelectedValue));
            grdGastos.DataBind();
        }
    }
}
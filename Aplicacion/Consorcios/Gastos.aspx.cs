using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Gastos : System.Web.UI.Page
    {
        private gastosServ serv;
        private const int col_IdGasto = 1;

        public Gastos()
        {
            serv = new gastosServ();
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
            ddlTipoGastos.DataSource = serv.GetTipoGastos();
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
                            grdGastos.DataSource = serv.DeleteGasto(idGasto, Convert.ToInt32(ddlTipoGastos.SelectedValue));
                            grdGastos.DataBind();
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
            grdGastos.DataSource = serv.GetDetalleGastos(Convert.ToInt32(idTipoGasto));
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
            grdGastos.DataSource = serv.AddGasto(Convert.ToInt32(ddlTipoGastos.SelectedValue), txtDetalleGasto.Text.ToUpper());
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
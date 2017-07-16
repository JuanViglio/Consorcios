using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Cobranza
{
    public partial class Cobros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscarDNI_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

            var uf = serv.GetUFByFilter(txtNombre.Text , "");

            grdAlquileres.DataSource = uf;
            grdAlquileres.DataBind();
        }

        protected void btnBuscarDireccion_Click(object sender, EventArgs e)
        {

        }

        protected void grdAlquileres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
            GridViewRow GridViewrow = null;

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
                        case "VER PAGOS":
                            var pagos = serv.GetPagosByFilter(Convert.ToDecimal(GridViewrow.Cells[3].Text));
                            grdPagos.DataSource = pagos;
                            grdPagos.DataBind();
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

        protected void grdAlquileres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }

        protected void grdAlquileres_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
            GridViewRow GridViewrow = null;

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
                        case "COBRAR":
                            serv.PagarExpensa(Convert.ToDecimal(GridViewrow.Cells[1].Text),Convert.ToDecimal(GridViewrow.Cells[2].Text));
                            grdAlquileres.DataSource = null;
                            grdAlquileres.DataBind();
                            grdPagos.DataSource = null;
                            grdPagos.DataBind();
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

        protected void grdPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdPagos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
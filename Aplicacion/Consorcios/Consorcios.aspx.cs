using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Consorcios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["Usuario"] == null)
                //{
                //    Response.Redirect("LoginConsorcios.aspx");
                //    return;
                //}

                consorciosServ serv = new consorciosServ();

                grdConsorcios.DataSource = serv.GetConsorcios();
                grdConsorcios.DataBind();
            }
        }

        protected void grdConsorcios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
                        case "MODIFICAR":
                            ClientScript.RegisterStartupScript(GetType(), "showDiv", "$('#divConsorciosModificar').slideDown();", true);

                            txtCodigo.Text = GridViewrow.Cells[0].Text;
                            txtDireccion.Text = GridViewrow.Cells[1].Text;
                            txtVencimiento1.Text = GridViewrow.Cells[2].Text;
                            txtVencimiento2.Text = GridViewrow.Cells[3].Text;
                            txtInteres.Text = GridViewrow.Cells[4].Text;
                            break;

                        case "UNIDADESFUNCIONALES":
                            Response.Redirect("UnidadesFuncionales.aspx", false);
                            break;

                        case "EXPENSAS":
                            Session["idConsorcio"] = GridViewrow.Cells[0].Text;
                            Response.Redirect("Expensas.aspx", false);
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

        protected void btnAceptarModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarModificar_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevoConsorcio_Click(object sender, EventArgs e)
        {

        }
    }
}
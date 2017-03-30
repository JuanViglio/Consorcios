using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Expensas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                expensasServ serv = new expensasServ();

                grdExpensas.DataSource = serv.GetExpensas("1");
                grdExpensas.DataBind();
            }
        }

        protected void grdExpensas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow GridViewrow = null;

            try
            {
                if (e.CommandSource.GetType().ToString().ToUpper().Contains("IMAGEBUTTON"))
                {
                    ImageButton _ImgButton = (ImageButton)e.CommandSource;
                    GridViewrow = (GridViewRow)_ImgButton.NamingContainer;

                    string Tipo = e.CommandName.ToUpper();
                    //lblError.Text = "";

                    switch (Tipo)
                    {
                        case "UNIDADESFUNCIONALES":
                            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
                            grdUnidades.DataSource = serv.GetUnidadesFuncionales("1");
                            grdUnidades.DataBind();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
            }

        }

        protected void grdUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnNuevaExpensa_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExpensaNueva.aspx", false);
        }
    }
}
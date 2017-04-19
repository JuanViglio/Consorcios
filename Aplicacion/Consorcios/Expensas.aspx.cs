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
        private int col_ID_Expensa = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                expensasServ serv = new expensasServ();

                grdExpensas.DataSource = serv.GetExpensas(Session["idConsorcio"].ToString());
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
                            grdUnidades.DataSource = serv.GetUnidadesFuncionales(Session["idConsorcio"].ToString());
                            grdUnidades.DataBind();
                            break;

                        case "EXPENSAS":
                            Session["idExpensa"] = GridViewrow.Cells[col_ID_Expensa].Text;
                            Response.Redirect("ExpensaNueva.aspx", false);
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
            expensasServ serv = new expensasServ();

            serv.AgregarExpensa(Session["idConsorcio"].ToString());

            Response.Redirect("ExpensaNueva.aspx", false);
        }

        protected void grdExpensas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdExpensas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_ID_Expensa].Visible = false;
        }
    }
}
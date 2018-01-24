using DAO;
using Servicios;
using System;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Privado : System.Web.UI.Page
    {
        private ExpensasEntities context = new ExpensasEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaExpensas();
            }
        }

        private void CargarGrillaExpensas()
        {
            expensasServ serv = new expensasServ(context);

            grdExpensas.DataSource = serv.GetExpensas(Session["idConsorcio"].ToString());
            grdExpensas.DataBind();
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
                        case "ELIMINAR":
                            expensasServ serv = new expensasServ(context);
                            serv.DeleteExpensa(int.Parse(GridViewrow.Cells[3].Text));
                            CargarGrillaExpensas();
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


        protected void grdExpensas_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

        }
    }
}
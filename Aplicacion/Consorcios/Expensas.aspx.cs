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
        private const int col_Coeficiente = 2;
        private const int col_Expensa_ID = 3;
        private const int col_Periodo = 6;
        private const int col_Pago_ID = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaExpensas();
            }
        }

        private void CargarGrillaExpensas()
        {
            expensasServ serv = new expensasServ();

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
                        case "UNIDADESFUNCIONALES":
                            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

                            if (GridViewrow.Cells[2].Text == "Aceptado" || GridViewrow.Cells[2].Text == "Finalizado")
                            {
                                Session["Estado"] = GridViewrow.Cells[2].Text;
                                Session["ExpensaId"] = GridViewrow.Cells[col_Expensa_ID].Text;
                                Session["PeriodoNumerico"] = GridViewrow.Cells[col_Periodo].Text;
                                grdUnidades.DataSource = serv.GetPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(GridViewrow.Cells[col_Periodo].Text));
                                grdUnidades.DataBind();

                                if (GridViewrow.Cells[2].Text == "Aceptado")                                
                                    divBotonesUF.Visible = true;
                                                                
                            }
                            else
                            {
                                grdUnidades.DataSource = null;
                                grdUnidades.DataBind();
                                divBotonesUF.Visible = false;
                                ClientScript.RegisterStartupScript(GetType(), "Atencion", "alert('La Expensa no esta Aceptada')", true);
                            }

                            break;

                        case "EXPENSAS":
                            if (GridViewrow.Cells[2].Text != "Finalizado")
                            {
                                if (GridViewrow.Cells[2].Text == "Aceptado")
                                    Session["Estado"] = "Aceptado";
                                else
                                    Session["Estado"] = "En Proceso";

                                Session["ExpensaId"] = GridViewrow.Cells[col_Expensa_ID].Text;
                                Response.Redirect("ExpensaNueva.aspx", false);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Atencion", "alert('La Expensa se encuentra Finalizada')", true);
                            }
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
            ImageButton _ImgButton = (ImageButton)e.CommandSource;
            GridViewRow GridViewrow = null;

            GridViewrow = (GridViewRow)_ImgButton.NamingContainer;

            Session["Coeficiente"] = GridViewrow.Cells[col_Coeficiente].Text;
            Session["PagoId"] = GridViewrow.Cells[col_Pago_ID].Text;
            Response.Redirect("ExpensaUFNueva.aspx", false);
        }

        protected void btnNuevaExpensa_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ();

            var ExpensaId = serv.AgregarExpensa(Session["idConsorcio"].ToString());

            if (ExpensaId == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Atencion", "alert('La ultima Expensa no esta Finalizada')", true);
            }
            else
            {
                Session["ExpensaId"] = ExpensaId;

                Response.Redirect("ExpensaNueva.aspx", false);
            }

        }

        protected void grdExpensas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdExpensas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_Expensa_ID].Visible = false;
            e.Row.Cells[col_Periodo].Visible = false;
        }

        protected void btnAceptarExpensasUF_Click(object sender, EventArgs e)
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

            serv.FinalizarPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(Session["PeriodoNumerico"].ToString()));

            grdUnidades.DataSource = "";
            grdUnidades.DataBind();
            divBotonesUF.Visible = false;

            CargarGrillaExpensas();

        }

        protected void btnAnularExpensasUF_Click(object sender, EventArgs e)
        {
            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

            serv.CancelarPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(Session["PeriodoNumerico"].ToString()));

            grdUnidades.DataSource = "";
            grdUnidades.DataBind();
            divBotonesUF.Visible = false;

            CargarGrillaExpensas();
        }

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_Pago_ID].Visible = false;
        }
    }
}
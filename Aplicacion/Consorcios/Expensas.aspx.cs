using DAO;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Expensas : System.Web.UI.Page
    {
        private const int col_PeriodoExpensa = 0;
        private const int col_Coeficiente = 2;
        private const int col_Expensa_ID = 3;
        private const int col_Periodo = 6;
        private const int col_Pago_ID = 4;
        private const int gridUF_col_Apellido = 1;
        private const int gridUF_col_Nombre = 2;

        private ExpensasEntities context = new ExpensasEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

                CargarGrillaExpensas();

                if (Session["direccionConsorcio"] != null)
                    lblTitulo.Text = "Expensas de " + Session["direccionConsorcio"].ToString();

                if (bool.Parse(Session["MostrarDivUF"].ToString()))
                {
                    grdUnidades.DataSource = serv.GetPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(Session["PeriodoNumerico"].ToString()));
                    grdUnidades.DataBind();
                    divBotonesUF.Visible = true;
                }
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
                        case "UNIDADESFUNCIONALES":
                            unidadesFuncionalesServ serv = new unidadesFuncionalesServ();
                            Dictionary<decimal, UnidadesFuncionalesModel> map = new Dictionary<decimal, UnidadesFuncionalesModel>();
                            
                            if (GridViewrow.Cells[2].Text == "Aceptado" || GridViewrow.Cells[2].Text == "Finalizado")
                            {
                                Session["Estado"] = GridViewrow.Cells[2].Text;
                                Session["ExpensaId"] = GridViewrow.Cells[col_Expensa_ID].Text;
                                Session["PeriodoNumerico"] = GridViewrow.Cells[col_Periodo].Text;
                                var pagos = serv.GetPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(GridViewrow.Cells[col_Periodo].Text));
                                grdUnidades.DataSource = pagos;
                                grdUnidades.DataBind();

                                if (GridViewrow.Cells[2].Text == "Aceptado")
                                {
                                    divBotonesUF.Visible = true;
                                    Session["MostrarDivUF"] = true;

                                    for (int i = 1; i <= pagos.Count; i++)
                                    {
                                        map.Add(i, pagos[i-1]);
                                    }

                                    Session["MapPagoId"] = map;
                                }
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

                                Session["Periodo"] = GridViewrow.Cells[col_PeriodoExpensa].Text;
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
            Session["NobreUF"] = GridViewrow.Cells[gridUF_col_Apellido].Text + " " + GridViewrow.Cells[gridUF_col_Nombre].Text;
            Response.Redirect("ExpensaUFNueva.aspx", false);
        }

        protected void btnNuevaExpensa_Click(object sender, EventArgs e)
        {
            expensasServ serv = new expensasServ(context);

            var ExpensaId = serv.AgregarExpensa(Session["idConsorcio"].ToString());
            Session["Periodo"] = null;

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
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

                serv.FinalizarPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(Session["PeriodoNumerico"].ToString()));

                grdUnidades.DataSource = "";
                grdUnidades.DataBind();
                divBotonesUF.Visible = false;

                CargarGrillaExpensas();
            }
        }

        protected void btnAnularExpensasUF_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                unidadesFuncionalesServ serv = new unidadesFuncionalesServ();

                serv.CancelarPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(Session["PeriodoNumerico"].ToString()));

                grdUnidades.DataSource = "";
                grdUnidades.DataBind();
                divBotonesUF.Visible = false;

                CargarGrillaExpensas();
            }
        }

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_Pago_ID].Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Consorcios.aspx#consorcios");
        }

        protected void btnAGastosParticulares_Click(object sender, EventArgs e)
        {
            Response.Redirect("GastosParticulares.aspx#consorcios");
        }
    }
}
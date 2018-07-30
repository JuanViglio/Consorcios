using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Expensas : System.Web.UI.Page
    {
        private const int col_PeriodoExpensa = 0;        
        private const int col_Expensa_ID = 3;
        private const int col_Periodo = 6;

        private const int gridUF_col_ID = 0;
        private const int gridUF_col_Apellido = 2;
        private const int gridUF_col_Nombre = 3;
        private const int col_Coeficiente = 4;
        private const int col_Pago_ID = 5;
        private ExpensasEntities context = new ExpensasEntities();

        #region Metodos Privados
        private void CargarGrillaExpensas()
        {
            expensasServ serv = new expensasServ(context);

            grdExpensas.DataSource = serv.GetExpensas(Session["idConsorcio"].ToString());
            grdExpensas.DataBind();
        }

        private void GuardarPagos_y_UnidadesFuncionaesCtaCte(unidadesFuncionalesServ _unidadesFuncionalesServ,
            expensasServ _expensasServ, pagosServ _pagosServ)
        {
            Dictionary<decimal, UnidadesFuncionalesModel> map = (Dictionary<decimal, UnidadesFuncionalesModel>)Session["MapPagoId"];

            foreach (var item in map)
            {
                var pago = _unidadesFuncionalesServ.GetPago(item.Value.PagoId);
                int expensaID = Convert.ToInt32(Session["ExpensaId"]);
                var PagoId = pago.ID.ToString();
                decimal coeficiente = pago.Coeficiente;
                decimal gastosOrdinarios = _expensasServ.GetTotalGastosOrdinarios(expensaID);
                decimal gastosExtraordinarios = _expensasServ.GetTotalGastosExtraordinarios(expensaID);
                decimal subtotalGastoOrdinario = gastosOrdinarios * coeficiente / 100;
                decimal subtotalGastoExtraordinario = gastosExtraordinarios * coeficiente / 100;
                decimal subtotalGastoCocheraOrd = _pagosServ.GetTotalGastosEvOrdinariosUF(int.Parse(PagoId));
                decimal subtotalGastoCocheraExt = _pagosServ.GetTotalGastosEvExtUF(int.Parse(PagoId));
                decimal importeGastoParticular = pago.ImporteGastoParticular;

                //GARDAR en PAGOS y en UnidadFuncionalCtaCte
                pago.ImportePago1 = subtotalGastoOrdinario + subtotalGastoExtraordinario + subtotalGastoCocheraOrd + subtotalGastoCocheraExt + importeGastoParticular;
                _pagosServ.ActualizarImportePago1(pago);

                DAO.UnidadesFuncionalesCtaCte ufCtaCte = new DAO.UnidadesFuncionalesCtaCte()
                {
                    UnidadesFuncionales = _unidadesFuncionalesServ.GetUnidadFuncional(Session["idConsorcio"].ToString(), item.Value.UF), 
                    Haber = pago.ImportePago1,
                    Fecha = DateTime.Now,               
                    Detalle = "Expensa " + item.Value.PeriodoDetalle
                };

                _unidadesFuncionalesServ.AddHaber(ufCtaCte);

            }
        }
        #endregion

        #region Page_Load
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
        #endregion

        #region Grilla grdExpensas
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
                    lblError.Text = "";

                    switch (Tipo)
                    {
                        case "UNIDADESFUNCIONALES":
                            divBotonesUF.Visible = false;
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
                                }

                                for (int i = 1; i <= pagos.Count; i++)
                                {
                                    map.Add(i, pagos[i-1]);
                                }

                                Session["MapPagoId"] = map;                                
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
                lblError.Text = ex.Message;
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
        #endregion

        #region Grilla grdUnidades
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

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_Pago_ID].Visible = false;
            e.Row.Cells[gridUF_col_ID].Visible = false;
        }
        #endregion

        #region Botones
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

        protected void btnAceptarExpensasUF_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Si")
            {
                unidadesFuncionalesServ _unidadesFuncionalesServ = new unidadesFuncionalesServ();
                expensasServ _expensasServ = new expensasServ(context);
                pagosServ _pagosServ = new pagosServ(context);

                _unidadesFuncionalesServ.FinalizarPagos(Session["idConsorcio"].ToString(), Convert.ToInt32(Session["PeriodoNumerico"].ToString()));

                GuardarPagos_y_UnidadesFuncionaesCtaCte(_unidadesFuncionalesServ, _expensasServ, _pagosServ);

                grdUnidades.DataSource = "";
                grdUnidades.DataBind();
                divBotonesUF.Visible = false;
            }

            expensasServ expensasServ = new expensasServ(context);

            var ExpensaId = expensasServ.AgregarExpensa(Session["idConsorcio"].ToString());

            CargarGrillaExpensas();
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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Consorcios.aspx#consorcios");
        }

        protected void btnAGastosParticulares_Click(object sender, EventArgs e)
        {
            Response.Redirect("GastosParticulares.aspx#consorcios");
        }
        #endregion
    }
}
using DAO;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class Consorcios : System.Web.UI.Page
    {
        private const int colIdConsorcio = 0;
        private const int colDireccionConsorcio = 1;
        IConsorciosServ serv;
        private ExpensasEntities context = new ExpensasEntities();

        public Consorcios()
        {
            serv = new consorciosServ(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["Usuario"] == null)
                //{
                //    Response.Redirect("LoginConsorcios.aspx");
                //    return;
                //}                

                try
                {
                    lblError.Text = "";
                    grdConsorcios.DataSource = serv.GetConsorcios();
                    grdConsorcios.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        protected void grdConsorcios_RowCommand(object sender, GridViewCommandEventArgs e)
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
                            ClientScript.RegisterStartupScript(GetType(), "showDiv", "$('#divConsorcioModificar').slideDown();", true);

                            txtCodigo.Text = GridViewrow.Cells[colIdConsorcio].Text;
                            txtDireccion.Text = GridViewrow.Cells[1].Text;
                            txtVencimiento1.Text = GridViewrow.Cells[2].Text;
                            txtVencimiento2.Text = GridViewrow.Cells[3].Text;
                            txtInteres.Text = GridViewrow.Cells[4].Text;
                            break;

                        case "UNIDADESFUNCIONALES":
                            Session["idConsorcio"] = GridViewrow.Cells[colIdConsorcio].Text;
                            Response.Redirect("UnidadesFuncionales.aspx", false);
                            break;

                        case "EXPENSAS":
                            Session["idConsorcio"] = GridViewrow.Cells[colIdConsorcio].Text;
                            Session["direccionConsorcio"] = GridViewrow.Cells[colDireccionConsorcio].Text;
                            Session["MostrarDivUF"] = false;
                            Response.Redirect("Expensas.aspx", false);
                            break;

                        case "DETALLES":
                            Session["idConsorcio"] = GridViewrow.Cells[colIdConsorcio].Text;
                            Session["addressConsorcio"] = GridViewrow.Cells[colDireccionConsorcio].Text;
                            Response.Redirect("Detalles.aspx#consorcios", false);
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
            lblError.Text = "";

            #region Validaciones
            if (txtDireccion.Text == "")
            {
                lblError.Text = "No se ingreso la Direccion";
                return;
            }
            else if (txtVencimiento1.Text == "")
            {
                lblError.Text = "No se ingreso el Vencimiento 1";
                return;
            }
            else if (!txtVencimiento1.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Vencimiento numerico";
                return;
            }
            else if (txtVencimiento2.Text == "")
            {
                lblError.Text = "No se ingreso el Vencimiento 2";
                return;
            }
            else if (!txtVencimiento2.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Vencimiento numerico";
                return;
            }
            else if (txtInteres.Text == "")
            {
                lblError.Text = "No se ingreso el Interes";
                return;
            }
            else if (!txtInteres.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Interes numerico";
                return;
            }
            #endregion

            grdConsorcios.DataSource = serv.UpdateConsorcios(txtCodigo.Text, txtDireccion.Text.ToUpper(), txtVencimiento1.Text, txtVencimiento2.Text, txtInteres.Text);
            grdConsorcios.DataBind();
        }

        protected void btnAceptarNuevoConsorcio_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            #region Validaciones
            if (txtCodigoNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Codigo del Consorcio";
                return;
            }
            else if (txtDireccionNuevo.Text == "")
            {
                lblError.Text = "No se ingreso la Direccion";
                return;
            }
            else if (txtVencimiento1Nuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Vencimiento 1";
                return;
            }
            else if (!txtVencimiento1Nuevo.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Vencimiento numerico";
                return;
            }
            else if (txtVencimiento2Nuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Vencimiento 2";
                return;
            }
            else if (!txtVencimiento2Nuevo.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Vencimiento numerico";
                return;
            }
            else if (txtInteresNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Interes";
                return;
            }
            else if (!txtInteresNuevo.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Interes numerico";
                return;
            }
            #endregion

            var consorcios = serv.AddConsorcio(txtCodigoNuevo.Text, txtDireccionNuevo.Text.ToUpper(), txtVencimiento1Nuevo.Text, txtVencimiento2Nuevo.Text, txtInteresNuevo.Text);

            if (consorcios != null)
            {
                grdConsorcios.DataSource = consorcios;
                grdConsorcios.DataBind();

                #region Limpiar campos
                txtCodigoNuevo.Text = "";
                txtDireccionNuevo.Text = "";
                txtVencimiento1Nuevo.Text = "";
                txtVencimiento2Nuevo.Text = "";
                txtInteresNuevo.Text = "";
                #endregion
            }
            else
                lblError.Text = "No se pudo ingresar el Consorcio. Revise el codigo ingresado";
            
        }

        protected void btnNuevoConsorcio_Click(object sender, EventArgs e)
        {

        }
    }
}
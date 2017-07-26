using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class UnidadesFuncionales : System.Web.UI.Page
    {
        private IUnidadesServ _serv;
        private const int col_idUF = 3;

        public UnidadesFuncionales()
        {
            _serv = new unidadesFuncionalesServ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                string idConsorcio = Session["idConsorcio"].ToString();
                grdUnidades.DataSource = _serv.GetUnidadesFuncionales(idConsorcio);
                grdUnidades.DataBind();
            }
        }


        protected void grdUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
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
                            ClientScript.RegisterStartupScript(GetType(), "showDiv", "$('#divUFModificar').slideDown();", true);

                            txtNumero.Text = GridViewrow.Cells[0].Text;
                            txtDueño.Text = GridViewrow.Cells[1].Text;
                            txtCoeficiente.Text = GridViewrow.Cells[2].Text;
                            txtID.Text = GridViewrow.Cells[3].Text;
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

            if (txtDueño.Text == "")
            {
                lblError.Text = "No se ingreso el Dueño de la Unidad Funcional";
                return;
            }
            else if (txtCoeficiente.Text == "")
            {
                lblError.Text = "No se ingreso el Coeficiente de la Unidad Funcional";
                return;
            }
            else if (!txtCoeficiente.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Coeficiente numerico";
                return;
            }
            #endregion

            try
            {
                string idConsorcio = Session["idConsorcio"].ToString();
                grdUnidades.DataSource = _serv.ModificarUnidades(idConsorcio, Convert.ToInt32(txtID.Text), txtNumero.Text, txtDueño.Text, Convert.ToDecimal(txtCoeficiente.Text));
                grdUnidades.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

        }

        protected void btnAceptarNuevoUF_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            #region Validaciones

            if (txtNumeroNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Numero de la Unididad Funcional";
                return;
            }
            else if (txtDueñoNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Dueño de la Unidad Funcional";
                return;
            }
            else if (txtCoeficienteNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Coeficiente de la Unidad Funcional";
                return;
            }
            else if (!txtCoeficienteNuevo.Text.IsNumeric())
            {
                lblError.Text = "No se ingreso un Coeficiente numerico";
                return;
            }
            #endregion

            try
            {
                string idConsorcio = Session["idConsorcio"].ToString();
                grdUnidades.DataSource = _serv.AgregarUnidad(idConsorcio, txtNumeroNuevo.Text, txtDueñoNuevo.Text, Convert.ToDecimal(txtCoeficienteNuevo.Text));
                grdUnidades.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_idUF].Visible = false;
        }
    }
}
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas.Consorcios
{
    public partial class UnidadesFuncionales : System.Web.UI.Page
    {
        private IUnidadesServ _serv;
        private const int col_numero = 0;
        private const int col_departamento = 1;
        private const int col_apellido = 2;
        private const int col_nombre = 3;
        private const int col_cochera = 4;
        private const int col_coeficiente = 5;
        private const int col_idUF = 6;

        #region Constructor y Page_Load
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
                CargarComboDueños();
            }
        }
        #endregion

        #region grdUnidades
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

                            txtNumero.Text = GridViewrow.Cells[col_numero].Text;
                            txtDepartamento.Text = GridViewrow.Cells[col_departamento].Text;
                            txtApellido.Text = GridViewrow.Cells[col_apellido].Text;
                            txtNombre.Text = GridViewrow.Cells[col_nombre].Text;
                            txtApellido.Text = GridViewrow.Cells[col_apellido].Text;
                            txtCoeficiente.Text = GridViewrow.Cells[col_coeficiente].Text;
                            txtID.Text = GridViewrow.Cells[col_idUF].Text;
                            ddlCochera.SelectedValue = GridViewrow.Cells[col_cochera].Text;
                            break;

                        case "CTACTE":
                            Session["idUF"] = GridViewrow.Cells[col_idUF].Text;
                            Session["numeroUF"] = GridViewrow.Cells[col_numero].Text;
                            Session["dueñoUF"] = GridViewrow.Cells[col_nombre].Text + " " + GridViewrow.Cells[col_apellido].Text;
                            Response.Redirect("UnidadesFuncionalesCtaCte.aspx#consorcios");
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

        protected void grdUnidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[col_idUF].Visible = false;
        }
        #endregion

        #region Botones
        protected void btnAceptarModificar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            #region Validaciones

            if (txtDepartamento.Text == "")
            {
                lblError.Text = "No se ingreso el Departamento de la Unidad Funcional";
                return;
            }
            if (txtApellido.Text == "")
            {
                lblError.Text = "No se ingreso el Apellido del dueño de la Unidad Funcional";
                return;
            }
            if (txtNombre.Text == "")
            {
                lblError.Text = "No se ingreso el Nombre del dueño de la Unidad Funcional";
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
                string departamento = txtDepartamento.Text;
                decimal coeficiente = decimal.Parse(txtCoeficiente.Text);
                grdUnidades.DataSource = _serv.ModificarUnidades(idConsorcio, Convert.ToInt32(txtID.Text), departamento, txtNumero.Text, txtApellido.Text.ToUpper(), txtNombre.Text.ToUpper(), coeficiente, ddlCochera.SelectedValue);
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
            else if (txtApellidoNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Apellido del dueño de la Unidad Funcional";
                return;
            }
            else if (txtNombreNuevo.Text == "")
            {
                lblError.Text = "No se ingreso el Nombre del dueño de la Unidad Funcional";
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
                grdUnidades.DataSource = _serv.AgregarUnidad(idConsorcio, txtNumeroNuevo.Text, txtDepartamentoNuevo.Text, txtApellidoNuevo.Text.ToUpper(), txtNombreNuevo.Text.ToUpper(), Convert.ToDecimal(txtCoeficienteNuevo.Text), ddlCocheraNueva.SelectedValue);
                grdUnidades.DataBind();

                txtNumeroNuevo.Text = "";
                txtDepartamento.Text = "";
                txtNombreNuevo.Text = "";
                txtApellidoNuevo.Text = "";
                txtNombreNuevo.Text = "";
                txtCoeficienteNuevo.Text = "";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Consorcios.aspx#consorcios");
        }
        #endregion

        #region Metodos Privados
        private void CargarComboDueños()
        {
            dueñosServ dueñosServ = new dueñosServ();

            ddlDueños.DataSource = dueñosServ.GetDueños();
            ddlDueños.DataTextField = "Apellido_y_Nombre";
            ddlDueños.DataValueField = "ID";
            ddlDueños.DataBind();
        }

        #endregion  
    }
}
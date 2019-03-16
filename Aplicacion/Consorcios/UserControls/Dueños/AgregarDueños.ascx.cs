using DAO;
using DAO.Models;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios.UserControls.Dueños
{
    public partial class AgregarDueños : System.Web.UI.UserControl
    {
        private dueñosServ _dueñosServ;

        #region Metodos Privados
        private void MostrarError(string error)
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControl2ID");
            Error errorUc = (Error)control;

            errorUc.MostrarError(error);
        }

        private void LlenarGrillaDueños()
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlGridDueñosID");
            GridDueños gridDueñosUc = (GridDueños)control;

            gridDueñosUc.LlenarGrillaDueños();
        }

        #endregion

        public AgregarDueños()
        {
            ExpensasEntities context = new ExpensasEntities();
            _dueñosServ = new dueñosServ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnAceptarNuevoProveedor_Click(object sender, EventArgs e)
        {
            MostrarError(string.Empty);
            try
            {
                DueñosModel dueñoModel = new DueñosModel{
                    Nombre = txtNombreNuevo.Text.ToUpper(),
                    Apellido = txtApellidoNuevo.Text.ToUpper(),
                    Direccion = txtDireccionNuevo.Text.ToUpper(),
                    Mail = txtMail.Text.ToUpper(),
                    Telefono = txtTelefonoNuevo.Text
                };

                _dueñosServ.AddDueño(dueñoModel);
                LlenarGrillaDueños();

                txtNombreNuevo.Text = "";
                txtApellidoNuevo.Text = "";
                txtDireccionNuevo.Text = "";
                txtMail.Text = "";
                txtTelefonoNuevo.Text = "";
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
    }
}
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
    public partial class ModificarDueños : System.Web.UI.UserControl
    {
        //private IProveedoresNeg _proveedoresNeg;
        private dueñosServ _dueñosServ;

        private void LlenarGrillaDueños()
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlGridDueñosID");
            GridDueños gridDueñosUc = (GridDueños)control;

            gridDueñosUc.LlenarGrillaDueños();
        }

        public ModificarDueños()
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

        protected void btnAceptarModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ConstantesWeb.MostrarError(string.Empty, this.Page);

                DueñosModel dueño = new DueñosModel() {
                    ID = int.Parse(txtCodigoModificar.Text),
                    Nombre = txtNombreModificar.Text.ToUpper(),
                    Apellido = txtApellidoModificar.Text.ToUpper(),
                    Direccion = txtDireccionModificar.Text.ToUpper(),
                    Mail = txtMailModificar.Text.ToUpper(),
                    Telefono = txtTelefonoModificar.Text,
                };

                _dueñosServ.ModificarDueño(dueño);
                LlenarGrillaDueños();
            }
            catch (Exception ex)
            {
                ConstantesWeb.MostrarError(ex.Message, this.Page);
            }
        }

        public void MostrarDatosParaModificar(string codigo)
        {
            txtCodigoModificar.Text = codigo;

            var proveedor = _dueñosServ.GetDueñoById(codigo.ToDecimal());
            var nombre = proveedor.Nombre?.ToUpper();
            var apellido = proveedor.Apellido?.ToUpper();
            var direccion = proveedor.Direccion?.ToUpper();
            var mail = proveedor.Mail?.ToUpper();
            var telefono = proveedor.Telefono.ToUpper();

            txtNombreModificar.Text = nombre;
            txtApellidoModificar.Text = apellido;
            txtDireccionModificar.Text = direccion == "&nbsp;" ? "" : direccion;
            txtMailModificar.Text = mail == "&nbsp;" ? "" : mail;
            txtTelefonoModificar.Text = telefono == "&nbsp;" ? "" : telefono;
        }

    }
}
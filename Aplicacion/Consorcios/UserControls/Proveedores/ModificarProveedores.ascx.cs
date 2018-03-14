﻿using DAO;
using DAO.Models;
using Negocio;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSistemmas.Common;

namespace WebSistemmas.Consorcios.UserControls.Proveedores
{
    public partial class ModificarProveedores : System.Web.UI.UserControl
    {
        private IProveedoresNeg _proveedoresNeg;
        private IProveedoresServ _proveedresServ;

        private void LlenarGrillaProveedores()
        {
            ContentPlaceHolder placeHolder = Page.Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
            Control control = placeHolder.FindControl("UserControlGridProveedoresID");
            GridProveedores gridProveedoresUc = (GridProveedores)control;

            gridProveedoresUc.LlenarGrillaProveedores();
        }

        public ModificarProveedores()
        {
            ExpensasEntities context = new ExpensasEntities();
            _proveedresServ = new proveedoresServ(context);
            _proveedoresNeg = new proveedoresNeg(_proveedresServ);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAceptarModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ConstantesWeb.MostrarError(string.Empty, this.Page);

                ProveedoresModel proveedor = new ProveedoresModel() {
                    Codigo = int.Parse(txtCodigoModificar.Text),
                    Nombre = txtNombreModificar.Text,
                    Direccion = txtDireccionModificar.Text,
                    Mail = txtMailModificar.Text,
                    Telefono = txtTelefonoModificar.Text 
                };

                _proveedoresNeg.ModificarProveedor(proveedor);
                LlenarGrillaProveedores();
            }
            catch (Exception ex)
            {
                ConstantesWeb.MostrarError(ex.Message, this.Page);
            }
        }

        public void MostrarDatosParaModificar(string codigo, string nombre, string direccion, string mail, string telefono)
        {
            txtCodigoModificar.Text = codigo;
            txtNombreModificar.Text = nombre;
            txtDireccionModificar.Text = direccion == "&nbsp;" ? "" : direccion;
            txtMailModificar.Text = mail == "&nbsp;" ? "" : mail;
            txtTelefonoModificar.Text = telefono == "&nbsp;" ? "" : telefono;
        }

    }
}
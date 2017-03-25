using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicios;

namespace ServempWeb
{
    public partial class RegistrarNT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            nuestraTierra serv = new nuestraTierra();

            if (txtUsuario.Text == "")
            {
                lblError.Text = "No se ingreso el Mail";
                return;
            }

            if (txtConfirm.Text == "")
            {
                lblError.Text = "No se ingreso la confirmación del Mail";
                return;
            }

            if (txtNombre.Text == "")
            {
                lblError.Text = "No se ingreso el Nombre";
                return;
            }
            
            var modelo = serv.GuardarPadre(txtUsuario.Text,txtNombre.Text);

            if (modelo != null )
            {
                Session["Usuario"] = modelo;
                Response.Redirect("NuestraTierra.aspx", false);
            }
            else
                lblError.Text = "El Mail ingresado ya está registrado";
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginNT.aspx", false);
        }

    }
}
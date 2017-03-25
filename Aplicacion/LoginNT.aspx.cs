using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicios;

namespace ServempWeb
{
    public partial class LoginNT : System.Web.UI.Page
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

            var modelo = serv.GetPadreByMail(txtUsuario.Text);

            if (modelo.Mail != null)
            {
                Session["Usuario"] = modelo;
                Response.Redirect("NuestraTierra.aspx", false);
            }
            else
                lblError.Text = "El Mail ingresado no está registrado";
        }

        protected void Registrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarNT.aspx", false);
        }

    }
}
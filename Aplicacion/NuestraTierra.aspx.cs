using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServempWeb
{
    public partial class NuestraTierra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Preguntar si llega desde el mail
            if (Request.QueryString["mail"] != null)
            {
                string mail = Request.QueryString["mail"];

                //buscar el usuario con el ID del mail
                nuestraTierra serv = new nuestraTierra();

                Session["Usuario"] = serv.GetPadreByMail(mail);

                if (Request.QueryString["confirm"] != null)
                {
                    bool confirm = Convert.ToBoolean(Request.QueryString["confirm"]);

                    if (confirm)
                    {
                        WebSistemmas.WebService ws = new WebSistemmas.WebService();
                        int totalConfirmados = ws.GetConfirmados().Count();
                        int cantidadMaxima = Convert.ToInt32(WebConfigurationManager.AppSettings["CantidadMaxima"]);

                        if (totalConfirmados >= cantidadMaxima)
                        {
                            string message = "No se puede agregar mas de " + cantidadMaxima + " jugadores";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                            divConfirmar.Visible = false;
                            divNoVoy.Visible = false;
                        }
                        else
                        {
                            DAO.NuestraTierra.PadresModel modelo = (DAO.NuestraTierra.PadresModel)Session["Usuario"];
                            modelo.Confirmado = true;
                            Session["Usuario"] = modelo;

                            serv.Confirmar(mail);
                        }
                    }
                    else
                    {
                        DAO.NuestraTierra.PadresModel modelo = (DAO.NuestraTierra.PadresModel)Session["Usuario"];
                        modelo.Confirmado = false;
                        Session["Usuario"] = modelo;

                        serv.NoVa(mail);
                    }
                }                
            }

            if (Session["Usuario"] == null)
                Response.Redirect("LoginNT.aspx", false);
            else
            {
                DAO.NuestraTierra.PadresModel modelo = (DAO.NuestraTierra.PadresModel)Session["Usuario"];
                LlenarChat(0);

                if (!IsPostBack)
                {
                    nuestraTierra serv = new nuestraTierra();

                    lblNombre.Text = "Hola " + modelo.Nombre;
                    lblConfirmar.Text = "Si queres CONFIRMAR que vas a jugar hace click en el boton ";
                    lblNoVoy.Text = "Si queres avisar que NO vas hace click en el boton ";
                    LlenarListas();

                    Session["Pagina"] = 0;

                    Session["UltimaPagina"] = serv.GetCantPaginasChat() - 1;
                    btnPaginaAnterior.Enabled = false;

                    if ((int)Session["Pagina"] == (int)Session["UltimaPagina"])
                        btnPaginaProxima.Enabled = false;

                    if (modelo.Confirmado == true)
                    {
                        divConfirmar.Visible = false;
                        divNoVoy.Visible = true;
                    }
                    else if (modelo.Confirmado == false)
                    {
                        divConfirmar.Visible = true;
                        divNoVoy.Visible = false;
                    }
                    else
                    {
                        divConfirmar.Visible = true;
                        divNoVoy.Visible = true;
                    }
                }
            }
        }

        private void LlenarChat(int pag)
        {
            nuestraTierra serv = new nuestraTierra();

            var listChat = serv.GetChat(pag);

            string div = "<table id='tableChat'>";
            foreach (var item in listChat)
            {
                div = div + "<tr><td style=\"color: orange;\">" + item.NombrePadre + "</td></tr><tr><td>" + item.Texto + "</td></tr><tr><td style='height: 20px;'></td></tr>";
            }

            div = div + "</table>";

            divChat.InnerHtml = div;
        }

        private void LlenarListas()
        {
            nuestraTierra serv = new nuestraTierra();

            var confirmados = serv.GetConfirmados("True");
            lstConfirmados.DataSource = confirmados;
            lstConfirmados.DataBind();

            if (confirmados.Count() == 0)
                lblTotalConfirmados.Text = "No hay confirmados para el partido";
            else if (confirmados.Count() == 1)
                lblTotalConfirmados.Text = "Se confirmó 1 persona para el partido" ;
            else
                lblTotalConfirmados.Text = "Se confirmaron " + confirmados.Count().ToString () + " personas para el partido" ;
            
            lstSinConfirmar.DataSource = serv.GetConfirmados(null);
            lstSinConfirmar.DataBind();

            lstNoVan.DataSource = serv.GetConfirmados("False");
            lstNoVan.DataBind();

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            nuestraTierra serv = new nuestraTierra();

            if (Session["Usuario"] != null)
            {
                WebSistemmas.WebService ws = new WebSistemmas.WebService();
                int totalConfirmados = ws.GetConfirmados().Count();
                int cantidadMaxima = Convert.ToInt32(WebConfigurationManager.AppSettings["CantidadMaxima"]);

                if (totalConfirmados >= cantidadMaxima)
                {
                    string message = "No se puede agregar mas de " + cantidadMaxima + " jugadores";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    divConfirmar.Visible = false;
                    divNoVoy.Visible = false;                    
                }
                else
                {
                    DAO.NuestraTierra.PadresModel modelo = (DAO.NuestraTierra.PadresModel)Session["Usuario"];
                    serv.Confirmar(modelo.Mail);

                    divConfirmar.Visible = false;
                    divNoVoy.Visible = true;
                }

                LlenarListas();
            }
            else
                Response.Redirect("LoginNT.aspx", false);

        }

        protected void btnNoVoy_Click(object sender, EventArgs e)
        {
            nuestraTierra serv = new nuestraTierra();

            if (Session["Usuario"] != null)
            {
                DAO.NuestraTierra.PadresModel modelo = (DAO.NuestraTierra.PadresModel)Session["Usuario"];
                serv.NoVa(modelo.Mail);

                divConfirmar.Visible = true;
                divNoVoy.Visible = false;

                LlenarListas();
            }
            else
                Response.Redirect("LoginNT.aspx", false);
        }

        protected void btnGuardarChat_Click(object sender, EventArgs e)
        {
            nuestraTierra serv = new nuestraTierra();

            if (txtMensajeChat.Text == "")
                return;

            if (Session["Usuario"] != null)
            {
                DAO.NuestraTierra.PadresModel modelo = (DAO.NuestraTierra.PadresModel)Session["Usuario"];
                serv.GuardarMensaje(System.DateTime.Now.ToShortDateString() + " - " + txtMensajeChat.Text, modelo.Nombre);
                txtMensajeChat.Text = "";

                LlenarChat(0);
                Session["Pagina"] = 0;
                Session["UltimaPagina"] = serv.GetCantPaginasChat() - 1;
                btnPaginaAnterior.Enabled = false;
               
                if ((int)Session["Pagina"] == (int)Session["UltimaPagina"])
                    btnPaginaProxima.Enabled = false;
                else
                    btnPaginaProxima.Enabled = true;
            }
            else
                Response.Redirect("LoginNT.aspx", false);

        }

        protected void btnPaginaProxima_Click(object sender, EventArgs e)
        {
            if (Session["Pagina"] != null)
            {
                int pagina = (int)Session["Pagina"] + 1;
                LlenarChat(pagina);
                Session["Pagina"] = pagina;
                btnPaginaAnterior.Enabled = true;

                if (pagina == (int)Session["UltimaPagina"])
                    btnPaginaProxima.Enabled = false;
            }
            else
                Response.Redirect("LoginNT.aspx", false);
        }

        protected void btnPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (Session["Pagina"] != null)
            {
                int pagina = (int)Session["Pagina"] - 1;
                LlenarChat(pagina);
                Session["Pagina"] = pagina;
                btnPaginaProxima.Enabled = true;

                if (pagina == 0)
                    btnPaginaAnterior.Enabled = false;                
            }
            else
                Response.Redirect("LoginNT.aspx", false);

        }
    }
}
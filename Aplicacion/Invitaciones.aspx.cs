using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSistemmas 
{
    public partial class Invitaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            nuestraTierra serv = new nuestraTierra();

            try
            {
                lblError.Text = "";

                foreach (var item in serv.GetMails())
                {                                    
                    string mail = item ; 

                    var chat = "http://www.websistemmas.com.ar/nuestratierra.aspx?mail=" + mail ;
                    var confirm = chat + "&confirm=true";
                    var novoy = chat + "&confirm=false";

                    string FechaInvitacion = "Invitación para el dia " + txtFechaInvitacion.Text + ". Club Independiente.";
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(mail));
                    msg.From = new MailAddress("juanviglio@gmail.com");
                    msg.Subject = "Invitacion Futbol NT";
                    msg.IsBodyHtml = true;
                    msg.Body = "<label style='font-family: cursive;font-size: 24px;'>Futbol Papás Nuestra Tierra</label><br/><br/><label style='font-family: cursive;font-size: 20px;'>" + FechaInvitacion + " </label><br/><br/><br/><a style='border-style: solid;border-width : 1px 1px 1px 1px;text-decoration : none;padding : 4px;border-color : #000000;background-color: green;color: #FFFFFF;font-size: 20px;' href=" + confirm + ">Confirmar</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a style='border-style: solid;border-width : 1px 1px 1px 1px; text-decoration : none;padding : 4px;border-color : #000000;background-color: #FF0000;color: #FFFFFF;font-size: 20px;' href=" + novoy + ">No Voy</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a style='border-style: solid;border-width : 1px 1px 1px 1px;text-decoration : none;padding : 4px;border-color : #000000;background-color: #000080;color: #FFFFFF;font-size: 20px;' href=" + chat + ">Chat</a>";
                    SmtpClient clienteSmtp = new SmtpClient();
                    clienteSmtp.Host = "smtp.gmail.com";
                    clienteSmtp.Port = 587;
                    clienteSmtp.UseDefaultCredentials = false;
                    clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clienteSmtp.Credentials = new System.Net.NetworkCredential("juanviglio@gmail.com", "Fortin2014");
                    clienteSmtp.EnableSsl = true;

                    clienteSmtp.Send(msg);
                }

                lblError.Text = "Se enviaron todas las invitaciones correctamente";
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }

        protected void btnPasarA_SinConfirmar_Click(object sender, EventArgs e)
        {
            nuestraTierra serv = new nuestraTierra();

            if (serv.PasarA_SinConfirmar())
                lblError.Text = "Se enviaron todas los padres a Sin Confirmar";
            else
                lblError.Text = "No se pasaron todos los padres a Sin Confirmar por algun ERROR";
        }
    }

}

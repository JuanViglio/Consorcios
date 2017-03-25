using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class nuestraTierra
    {
        private NuestraTierraEntities context = new NuestraTierraEntities();

        #region padres
        public DAO.NuestraTierra.PadresModel GuardarPadre(string mail, string nombre)
        {
            try
            {
                if (GetPadreByMail(mail) != null)
                {
                    context.AddToPadres(new Padres { Mail = mail, Nombre = nombre, Admin = false });
                    context.SaveChanges();

                    NuestraTierra.PadresModel model = new NuestraTierra.PadresModel{Mail = mail, Nombre  = nombre,Admin = false };

                    return model;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public DAO.NuestraTierra.PadresModel GetPadreByMail(string mail)
        {
            DAO.NuestraTierra.PadresModel modelo = new DAO.NuestraTierra.PadresModel();

            var padres = context.Padres.Where(x => x.Mail == mail).FirstOrDefault();

            if (padres != null)
            {
                modelo.Mail = mail;
                modelo.Nombre = padres.Nombre;
                modelo.Password = padres.Password;
                if (padres.Confirmado != null)
                    modelo.Confirmado = padres.Confirmado;
            }

            return modelo;
        }

        public List<Padres> GetConfirmados(string estado)
        {
            List<Padres> padres;

            if (estado == null)
                padres = context.Padres.Where(x => x.Confirmado == null).ToList();
            else
            {
                bool bitEstado = Convert.ToBoolean(estado) ;

                padres = context.Padres.Where(x => x.Confirmado == bitEstado).ToList();
            }
            
            return padres;
        }

        public bool PasarA_SinConfirmar()
        {
            var padres = context.Padres.ToList();

            foreach (var item in padres)
            {
                item.Confirmado = null;
            }

            context.SaveChanges();

            return true;
        }

        public void Confirmar(string mail)
        {
            var padre = context.Padres.Where(x => x.Mail == mail).FirstOrDefault();

            padre.Confirmado = true;
            context.SaveChanges();
        }

        public void NoVa(string mail)
        {
            var padre = context.Padres.Where(x => x.Mail == mail).FirstOrDefault();

            padre.Confirmado = false;
            context.SaveChanges();
        }

        public IEnumerable<string> GetMails()
        {
            var Mails = context.Padres.Where(x => x.Mail != "carlitosSenior@sinmail.com" && x.Mail != "guillermobelsito@gmail.com").ToList().Select(x => x.Mail);

            //List<string> Mails = new List<string>();
            
            //Mails.Add("elkiman@gmail.com");
            //Mails.Add("fagtan18@gmail.com");

            return Mails;
        }

        #endregion


        #region chat       
        public int GetCantPaginasChat()
        {
            int cant = context.Chat.Count();

            int entero = cant / 5;
            if ((cant % 5) > 0)
                entero ++;

            if (entero == 0) entero++;

            return entero;
        }

        public IEnumerable<Chat> GetChat(int c)
        {
            var chat = context.Chat.ToList();
                        
            var chatQuery = context.Chat.OrderByDescending(x => x.idChat).Skip(5 * c).Take(5);

            return chatQuery;
        }

        public void GuardarMensaje(string mensaje, string nombrePadre)
        {
            try
            {
                context.AddToChat(new Chat { NombrePadre = nombrePadre , Texto = mensaje});
                context.SaveChanges();
            }
            catch 
            {
                               
            }
        }

        #endregion
    }
}

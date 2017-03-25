using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;

namespace WebSistemmas
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://websistemmas.com.ar")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetNombre(string ID)
        {
            NuestraTierraEntities context = new NuestraTierraEntities();

            Padres padre = context.Padres.Where(x => x.Mail == ID).FirstOrDefault();

            if (padre != null)
                return "Hola " + padre.Nombre;
            else
                return "No se encontró el mail " + ID;

        }

        [WebMethod]
        public List<string> GetConfirmados()
        {
            NuestraTierraEntities context = new NuestraTierraEntities();
            List<string> lista = new List<string>();

            List<Padres> padres = context.Padres.Where(x => x.Confirmado == true).ToList();

            foreach (var item in padres)
            {
                lista.Add(item.Nombre);
            }

            return lista;
        }

        [WebMethod]
        public List<string> Confirmar(string ID, string Confirma)
        {
            NuestraTierraEntities context = new NuestraTierraEntities();

            int cantidadConfirmados = context.Padres.Where(x => x.Confirmado == true).Count();
            int cantidadMaxima = Convert.ToInt32(WebConfigurationManager.AppSettings["CantidadMaxima"]);

            List<string> lista = new List<string>();


            Padres padre = context.Padres.Where(x => x.Mail == ID).FirstOrDefault();

            if ((Confirma == "True" && cantidadConfirmados < cantidadMaxima) || Confirma == "False")
            {
                padre.Confirmado =  Confirma == "True" ? true : false;
                context.SaveChanges();

                List<Padres> padres = context.Padres.Where(x => x.Confirmado == true).ToList();

                foreach (var item in padres)
                {
                    lista.Add(item.Nombre);
                }
            }                    

            return lista;
        }
    }
}

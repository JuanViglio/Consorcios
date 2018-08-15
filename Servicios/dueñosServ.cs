using DAO;
using DAO.Models;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class dueñosServ
    {
        private ExpensasEntities _context;

        public dueñosServ()
        {
            _context = new ExpensasEntities();
        }

        public List<DueñosModel> GetDueños ()
        {
            List<DueñosModel> dueñosModel = new List<DueñosModel>();
            var dueños = _context.Dueños.OrderBy(x => x.Apellido).ToList();

            foreach (var item in dueños)
            {
                dueñosModel.Add(new DueñosModel() {
                    Apellido_y_Nombre = item.Apellido + " " + item.Nombre,
                    Apellido = item.Apellido,
                    Nombre = item.Nombre,
                    Telefono = item.Telefono,
                    Mail = item.Mail,
                    Direccion = item.Direccion,
                    ID = item.ID
                });
            }

            return dueñosModel;
        }
    }
}

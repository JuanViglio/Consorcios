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

        public Dueños GetDueñoById(decimal idDueño)
        {
            return _context.Dueños.Where(x => x.ID == idDueño).FirstOrDefault();
        }

        public List<DueñosModel> GetDueñosCombo()
        {
            var dueñosModel = new List<DueñosModel>() {
                new DueñosModel() {
                    ID = 0,
                    Apellido_y_Nombre = "Seleccione un Propietario" }
            };

            var consorcios = _context.Dueños.ToList();

            foreach (var item in consorcios)
            {
                dueñosModel.Add(new DueñosModel()
                {
                    ID = item.ID,
                    Apellido_y_Nombre = item.Apellido + " " + item.Nombre
                });
            }

            return dueñosModel;
        }

        public void AddDueño (DueñosModel dueñoModel)
        {
            _context.AddToDueños(new Dueños
            {
                Nombre = dueñoModel.Nombre,
                Apellido = dueñoModel.Apellido,
                Mail = dueñoModel.Mail,
                Telefono = dueñoModel.Telefono,
                Direccion = dueñoModel.Direccion
            });

            _context.SaveChanges();
        }

        public void EliminarDueño(int idDueño)
        {
            var dueño = _context.Dueños.Where(x => x.ID == idDueño).FirstOrDefault();
            var uf = _context.UnidadesFuncionales.ToList();

            if (dueño != null)
            {
                if (dueño.UnidadesFuncionales.Any())
                {
                    throw new System.Exception("No se puede eliminar un Dueño que posea Unidades Funcionales.");
                }

                _context.DeleteObject(dueño);
                _context.SaveChanges();
            } 
        }

        public void ModificarDueño (DueñosModel dueñoModificado)
        {
            var dueño = GetDueñoById(dueñoModificado.ID);
            dueño.Nombre = dueñoModificado.Nombre;
            dueño.Apellido = dueñoModificado.Apellido;
            dueño.Direccion = dueñoModificado.Direccion;
            dueño.Telefono = dueñoModificado.Telefono;
            dueño.Mail = dueñoModificado.Mail;
            _context.SaveChanges();
        }
    }
}

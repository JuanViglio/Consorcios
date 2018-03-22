using DAO;
using DAO.Models;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class proveedoresServ : IProveedoresServ
    {
        private ExpensasEntities _context;

        public proveedoresServ(ExpensasEntities context)
        {
            _context = context;
        }

        public IEnumerable<ProveedoresModel> GetProveedores(bool ninguno = false)
        {
            List<ProveedoresModel> proveedores = new List<ProveedoresModel>();

            if (ninguno)
            {
                proveedores.Add(new ProveedoresModel() { Codigo = 0, Nombre = "Ninguno" });
            }

            foreach (var item in _context.Proveedores.ToList())
            {
                proveedores.Add(new ProveedoresModel() {
                    Codigo = item.ID,
                    Nombre = item.Nombre,
                    Direccion = item.Direccion,
                    Mail = item.Mail,
                    Saldo = item.Saldo.Value,
                    Telefono = item.Telefono,
                    Tipo = item.Tipo });
            } 

            return proveedores; 
        }

        public void AgregarProveedor(string nombre, string direccion, string mail, string tipo)
        {
            try
            {
                _context.AddToProveedores(new Proveedores { Nombre = nombre, Direccion = direccion, Mail = mail, Tipo = tipo, Saldo = 0 });
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("No se pudo agregar el Proveedor");
            }
        }

        public void EliminarProveedor(int idProveedor)
        {
            try
            {
                Proveedores proveedor = _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
                _context.DeleteObject(proveedor);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("No se pudo Eliminar el Proveedor");
            }
        }

        public void ModificarProveedor(ProveedoresModel proveedorModel)
        {
            try
            {
                var proveedor = _context.Proveedores.Where(x => x.ID == proveedorModel.Codigo).FirstOrDefault();

                proveedor.Nombre = proveedorModel.Nombre;
                proveedor.Direccion = proveedorModel.Direccion;
                proveedor.Mail = proveedorModel.Mail;
                proveedor.Telefono = proveedorModel.Telefono;
                proveedor.Tipo = proveedorModel.Tipo;

                _context.SaveChanges();
            }
            catch 
            {
                throw new Exception ("No se pudo Modificar el Proveedor") ;
            }


        }

        public string GetTipo (decimal id)
        {
            return _context.Proveedores.Where(x => x.ID == id).FirstOrDefault().Tipo;
        }
    }
}

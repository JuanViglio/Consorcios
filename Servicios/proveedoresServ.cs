using DAO;
using DAO.Models;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class proveedoresServ : IProveedoresServ
    {
        private ExpensasEntities _context;

        private decimal GetSaldo(decimal idProveedor)
        {
            var debe = _context.ProveedoresCtaCte.Where(x => x.Proveedores.ID == idProveedor).Sum(x=> x.Debe).GetValueOrDefault();
            var haber = _context.ProveedoresCtaCte.Where(x => x.Proveedores.ID == idProveedor).Sum(x => x.Haber).GetValueOrDefault();
            return debe - haber;
        }

        public void ActualizarSaldo(decimal idProveedor)
        {
            var proveedor = _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
            proveedor.Saldo = GetSaldo(idProveedor);
            _context.SaveChanges();
        }

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

        public decimal AddHaber (decimal importe, decimal idProveedor, string tipoGasto, string detalle)
        {
            ProveedoresCtaCte registro = new ProveedoresCtaCte();

            registro.Proveedores = _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
            registro.Haber = importe;
            registro.TipoGasto = tipoGasto;
            registro.Detalle = detalle;
            registro.Fecha = DateTime.Now;

            _context.AddToProveedoresCtaCte(registro);
            var ctaCteId = _context.SaveChanges();

            ActualizarSaldo(idProveedor);

            return registro.ID;        
        }

        public void AddDebe(decimal importe, decimal idProveedor, decimal idGasto, string tipoGasto, string detalle)
        {
            ProveedoresCtaCte registro = new ProveedoresCtaCte();

            registro.Proveedores = _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
            registro.Debe = importe;
            registro.Gasto_ID = idGasto;
            registro.TipoGasto = tipoGasto;
            registro.Detalle = detalle;

            _context.AddToProveedoresCtaCte(registro);
            _context.SaveChanges();

            ActualizarSaldo(idProveedor);
        }

        public void DeleteHaber(decimal idGasto)
        {
            var ctaCteId = _context.GastosExtDetalle.Where(x => x.ID == idGasto).FirstOrDefault().ProveedoresCtaCte_ID.Value;
            var haber = _context.ProveedoresCtaCte.Where(x => x.ID == ctaCteId).FirstOrDefault();

            var idProveedor = (from C in _context.ProveedoresCtaCte
                                join P in _context.Proveedores
                                on C.Proveedores.ID equals P.ID
                                where C.ID == ctaCteId
                                select C.Proveedores.ID).FirstOrDefault();


            _context.DeleteObject(haber);
            _context.SaveChanges();

            ActualizarSaldo(idProveedor);
        }

        public IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor)
        {
            return _context.ProveedoresCtaCte.Where(x => x.Proveedores.ID == idProveedor).ToList().OrderByDescending(x => x.ID);
        }
    }
}

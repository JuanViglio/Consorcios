﻿using DAO;
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
            return haber - debe;
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

        public IEnumerable<ProveedoresModel> GetProveedores(string nombre = "", bool ninguno = false)
        {
            List<ProveedoresModel> proveedores = new List<ProveedoresModel>();

            if (ninguno)
            {
                proveedores.Add(new ProveedoresModel() { Codigo = 0, Nombre = "Ninguno" });
            }

            foreach (var item in _context.Proveedores.Where(x => x.Nombre.Contains(nombre) || nombre == string.Empty).OrderBy(x => x.Nombre).ToList())
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
                _context.AddToProveedores(new Proveedores { Nombre = nombre.ToUpper(), Direccion = direccion.ToUpper(), Mail = mail.ToUpper(), Tipo = tipo, Saldo = 0 });
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

        public void EliminarProveedorCtaCte(int idProveedorCtaCte)
        {
            try
            {
                List<Proveedores> proveedores = _context.Proveedores.ToList();
                ProveedoresCtaCte proveedorCtaCte = _context.ProveedoresCtaCte.Where(x => x.ID == idProveedorCtaCte).FirstOrDefault();                

                var idProveedor = proveedorCtaCte.Proveedores.ID;
                _context.DeleteObject(proveedorCtaCte);
                _context.SaveChanges();

                ActualizarSaldo(idProveedor);
            }
            catch
            {
                throw new Exception("No se pudo Eliminar el Movimiento del Proveedor");
            }
        }

        public void ModificarProveedor(ProveedoresModel proveedorModel)
        {
            try
            {
                var proveedor = _context.Proveedores.Where(x => x.ID == proveedorModel.Codigo).FirstOrDefault();

                proveedor.Nombre = proveedorModel.Nombre.ToUpper();
                proveedor.Direccion = proveedorModel.Direccion.ToUpper();
                proveedor.Mail = proveedorModel.Mail.ToUpper();
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

        public void AddDebe(DateTime fecha, decimal importe, decimal idProveedor, decimal? ordenDePago, string detalle)
        {
            ProveedoresCtaCte registro = new ProveedoresCtaCte();

            registro.Proveedores = _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
            registro.Debe = importe;
            registro.OrdenDePago = ordenDePago;
            registro.Detalle = detalle;
            registro.Fecha = fecha;

            _context.AddToProveedoresCtaCte(registro);
            _context.SaveChanges();

            ActualizarSaldo(idProveedor);
        }

        public void DeleteHaber(decimal idGasto)
        {
            var ProveedorCtaCteId = _context.GastosEvExt.Where(x => x.ID == idGasto).FirstOrDefault().ProveedoresCtaCte_ID.Value;

            if (ProveedorCtaCteId > 0)
            {
                var haber = _context.ProveedoresCtaCte.Where(x => x.ID == ProveedorCtaCteId).FirstOrDefault();

                var idProveedor = (from C in _context.ProveedoresCtaCte
                                    join P in _context.Proveedores
                                    on C.Proveedores.ID equals P.ID
                                    where C.ID == ProveedorCtaCteId
                                   select C.Proveedores.ID).FirstOrDefault();


                _context.DeleteObject(haber);
                _context.SaveChanges();

                ActualizarSaldo(idProveedor);
            }
        }

        public IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor)
        {
            return _context.ProveedoresCtaCte.Where(x => x.Proveedores.ID == idProveedor).ToList().OrderByDescending(x => x.ID);
        }

        public Proveedores GetProveedorById (decimal idProveedor)
        {
            return _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
        }

        public void ModificarProveedorCtaCte (decimal ctaCte_Id, string detalle, decimal importeCompra, decimal idProveedor)
        {
            var proveedorCtaCte = _context.ProveedoresCtaCte.Where(x => x.ID == ctaCte_Id).FirstOrDefault();

            if (proveedorCtaCte != null)
            {
                proveedorCtaCte.Detalle = detalle;
                proveedorCtaCte.Haber = importeCompra;
                proveedorCtaCte.Proveedores = _context.Proveedores.Where(x => x.ID == idProveedor).FirstOrDefault();
                _context.SaveChanges();
            }
        }

    }
}

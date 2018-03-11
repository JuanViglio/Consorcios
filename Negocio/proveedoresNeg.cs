﻿using DAO;
using Negocio.Interfaces;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class proveedoresNeg : IProveedoresNeg
    {
        readonly IProveedoresServ _proveedoresServ;

        public proveedoresNeg(IProveedoresServ proveedoresServ)
        {
            _proveedoresServ = proveedoresServ;
        }

        public IEnumerable<Proveedores> GetProveedores()
        {
            return _proveedoresServ.GetProveedores();
        }

        public void AgregarProveedor(string nombre, string direccion, string mail)
        {
            try
            {
                _proveedoresServ.AgregarProveedor(nombre, direccion, mail);    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarProveedor(int idProveedor)
        {
            try
            {
                _proveedoresServ.EliminarProveedor(idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

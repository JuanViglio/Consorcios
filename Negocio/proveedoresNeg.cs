using DAO;
using DAO.Models;
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

        public IEnumerable<ProveedoresModel> GetProveedores(bool ninguno = false)
        {
            return _proveedoresServ.GetProveedores(ninguno);
        }

        public void AgregarProveedor(string nombre, string direccion, string mail, string tipo)
        {
            try
            {
                _proveedoresServ.AgregarProveedor(nombre, direccion, mail, tipo);    
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

        public void ModificarProveedor(ProveedoresModel proveedorModel)
        {
            try
            {
                _proveedoresServ.ModificarProveedor(proveedorModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTipo (decimal id)
        {
            return _proveedoresServ.GetTipo(id);
        }
    }
}

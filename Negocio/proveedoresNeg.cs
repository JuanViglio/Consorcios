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

        public IEnumerable<ProveedoresModel> GetProveedores(string nombre = "", bool ninguno = false)
        {
            return _proveedoresServ.GetProveedores(nombre, ninguno);
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

        public void EliminarProveedorCtaCte(int idProveedorCtaCte)
        {
            try
            {
                _proveedoresServ.EliminarProveedorCtaCte(idProveedorCtaCte);
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

        public void AddDebe (string fecha, string importe, decimal idProveedor, string ordenDePago, string detalle)
        {
            DateTime dteFecha;
            decimal dcmImporte;
            decimal dcmOrdenDePago;

            if (!DateTime.TryParse(fecha, out dteFecha))
            {
                throw new Exception("No se ingreso la Fecha correctamente");
            }
            else if (!Decimal.TryParse(importe, out dcmImporte))
            {
                throw new Exception("No se ingreso el Importe correctamente");
            }
            else if(detalle == string.Empty)
            {
                throw new Exception("No se ingreso el Detalle");
            }
            else if (!Decimal.TryParse(ordenDePago, out dcmOrdenDePago))
            {
                throw new Exception("No se ingreso la Orden de Pago correctamente");
            }

            _proveedoresServ.AddDebe(dteFecha, dcmImporte, idProveedor, dcmOrdenDePago, detalle);
        }

        public decimal AddHaber(decimal importe, decimal idProveedor, string tipoGasto, string detalle)
        {
            return _proveedoresServ.AddHaber(importe, idProveedor, tipoGasto, detalle);
        }

        public void DeleteHaber(decimal idGasto)
        {
            _proveedoresServ.DeleteHaber(idGasto);
        }

        public IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor)
        {
            return _proveedoresServ.GetCtaCte(idProveedor);
        }

        public void ActualizarSaldo(decimal idProveedor)
        {
            _proveedoresServ.ActualizarSaldo(idProveedor);
        }
    }
}

using DAO;
using DAO.Models;
using System;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface IProveedoresNeg
    {
        IEnumerable<ProveedoresModel> GetProveedores(bool ninguno = false);

        void AgregarProveedor(string Nombre, string Direccion, string Mail, string tipo);

        void EliminarProveedor(int idProveedor);

        void ModificarProveedor(ProveedoresModel proveedorModel);

        string GetTipo(decimal id);

        void AddDebe(string fecha, string importe, decimal idProveedor, string ordenDePago, string detalle);

        void DeleteHaber(decimal idGasto);

        decimal AddHaber(decimal importe, decimal idProveedor, string tipoGasto, string detalle);

        IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor);

        void ActualizarSaldo(decimal idProveedor);
    }
}

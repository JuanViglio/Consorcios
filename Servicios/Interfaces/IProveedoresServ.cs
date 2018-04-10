using DAO;
using DAO.Models;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IProveedoresServ
    {
        IEnumerable<ProveedoresModel> GetProveedores(bool ninguno = false);

        void AgregarProveedor(string nombre, string direccion, string mail, string tipo);

        void EliminarProveedor(int idProveedor);

        void ModificarProveedor(ProveedoresModel proveedorModel);

        string GetTipo(decimal id);

        decimal AddHaber(decimal importe, decimal idProveedor, string tipoGasto, string detalle);

        void DeleteHaber(decimal idGasto);

        void AddDebe(decimal importe, decimal idProveedor, decimal idGasto, string tipoGasto, string detalle);

        IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor);

        void ActualizarSaldo(decimal idProveedor);
    }
}

using DAO;
using DAO.Models;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IProveedoresServ
    {
        IEnumerable<Proveedores> GetProveedores();

        void AgregarProveedor(string nombre, string direccion, string mail);

        void EliminarProveedor(int idProveedor);

        void ModificarProveedor(ProveedoresModel proveedorModel);
    }
}

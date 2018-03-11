using DAO;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface IProveedoresNeg
    {
        IEnumerable<Proveedores> GetProveedores();

        void AgregarProveedor(string Nombre, string Direccion, string Mail);

        void EliminarProveedor(int idProveedor);
    }
}

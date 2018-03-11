using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IProveedoresServ
    {
        IEnumerable<Proveedores> GetProveedores();

        void AgregarProveedor(string nombre, string direccion, string mail);

        void EliminarProveedor(int idProveedor);
    }
}

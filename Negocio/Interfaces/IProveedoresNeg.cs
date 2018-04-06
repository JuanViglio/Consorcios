﻿using DAO;
using DAO.Models;
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

        void AddDebe(decimal importe, decimal idProveedor, decimal idGasto, string tipoGasto, string detalle);

        void AddHaber(decimal importe, decimal idProveedor, decimal idGasto, string tipoGasto, string detalle);

        IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor);
    }
}

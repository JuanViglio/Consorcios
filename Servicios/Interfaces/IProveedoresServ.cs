﻿using DAO;
using DAO.Models;
using System;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IProveedoresServ
    {
        IEnumerable<ProveedoresModel> GetProveedores(string nombre = "", bool ninguno = false);

        void AgregarProveedor(string nombre, string direccion, string mail, string tipo);

        void EliminarProveedor(int idProveedor);

        void EliminarProveedorCtaCte(int idProveedorCtaCte);

        void ModificarProveedor(ProveedoresModel proveedorModel);

        string GetTipo(decimal id);

        decimal AddHaber(decimal importe, decimal idProveedor, string tipoGasto, string detalle);

        void DeleteHaber(decimal idGasto);

        void AddDebe(DateTime fecha, decimal importe, decimal idProveedor, decimal? ordenDePago, string detalle);

        IEnumerable<ProveedoresCtaCte> GetCtaCte(decimal idProveedor);

        void ActualizarSaldo(decimal idProveedor);

        Proveedores GetProveedorById(decimal idProveedor);

        void ModificarProveedorCtaCte(decimal ctaCte_Id, string detalle, decimal importeCompra, decimal idProveedor);
    }
}

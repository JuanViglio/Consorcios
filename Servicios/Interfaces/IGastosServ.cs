﻿using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IGastosServ
    {
        void DeleteDetalle(decimal idExpensaDetalle);

        void DeleteGastoEvOrdinario(decimal idGasto);

        void DeleteGastoEvExtraordinario(decimal idGasto);

        List<Gastos> GetDetalleGastos(int tipoGasto, string detalle);

        List<GastosOrdinariosModel> GetDetalleGastosCombo(int tipoGasto);

        List<Gastos> AddGasto(int idTipoGasto, string detalleGasto);

        Gastos GetGastoByNombre(string nombre);

        List<Gastos> UpdateGasto(decimal idGasto, string Detalle, int idTipoGasto);
    }
}

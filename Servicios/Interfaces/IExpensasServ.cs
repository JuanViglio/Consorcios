﻿using DAO;
using System;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IExpensasServ
    {
        void AgregarExpensaDetalle(int IdExpensa, string Detalle, decimal Importe, int TipoGasto, decimal IdGasto);

        void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, decimal Importe);

        void AgregarGastoEvOrdinario(int IdExpensa, string Detalle, decimal Importe, int TipoGasto);

        void ModificarGastoEvOrdinario(int IdGasto, string Detalle, decimal Importe);

        void AgregarGastoExtraordinario(int IdExpensa, string Detalle, decimal Importe);

        void ModificarGastoExtraordinario(int IdExpensaDetalle, string Detalle, decimal Importe);

        void ActualizarTotalGastosEvOrdinarios(decimal idExpensa);

        void ActualizarTotalGastosEvExtraordinarios(decimal idExpensa);

        decimal GetTotalGastosOrdinarios(int idExpensa);

        IEnumerable<GastosOrdinariosModel> GetGastosOrdinarios(int ExpensaID);

        decimal GetTotalGastosExtraordinarios(int IdExpensa);

        bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar);

        ExpensasDetalle GetExpensaDetalle(int ExpensaID, int GastoID);

        Expensas GetUltimaExpensa(string IdConsorcio);

        void GuardarUltimoTotal(int idExpensa, decimal total);

        string GetConsorcio(decimal IdExpensa);
    }
}

﻿using System.Globalization;

namespace WebSistemmas.Common
{
    public static class Constantes
    {
        public const string TotalGastoEvOrdinarios = "TOTAL DE GASTOS EVENTUALES ORDINARIOS";
        public const string TotalGastoEvExtraordinarios = "TOTAL DE GASTOS EVENTUALES EXTRAORDINARIOS";
        public const string FondoPrevisionOrdinario = "FONDO DE PREVISION MENSUAL ORDINARIO";
        public const string FondoPrevisionExtraordinario = "FONDO DE PREVISION MENSUAL EXTRAORDINARIO";
        public const int GastoTipoOrdinario = 1;
        public const int GastoTipoEvOrdinario = 2;
        public const int GastoTipoEvExtraordinario = 3;
        public const string EstadoAceptado = "Aceptado";
        public const string ErrorFaltaDetalle = "No se ingreso el Detalle";
        public const string ErrorFaltaImporte = "No se ingreso el Importe correctamente";
        public const string ErrorFaltaPeriodo = "No existe el Periodo";

        public static decimal GetDecimalFromCurrency (string valor)
        {
            decimal gastoDecimal;
            decimal.TryParse(valor, NumberStyles.Currency, new CultureInfo("en-US"), out gastoDecimal);
            return gastoDecimal;
        }
    }
}
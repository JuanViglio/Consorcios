using System.Globalization;

namespace WebSistemmas.Common
{
    public static class Constantes
    {
        public const string TotalGastoEvOrdinarios = "TOTAL DE GASTOS EVENTUALES ORDINARIOS";
        public const string TotalGastoEvExtraordinarios = "TOTAL DE GASTOS EVENTUALES EXTRAORDINARIOS";
        public const string FondoPrevisionOrdinario = "FONDO DE PREVISION MENSUAL ORDINARIO";
        public const string FondoPrevisionExtraordinario = "FONDO DE PREVISION MENSUAL EXTRAORDINARIO";

        public static decimal GetDecimalFromCurrency (string valor)
        {
            decimal gastoDecimal;
            decimal.TryParse(valor, NumberStyles.Currency, new CultureInfo("en-US"), out gastoDecimal);
            return gastoDecimal;
        }
    }
}
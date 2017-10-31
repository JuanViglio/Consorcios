using System.Globalization;

namespace WebSistemmas.Common
{
    public static class Constantes
    {
        public static decimal GetDecimalFromCurrency (string valor)
        {
            decimal gastoDecimal;
            decimal.TryParse(valor, NumberStyles.Currency, new CultureInfo("en-US"), out gastoDecimal);
            return gastoDecimal;
        }
    }
}
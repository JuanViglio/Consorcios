using System.Globalization;
using System.Web.UI;

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
        public const string ErrorFaltaGasto = "No se selecciono el Gasto";
        public const string ErrorImporteCero = "No se puede guardar el Importe 0";
        public const string PrecioComprayVentaDistintos = "Precio de Compra y Venta distintos";
        public const string PrecioComprayVentaIguales = "Precio de Compra y Venta iguales";
        public const string GastoEvOrd = "EvOrd";
        public const string GastoEvExt = "EvExt";
        public const string SeguroIC = "SEGURO IC";
        public const string SeguroAP = "SEGURO AP";
        public const string TipoSeguroAP = "AP";
        public const string TipoSeguroIC = "IC";
        public const string EstadoAdeudado = "ADEUDADO";

        public static decimal GetDecimalFromCurrency (string valor)
        {
            decimal gastoDecimal;
            decimal.TryParse(valor, NumberStyles.Currency, new CultureInfo("en-US"), out gastoDecimal);
            return gastoDecimal;
        }
    }
}
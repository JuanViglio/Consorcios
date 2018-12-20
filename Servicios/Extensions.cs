namespace Servicios
{
    public static class Extensions
    {
        public static bool IsNumeric(this string s)
        {
            decimal output;
            return decimal.TryParse(s, out output);
        }

        public static decimal ToDecimal (this string s)
        {
            decimal output;
            string valor = s.Replace('.',',');
            if (decimal.TryParse(valor, out output))
                return output;
            else
                return 0;
        }

        public static int ToInt(this string s)
        {
            int output;
            if (int.TryParse(s, out output))
                return output;
            else
                return 0;
        }

    }
}

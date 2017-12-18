namespace Servicios
{
    public static class Extensions
    {
        public static bool IsNumeric(this string s)
        {
            decimal output;
            return decimal.TryParse(s, out output);
        }
    }
}

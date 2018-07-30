namespace DAO
{
    public class UnidadesFuncionalesModel
    {
        public decimal ID { get; set; }
        public string Departamento { get; set; }
        public string  Apellido { get; set; }
        public string Nombre { get; set; }
        public decimal? Coeficiente { get; set; }
        public decimal PagoId { get; set; }
        public string Direccion { get; set; }
        public string UF { get; set; }
        public string Cochera { get; set; }
        public bool Aplicar { get; set; }
        public string PeriodoDetalle { get; set; }
    }
}

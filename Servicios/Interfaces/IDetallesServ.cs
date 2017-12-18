namespace Servicios.Interfaces
{
    public interface IDetallesServ
    {
        void GuardarDetalle(string detalle, string idConsorcio, decimal idGasto);

        string GetDetalle(string idConsorcio, decimal idGasto);
    }
}

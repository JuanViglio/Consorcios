namespace Servicios.Interfaces
{
    public interface IGastosServ
    {
        void DeleteDetalle(decimal idExpensaDetalle);

        void DeleteGastoEvOrdinario(decimal idGasto);

        void DeleteGastoEvExtraordinario(decimal idGasto);
    }
}

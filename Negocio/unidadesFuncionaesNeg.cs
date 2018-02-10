using DAO;
using Servicios.Interfaces;
using System.Collections.Generic;

namespace Negocio
{
    public class unidadesFuncionaesNeg
    {
        readonly IUnidadesServ _unidadesServ;

        public unidadesFuncionaesNeg (IUnidadesServ unidadesServ)
        {
            _unidadesServ = unidadesServ;
        }

        public List<UnidadesFuncionalesModel> GetPagosConCochera(string idConsorcio, int periodoNumerico, bool agregarValorCochera)
        {
            return _unidadesServ.GetPagosConCochera(idConsorcio, periodoNumerico, agregarValorCochera);
        }
    }
}

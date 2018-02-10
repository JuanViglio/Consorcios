using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IUnidadesServ
    {
        IEnumerable<UnidadesFuncionalesModel> GetUnidadesFuncionales(string consorciosID);

        List<UnidadesFuncionales> GetAllUnidadesFuncionales();

        IEnumerable<UnidadesFuncionalesModel> ModificarUnidades(string idConsorcio, int idUF, string departamento, string idUf, string apellido, string nombre, decimal coeficiente, string cochera);

        IEnumerable<UnidadesFuncionalesModel> AgregarUnidad(string idConsorcio, string idUf, string departamento, string apellido, string nombre, decimal coeficiente, string cochera);

        UnidadesFuncionales GetUnidadFuncional(string consorciosID, string UF);

        List<UnidadesFuncionalesModel> GetPagos(string consorciosID, int Periodo);

        List<UnidadesFuncionalesModel> GetPagosConCochera(string consorciosID, int Periodo, bool agregarValorCochera);
    }
}

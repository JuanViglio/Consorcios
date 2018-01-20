using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IUnidadesServ
    {
        List<UnidadesFuncionales> GetUnidadesFuncionales(string consorciosID);

        List<UnidadesFuncionales> ModificarUnidades(string idConsorcio, int idUF, string departamento, string idUf, string apellido, string nombre, decimal coeficiente);

        List<UnidadesFuncionales> AgregarUnidad(string idConsorcio, string idUf, string departamento, string apellido, string nombre, decimal coeficiente);

        UnidadesFuncionales GetUnidadFuncional(string consorciosID, string UF);
    }
}

using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios.Interfaces
{
    public interface IUnidadesServ
    {
        List<UnidadesFuncionales> GetUnidadesFuncionales(string consorciosID);

        List<UnidadesFuncionales> ModificarUnidades(string idConsorcio, int idUF, string idUf, string dueño, decimal coeficiente);

        List<UnidadesFuncionales> AgregarUnidad(string idConsorcio, string idUf, string dueño, decimal coeficiente);
    }
}

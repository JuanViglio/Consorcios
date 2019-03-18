using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IUnidadesServ
    {
        IEnumerable<UnidadesFuncionalesModel> GetUnidadesFuncionales(string consorciosID, string filtro);

        IEnumerable<UnidadesFuncionalesModel> GetUnidadesFuncionalesCombo(string consorciosID);

        IEnumerable<UnidadesFuncionalesModel> GetUFByPropietarioId(decimal propietarioID);

        List<UnidadesFuncionales> GetAllUnidadesFuncionales();

        IEnumerable<UnidadesFuncionalesModel> ModificarUnidades(string idConsorcio, int idUF, string departamento, string idUf, decimal coeficiente, string cochera, decimal idDueño);

        IEnumerable<UnidadesFuncionalesModel> AgregarUnidad(string idConsorcio, string idUf, string departamento, decimal coeficiente, string cochera, decimal idDueño);

        UnidadesFuncionales GetUnidadFuncional(string consorciosID, string UF);

        List<UnidadesFuncionalesModel> GetPagos(string consorciosID, int Periodo);

        List<UnidadesFuncionalesModel> GetPagosConCochera(string consorciosID, int Periodo, bool agregarValorCochera);

        List<UnidadesFuncionalesCtaCte> GetCtaCte(decimal idUF);

        void AddHaber(UnidadesFuncionalesCtaCte UFctaCte);

        void PagarExpensa(decimal importe, decimal idPago);
    }
}

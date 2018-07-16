using DAO;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface IUnidadesFuncionalesNeg
    {
        List<UnidadesFuncionalesCtaCte> GetCtaCte(decimal idUF);
    }
}

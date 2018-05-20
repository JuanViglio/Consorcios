using DAO;
using DAO.Models;
using System;
using System.Collections.Generic;

namespace Negocio.Interfaces
{
    public interface ISegurosNeg
    {
        IEnumerable<SegurosModel> GetSeguros();

        void GuardarSeguro(SegurosModel seguroModelo, List<SeguroDetalleModel> segurosDetalleModelo);

        void Validar(string compañia, string poliza, string idConsorcios, string cantCuotas, string cuotasDeGracia, string importe);

        SegurosModel GetSeguroModelo(string compañia, string poliza, string idConsorcios, string cantCuotas, string cuotasDeGracia, string importe, DateTime dteFechaInicio, DateTime dteFechaFin, string tipo);

        List<SeguroDetalleModel> GetSeguroDetalleModelo(string cantCuotas, DateTime dteFechaInicio, string cuotasDeGracia, string importe);

        SeguroActivoModel GetSeguroByConsorcio(int idExpensa, string idConsorcio, string tipo);
    }
}

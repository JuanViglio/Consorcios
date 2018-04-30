using DAO;
using DAO.Models;
using Negocio.Interfaces;
using Servicios.Interfaces;
using System.Collections.Generic;

namespace Negocio
{
    public class segurosNeg : ISegurosNeg
    {
        readonly ISegurosServ _segurosServ;
        readonly IConsorciosServ _consorciosServ;

        public segurosNeg(ISegurosServ segurosServ, IConsorciosServ consorciosServ)
        {
            _segurosServ = segurosServ;
            _consorciosServ = consorciosServ;
        }

        public IEnumerable<SegurosModel> GetSeguros()
        {
            return _segurosServ.GetSeguros();
        }

        public void GuardarSeguro(SegurosModel seguroModelo, List<SeguroDetalleModel> segurosDetalleModelo)
        {
            Consorcios consorcio = _consorciosServ.GetConsorcioById(seguroModelo.Consorcio);
            Seguros seguro = new Seguros();

            seguro.CantCuotas = seguroModelo.CantCuotas;
            seguro.CantCuotasEn0 = seguroModelo.CantCuotas0;
            seguro.Compañia = seguroModelo.Compañia;
            seguro.Poliza = seguroModelo.Poliza;
            seguro.Tipo = seguroModelo.Tipo;
            seguro.FechaInicio = seguroModelo.FechaInicio;
            seguro.FechaFin = seguroModelo.FechaFin;
            seguro.Estado = "CREADO";
            seguro.Consorcios = consorcio;

            foreach (var item in segurosDetalleModelo)
            {
                seguro.SegurosDetalle.Add(new SegurosDetalle()
                {
                    Cuota = item.Cuota,
                    Importe = item.Importe,
                    Periodo = item.Periodo
                });
            }

            _segurosServ.GuardarSeguros(seguro);
        }
    }
}

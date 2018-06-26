using DAO;
using DAO.Models;
using Negocio.Interfaces;
using Servicios;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using WebSistemmas.Common;

namespace Negocio
{
    public class segurosNeg : ISegurosNeg
    {
        readonly ISegurosServ _segurosServ;
        readonly IConsorciosServ _consorciosServ;
        readonly IExpensasServ _expensasServ;

        #region Metodos Privados
        private bool BuscarSeguroActivo(string idConsorcio, string tipo)
        {
            return _segurosServ.GetSeguroActivo(idConsorcio, tipo);
        }

        private decimal GetImporteCuota(int cuota, int primeraCuota0, string importe)
        {
            if (cuota < primeraCuota0)
                return decimal.Parse(importe);
            else
                return 0;
        }

        private string getPeriodoNuevo(string periodoActual)
        {
            int mes = int.Parse(periodoActual.Substring(4, 2));
            int año = int.Parse(periodoActual.Substring(0, 4));
            string periodoNuevo;

            if (mes < 12)
            {
                periodoNuevo = año.ToString() + (mes + 1).ToString("D2");
            }
            else
            {
                periodoNuevo = (año + 1).ToString() + "01";
            }

            return periodoNuevo;
        }
        #endregion

        public segurosNeg(ISegurosServ segurosServ, IConsorciosServ consorciosServ, IExpensasServ expensasServ)
        {
            _segurosServ = segurosServ;
            _consorciosServ = consorciosServ;
            _expensasServ = expensasServ;
        }

        public IEnumerable<SegurosModel> GetSeguros()
        {
            return _segurosServ.GetSeguros();
        }

        public void GuardarSeguro(SegurosModel seguroModelo, List<SeguroDetalleModel> segurosDetalleModelo)
        {
            if (BuscarSeguroActivo(seguroModelo.Consorcio, seguroModelo.Tipo))
            {
                throw new Exception("Existe un Seguro en estado ACTIVO para este Consorcio");
            }

            Consorcios consorcio = _consorciosServ.GetConsorcioById(seguroModelo.Consorcio);
            Seguros seguro = new Seguros();

            seguro.CantCuotas = seguroModelo.CantCuotas;
            seguro.CantCuotasEn0 = seguroModelo.CantCuotas0;
            seguro.Compañia = seguroModelo.Compañia;
            seguro.Poliza = seguroModelo.Poliza;
            seguro.Tipo = seguroModelo.Tipo;
            seguro.FechaInicio = seguroModelo.FechaInicio;
            seguro.FechaFin = seguroModelo.FechaFin;
            seguro.Estado = "ACTIVO";
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

            try
            {
                _segurosServ.GuardarSeguros(seguro);
            }
            catch 
            {
                throw new Exception("No se pudo guardar el nuevo Seguro");
            }
        }

        public void Validar(string compañia, string poliza, string idConsorcios, string cantCuotas, string cuotasDeGracia, string importe)
        {
            string mensaje = string.Empty;

            if (compañia == string.Empty)
            {
                mensaje = "No se ingreso la Compañia";
            }
            else if (poliza == string.Empty)
            {
                mensaje = "No se ingreso la Poliza";
            }
            else if (idConsorcios == "0")
            {
                mensaje = "No se selecciono el Consorcio";
            }
            else if (!cantCuotas.IsNumeric())
            {
                mensaje = "No se ingreso la Cantidad de Cuotas correctamente";
            }
            else if (!cuotasDeGracia.IsNumeric())
            {
                mensaje = "No se ingreso la Cantidad de Cuotas de Gracia correctamente";
            }
            else if (!importe.IsNumeric())
            {
                mensaje = "No se ingreso el Importe correctamente";
            }

            if (mensaje != string.Empty)
            {
                throw new Exception(mensaje);
            }
    }

        public SegurosModel GetSeguroModelo (string compañia, string poliza, string idConsorcios, string cantCuotas, string cuotasDeGracia, string importe, DateTime dteFechaInicio, DateTime dteFechaFin, string tipo)
        {
            try
            {
                Validar(compañia, poliza, idConsorcios, cantCuotas, cuotasDeGracia, importe);

                var seguro = new SegurosModel();

                seguro.Compañia = compañia;
                seguro.Poliza = poliza;
                seguro.Tipo = tipo;
                seguro.FechaInicio = dteFechaInicio;
                seguro.FechaFin = dteFechaFin;
                seguro.Consorcio = idConsorcios;
                seguro.CantCuotas = int.Parse(cantCuotas);
                seguro.CantCuotas0 = int.Parse(cuotasDeGracia);

                return seguro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SeguroDetalleModel> GetSeguroDetalleModelo(string cantCuotas, DateTime dteFechaInicio, string cuotasDeGracia, string importe)
        {
            var detalle = new List<SeguroDetalleModel>();
            int cuotas = int.Parse(cantCuotas);
            var periodo = dteFechaInicio.Year.ToString() + dteFechaInicio.Month.ToString("D2");
            var primeraCuota0 = cuotas - int.Parse(cuotasDeGracia);

            for (int i = 0; i < cuotas; i++)
            {
                detalle.Add(new SeguroDetalleModel()
                {
                    Cuota = i + 1,
                    Periodo = int.Parse(periodo),
                    Importe = GetImporteCuota(i, primeraCuota0, importe)
                });
                periodo = getPeriodoNuevo(periodo);
            }

            return detalle;
        }

        public SeguroActivoModel GetSeguroByConsorcio(int idExpensa, string idConsorcio, string tipo)
        {
            var periodo =_expensasServ.GetDatosExpensa(idExpensa).PeriodoNumerico;
            string tipoSeguro;

            if (tipo == Constantes.SeguroAP)
                tipoSeguro = Constantes.TipoSeguroAP;
            else
                tipoSeguro = Constantes.TipoSeguroIC;

            return _segurosServ.GetSeguroByConsorcio(idConsorcio, periodo, tipoSeguro);
        }
    }
}

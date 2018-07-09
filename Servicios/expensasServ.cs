using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Servicios.Mapper;
using WebSistemmas.Common;
using Servicios;

namespace Servicios
{
    public class expensasServ : IExpensasServ
    {
        private const int GastoTipoFijo = 1;
        private const int GastoTipoEvOrdinario = 2;
        private const int GastoTipoEvExtraordinario = 3;
        readonly ISegurosServ _segurosServ;

        private ExpensasEntities _context; 

        public expensasServ(ExpensasEntities context)
        {
            _context = context;
            _segurosServ = new segurosServ(context);
        }

        public List<Expensas> GetExpensas(string IdConsorcio)
        {
            var expensas = _context.Expensas.Where(x => x.Consorcios.ID == IdConsorcio).OrderByDescending(x => x.PeriodoNumerico).ToList();

            return expensas;
        }

        public ExpensaModel GetDatosExpensa(decimal IdExpensa)
        {
            var expensa = from E in _context.Expensas
                              join C in _context.Consorcios
                              on E.Consorcios.ID equals C.ID 
                              where E.ID == IdExpensa 
                              select new ExpensaModel { ConsorcioId = C.ID, PeriodoNumerico = E.PeriodoNumerico.Value };

            return expensa.FirstOrDefault();
        }

        public List<UnidadesFuncionales> GetUnidadesFuncionales (string ConsorcioId)
        {
            var uf = from U in _context.UnidadesFuncionales
                     join C in _context.Consorcios
                     on U.Consorcios.ID equals C.ID
                     where C.ID == ConsorcioId
                     select U;

            return uf.ToList();
        }

        public Expensas GetUltimaExpensa(string IdConsorcio)
        {
            var expensas = _context.Expensas.Where(x => x.Consorcios.ID == IdConsorcio).OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault();

            return expensas;
        }

        public decimal AgregarExpensa(string IdConsorcio)
        {
            var expensas = _context.Expensas.Where(x => x.Consorcios.ID == IdConsorcio);

            if (expensas.Count() == 0)
            {
                Expensas expensa = new Expensas();
                Consorcios consorcio = _context.Consorcios.Where(x => x.ID == IdConsorcio).FirstOrDefault();

                expensa.Consorcios = consorcio;
                expensa.PeriodoNumerico = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                expensa.Periodo = GetDescripcionPeriodo(expensa.PeriodoNumerico.Value);
                expensa.Estado = "En Proceso";

                AddNewExpensaDetalle(expensa);
                _context.AddToExpensas(expensa);
                _context.SaveChanges();

                return _context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID;

            }
            else if (expensas.OrderByDescending(x => x.ID).FirstOrDefault().Estado == "Finalizado")
            {
                Expensas expensa = new Expensas();
                List<GastosFijos> expensaDetalle;
                Consorcios consorcio = _context.Consorcios.Where(x => x.ID == IdConsorcio).FirstOrDefault();

                expensa.Consorcios = consorcio;
                expensa.PeriodoNumerico = GetNuevoPeriodo(IdConsorcio);
                expensa.Periodo = GetDescripcionPeriodo(expensa.PeriodoNumerico.Value);
                expensa.Estado = "En Proceso";

                expensaDetalle = GetUltimoDetallePorTipo(GastoTipoFijo);
                foreach (var item in expensaDetalle)
                {
                    if (item.Detalle.Contains(Constantes.SeguroAP))
                    {
                        var seguroModel = _segurosServ.GetSeguroByConsorcio(IdConsorcio, expensa.PeriodoNumerico.Value, "AP");
                        if (seguroModel != null)
                        {
                            var detalle = seguroModel != null ? "SEGURO AP CUOTA " + seguroModel.Cuota + " DE " + seguroModel.CantCuota : string.Empty;
                            var importe = seguroModel != null ? seguroModel.Importe : 0;
                            expensa.GastosFijos.Add(new GastosFijos { Detalle = detalle, Importe = importe, TipoGasto_ID = item.TipoGasto_ID, Sumar = item.Sumar });
                        }
                    }
                    else if (item.Detalle.Contains(Constantes.SeguroIC))
                    {
                        var seguroModel = _segurosServ.GetSeguroByConsorcio(IdConsorcio, expensa.PeriodoNumerico.Value, "IC");
                        if (seguroModel != null)
                        {
                            var detalle = seguroModel != null ? "SEGURO IC CUOTA " + seguroModel.Cuota + " DE " + seguroModel.CantCuota : string.Empty;
                            var importe = seguroModel != null ? seguroModel.Importe : 0;
                            expensa.GastosFijos.Add(new GastosFijos { Detalle = detalle, Importe = importe, TipoGasto_ID = item.TipoGasto_ID, Sumar = item.Sumar });
                        }
                    }
                    else
                    {
                        if (item.Detalle != Constantes.FondoPrevisionOrdinario)
                        {
                            expensa.GastosFijos.Add(new GastosFijos { Detalle = item.Detalle, Importe = item.Importe, TipoGasto_ID = item.TipoGasto_ID, Sumar = item.Sumar });
                        }
                    }
                    expensa.Total_Gastos = +item.Importe;
                }

                AddNewExpensaDetalle(expensa);
                _context.AddToExpensas(expensa);
                _context.SaveChanges();

                return _context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID;
            }
            else
            {
                 return 0;
            }
        }

        public bool DeleteExpensa (int idExpensa)
        {
            try
            {
                var detalle = _context.GastosFijos.Where(x => x.Expensas.ID == idExpensa).ToList();

                if (detalle != null)
                {
                    foreach (var item in detalle)
                    {
                        _context.DeleteObject(item);
                    }                    
                }

                var extraordinarios = _context.GastosEvExt.Where(x => x.Expensas.ID == idExpensa).ToList();

                if (extraordinarios != null)
                {
                    foreach (var item in extraordinarios)
                    {
                        _context.DeleteObject(item);
                    }                    
                }

                var expensa = _context.Expensas.Where(x => x.ID == idExpensa).FirstOrDefault();

                _context.DeleteObject(expensa);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private List<GastosFijos> GetUltimoDetalle()
        {
            decimal idExpensa = Convert.ToDecimal(_context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().ID.ToString());

            return _context.GastosFijos.Where(x => x.Expensas.ID == idExpensa).ToList();
        }

        private List<GastosFijos> GetUltimoDetallePorTipo(int tipoGasto)
        {
            return GetUltimoDetalle().Where(x => x.TipoGasto_ID == tipoGasto).ToList();
        }

        private string GetDescripcionPeriodo(int Periodo)
        {
            switch (Periodo.ToString().Substring(4, 2))
            {
                case "01":
                    return "ENE " + Periodo.ToString().Substring(0, 4);

                case "02":
                    return "FEB " + Periodo.ToString().Substring(0, 4);

                case "03":
                    return "MAR " + Periodo.ToString().Substring(0, 4);

                case "04":
                    return "ABR " + Periodo.ToString().Substring(0, 4);

                case "05":
                    return "MAY " + Periodo.ToString().Substring(0, 4);

                case "06":
                    return "JUN " + Periodo.ToString().Substring(0, 4);

                case "07":
                    return "JUL " + Periodo.ToString().Substring(0, 4);

                case "08":
                    return "AGO " + Periodo.ToString().Substring(0, 4);

                case "09":
                    return "SEP " + Periodo.ToString().Substring(0, 4);

                case "10":
                    return "OCT " + Periodo.ToString().Substring(0, 4);

                case "11":
                    return "NOV " + Periodo.ToString().Substring(0, 4);

                case "12":
                    return "DIC " + Periodo.ToString().Substring(0, 4);

                default:
                    return "";

            }
        }

        private int GetNuevoPeriodo(string IdConsorcio)
        {
            string periodoActual = _context.Expensas.OrderByDescending(x => x.PeriodoNumerico).Where(x => x.Consorcios.ID == IdConsorcio).FirstOrDefault().PeriodoNumerico.Value.ToString();

            if (periodoActual.Substring(4, 2) == "12")
                return (Convert.ToInt32(periodoActual.Substring(0, 4)) + 1) * 100 + 1;
            else
                return Convert.ToInt32(periodoActual) + 1;
        }

        public void AgregarExpensaDetalle(int IdExpensa, string Detalle, decimal Importe, int TipoGasto, decimal IdGasto)
        {
            GastosFijos detalle = new GastosFijos();

            detalle.Expensas = _context.Expensas.FirstOrDefault(x => x.ID == IdExpensa);
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.TipoGasto_ID = TipoGasto;
            detalle.Sumar = true;
            detalle.Gastos_ID = IdGasto;

            _context.AddToGastosFijos(detalle);
            _context.SaveChanges();
        }

        public void AgregarGastoEvOrdinario(int IdExpensa, string Detalle, decimal Importe, decimal ImporteCompra, decimal ProveedorId, decimal CtaCteId)
        {
            GastosEvOrd detalle = new GastosEvOrd();

            detalle.Expensas = _context.Expensas.FirstOrDefault(x => x.ID == IdExpensa);
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.ImporteCompra = ImporteCompra;
            detalle.Proveedores_ID = ProveedorId;
            detalle.ProveedoresCtaCte_ID = CtaCteId;

            _context.AddToGastosEvOrd(detalle);
            _context.SaveChanges();
        }

        public void ModificarGastoEvOrdinario(int IdGasto, string Detalle, decimal Importe, decimal ImporteCompra)
        {
            var gasto = _context.GastosEvOrd.Where(x => x.ID == IdGasto).FirstOrDefault();

            gasto.Detalle = Detalle;
            gasto.Importe = Importe;
            gasto.ImporteCompra = ImporteCompra;

            _context.SaveChanges();
        }

        public void ActualizarTotalGastosEvOrdinarios(decimal idExpensa)
        {
            //obtener la suma de los gastos ev. ordinarios
            var gastosEvOrdinarios = _context.GastosEvOrd.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe).GetValueOrDefault();

            //actualizar la tabla GastosDetalles
            var expensaDetalle = _context.GastosFijos.Where(x => x.Detalle == Constantes.TotalGastoEvOrdinarios && x.Expensas.ID == idExpensa).FirstOrDefault();
            expensaDetalle.Importe = gastosEvOrdinarios;

            _context.SaveChanges();
        }

        public void ActualizarTotalGastosEvExtraordinarios(decimal idExpensa)
        {
            //obtener la suma de los gastos ev. ordinarios
            var gastosEvExtraordinarios = _context.GastosEvExt.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe).GetValueOrDefault();

            //actualizar la tabla GastosDetalles
            var expensaDetalle = _context.GastosFijos.Where(x => x.Detalle == Constantes.TotalGastoEvExtraordinarios && x.Expensas.ID == idExpensa).FirstOrDefault();
            expensaDetalle.Importe = gastosEvExtraordinarios;

            _context.SaveChanges();
        }

        public void ModificarExpensaDetalle(int IdExpensaDetalle, string Detalle, decimal Importe)
        {
            GastosFijos detalle = _context.GastosFijos.Where(x => x.ID == IdExpensaDetalle).FirstOrDefault();

            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            _context.SaveChanges();
        }

        public decimal AgregarGastoExtraordinario(int IdExpensa, string Detalle, decimal Importe, decimal ImporteCompra, decimal ProveedorId, decimal CtaCteId)
        {
            GastosEvExt detalle = new GastosEvExt();

            detalle.Expensas = _context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.ImporteCompra = ImporteCompra;
            detalle.Proveedores_ID = ProveedorId;
            detalle.ProveedoresCtaCte_ID = CtaCteId;

            _context.AddToGastosEvExt(detalle);
            _context.SaveChanges();

            return detalle.ID;
        }

        public void ModificarGastoExtraordinario(int IdExpensaDetalle, string Detalle, decimal Importe, decimal ImporteCompra)
        {
            var detalle = _context.GastosEvExt.Where(x => x.ID == IdExpensaDetalle).FirstOrDefault();
            detalle.Detalle = Detalle;
            detalle.Importe = Importe;
            detalle.ImporteCompra = ImporteCompra;

            _context.SaveChanges();
        }

        public List<GastosFijos> GetGastosByTipo(int IdExpensa, int TipoGasto, bool SoloSumarChequeada = false)
        {
            List<GastosFijos> expensasDetalle;

            if (SoloSumarChequeada)
                expensasDetalle = _context.GastosFijos.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID.Value == TipoGasto && x.Sumar == true).ToList();
            else
                expensasDetalle = _context.GastosFijos.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID.Value == TipoGasto).ToList();

            return expensasDetalle;
        }


        public IEnumerable<GastosOrdinariosModel> GetGastosOrdinarios(int ExpensaID, bool SoloSumarChequeada = false)
        {
            var expensasDetalles = GetGastosByTipo(ExpensaID, GastoTipoFijo, SoloSumarChequeada);
            expensasDetalles.AddRange(GetGastosByTipo(ExpensaID, GastoTipoEvOrdinario, SoloSumarChequeada));
            expensasDetalles.AddRange(GetGastosByTipo(ExpensaID, GastoTipoEvExtraordinario, SoloSumarChequeada));

            var gastosOrdinarios = AutoMapper.MapToGastosOrdinarios(expensasDetalles);

            return gastosOrdinarios;
        }

        public GastosFijos GetExpensaDetalle(int ExpensaID, int GastoID)
        {
            var expensaDetalle = _context.GastosFijos.Where(x => x.Expensas.ID == ExpensaID && x.TipoGasto_ID.Value == GastoTipoFijo && x.Gastos_ID == GastoID).FirstOrDefault();

            return expensaDetalle;
        }

        public List<GastosFijos> GetExpensaDetalle(int ExpensaID)
        {
            var expensaDetalle = _context.GastosFijos.Where(x => x.Expensas.ID == ExpensaID && x.Sumar == true).ToList();

            return expensaDetalle;
        }

        public List<GastosEvOrd> GetGastosEvOrdinarios(int IdExpensa)
        {
            return _context.GastosEvOrd.Where(x => x.Expensas.ID == IdExpensa).ToList();
        }

        public List<GastosEvExt> GetGastosEvExtraordinarios(int IdExpensa)
        {
            return _context.GastosEvExt.Where(x => x.Expensas.ID == IdExpensa).ToList();
        }

        public decimal GetTotalGastosExtraordinarios(int IdExpensa)
        {
            return _context.GastosFijos.Where(x => x.Expensas.ID == IdExpensa && x.Sumar == true && x.TipoGasto_ID == GastoTipoEvExtraordinario).Sum(x => x.Importe).GetValueOrDefault();
        }

        public decimal GetUltimoTotal()
        {
            return _context.Expensas.OrderByDescending(x => x.PeriodoNumerico).FirstOrDefault().Total_Gastos.Value;
        }

        public decimal GetTotalGastosOrdinarios(int idExpensa)
        {
            var detalle = _context.GastosFijos.Where(x => x.Expensas.ID == idExpensa && x.Sumar == true && x.TipoGasto_ID != GastoTipoEvExtraordinario).Sum(x => x.Importe);

            return detalle ?? 0;
        }

        public decimal GetTotalGastosEvOrdinarios(int idExpensa)
        {
            var detalle = _context.GastosEvOrd.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public decimal GetTotalGastosEvExtraordinarios(int idExpensa)
        {
            var detalle = _context.GastosEvExt.Where(x => x.Expensas.ID == idExpensa).Sum(x => x.Importe);

            return (detalle == null) ? 0 : detalle.Value;
        }

        public void GuardarUltimoTotal(int idExpensa, decimal total)
        {
            var expensa = _context.Expensas.Where(x => x.ID == idExpensa).FirstOrDefault();

            expensa.Total_Gastos = total;
            _context.SaveChanges();
        }

        public void ActualizarGastosExtraordinario(int IdExpensa, decimal Importe)
        {
            var detalle = _context.GastosFijos.Where(x => x.Expensas.ID == IdExpensa && x.TipoGasto_ID == GastoTipoEvExtraordinario).FirstOrDefault();

            if (detalle == null)
            {
                GastosFijos nuevoDetalle = new GastosFijos();
                nuevoDetalle.Expensas = _context.Expensas.Where(x => x.ID == IdExpensa).FirstOrDefault();
                nuevoDetalle.Importe = Importe;
                nuevoDetalle.TipoGasto_ID = GastoTipoEvExtraordinario;
                nuevoDetalle.Detalle = Constantes.FondoPrevisionExtraordinario;
                _context.AddToGastosFijos(nuevoDetalle);
            }
            else
            {
                detalle.Importe = Importe;
            }

            _context.SaveChanges();
        }

        public bool ActualizarCheckSumar(int idExpensaDetalle, bool sumar)
        {
            var detalle = _context.GastosFijos.FirstOrDefault(x => x.ID == idExpensaDetalle);

            if (detalle != null)
            {
                detalle.Sumar = sumar;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool CambiarEstadoExpensa(decimal expensaID, string estado)
        {            
            var expensa = _context.Expensas.Where(x => x.ID == expensaID).FirstOrDefault();

            if (expensa != null)
            {
                expensa.Estado = estado;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Expensas GetExpensa(decimal expensaID)
        {
            return _context.Expensas.Where(x => x.ID == expensaID).FirstOrDefault();
        }

        public int GetPeriodoNumerico(decimal expensaID)
        {
            var expensa = _context.Expensas.Where(x => x.ID == expensaID).FirstOrDefault();

            if (expensa != null)
                return expensa.PeriodoNumerico.Value;
            else
                throw new Exception("No se encontro la Expensa en la base de datos");
        }

        public decimal GetProveedorCtaCteId(string tipo, decimal GastoId)
        {
            if (tipo == Constantes.GastoEvExt)
            {
                return _context.GastosEvExt.FirstOrDefault(x => x.ID == GastoId).ProveedoresCtaCte_ID.Value;
            }
            else
            {
                return 0;
            }            
        }

        #region Private Methods
        private void AddNewExpensaDetalle(Expensas expensa)
        {
            //Agregar las filas de Totales a ExpensasDetalles
            expensa.GastosFijos.Add(new GastosFijos { Detalle = Constantes.TotalGastoEvOrdinarios, Importe = 0, TipoGasto_ID = GastoTipoEvOrdinario, Sumar = true, Orden = 1 });
            expensa.GastosFijos.Add(new GastosFijos { Detalle = Constantes.TotalGastoEvExtraordinarios, Importe = 0, TipoGasto_ID = GastoTipoEvExtraordinario, Sumar = true, Orden = 1 });
            expensa.GastosFijos.Add(new GastosFijos { Detalle = Constantes.FondoPrevisionOrdinario, Importe = 0, TipoGasto_ID = GastoTipoFijo, Sumar = true, Orden = 2 });
            expensa.GastosFijos.Add(new GastosFijos { Detalle = Constantes.FondoPrevisionExtraordinario, Importe = 0, TipoGasto_ID = GastoTipoEvExtraordinario, Sumar = true, Orden = 2 });
        }
        #endregion
    }
}

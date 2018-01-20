﻿using DAO;
using Servicios.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class gastosServ : IGastosServ
    {
        private ExpensasEntities _context;

        public gastosServ(ExpensasEntities context)
        {
            _context = context;
        }

        public List<TipoGastos> GetTipoGastos()
        {
            List<TipoGastos> tipoGastos = new List<TipoGastos>();

            foreach (var item in _context.TipoGastos.ToList())
            {
                tipoGastos.Add(new TipoGastos {ID = item.ID, Detalle = item.Detalle });
            }

            return tipoGastos;
        }

        public List<Gastos> GetDetalleGastos(int tipoGasto)
        {
            return _context.Gastos.Where(x => x.TipoGastos.ID == tipoGasto).OrderBy(x => x.Detalle).ToList();
        }

        public List<GastosOrdinariosModel> GetDetalleGastosCombo (int tipoGasto)
        {
            var gastos = _context.Gastos.Where(x => x.TipoGastos.ID == tipoGasto).OrderBy(x => x.Detalle).ToList();
            var gastosModel = new List<GastosOrdinariosModel>();

            gastosModel.Add(new GastosOrdinariosModel() { Detalle = "Seleccione un Gasto", ID = 0 });

            foreach (var item in gastos)
            {
                gastosModel.Add(new GastosOrdinariosModel() { Detalle = item.Detalle, ID = item.ID });
            }

            return gastosModel;
        }

        public void DeleteDetalle(decimal idExpensaDetalle)
        {
            ExpensasDetalle expensaDetalle = _context.ExpensasDetalle.Where(x => x.ID == idExpensaDetalle).FirstOrDefault();
            _context.DeleteObject(expensaDetalle);
            _context.SaveChanges();            
        }

        public void DeleteGastoEvOrdinario(decimal idGasto)
        {
            GastosEvOrdinariosDetalle gastosEvOrdinariosDetalle = _context.GastosEvOrdinariosDetalle.Where(x => x.ID == idGasto).FirstOrDefault();
            _context.DeleteObject(gastosEvOrdinariosDetalle);
            _context.SaveChanges();
        }

        public void DeleteGastoEvExtraordinario(decimal idGasto)
        {
            GastosExtDetalle gastoExtDetalle = _context.GastosExtDetalle.Where(x => x.ID == idGasto).FirstOrDefault();
            _context.DeleteObject(gastoExtDetalle);
            _context.SaveChanges();
        }

        public List<Gastos> AddGasto(int idTipoGasto, string detalleGasto)
        {
            TipoGastos tipoGasto = _context.TipoGastos.Where(x => x.ID == idTipoGasto).FirstOrDefault();
            Gastos gasto = new Gastos();

            gasto.Detalle = detalleGasto;
            gasto.TipoGastos = tipoGasto;

            _context.AddToGastos(gasto);
            _context.SaveChanges();

            return GetDetalleGastos(idTipoGasto);
        }

        public List<Gastos> DeleteGasto(int idGasto, int tipoGasto)
        {
            var gasto = _context.Gastos.Where(x => x.ID == idGasto).FirstOrDefault();
            _context.DeleteObject(gasto);
            _context.SaveChanges();

            return GetDetalleGastos(tipoGasto);
        }        
    }
}

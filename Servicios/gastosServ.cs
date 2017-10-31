﻿using DAO;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class gastosServ : IGastosServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public List<TipoGastos> GetTipoGastos()
        {
            List<TipoGastos> tipoGastos = new List<TipoGastos>();

            foreach (var item in context.TipoGastos.ToList())
            {
                tipoGastos.Add(new TipoGastos {ID = item.ID, Detalle = item.Detalle });
            }

            return tipoGastos;
        }

        public List<Gastos> GetGastos(int tipoGasto)
        {
            return context.Gastos.Where(x => x.TipoGastos.ID == tipoGasto).OrderBy(x => x.Detalle).ToList();
        }

        public void DeleteDetalle(decimal idExpensaDetalle)
        {
            ExpensasDetalle expensaDetalle = context.ExpensasDetalle.Where(x => x.ID == idExpensaDetalle).FirstOrDefault();
            context.DeleteObject(expensaDetalle);
            context.SaveChanges();            
        }

        public void DeleteGastoEvOrdinario(decimal idGasto)
        {
            GastosEvOrdinariosDetalle gastosEvOrdinariosDetalle = context.GastosEvOrdinariosDetalle.Where(x => x.ID == idGasto).FirstOrDefault();
            context.DeleteObject(gastosEvOrdinariosDetalle);
            context.SaveChanges();
        }

        public void DeleteGastoEvExtraordinario(decimal idGasto)
        {
            GastosExtDetalle gastoExtDetalle = context.GastosExtDetalle.Where(x => x.ID == idGasto).FirstOrDefault();
            context.DeleteObject(gastoExtDetalle);
            context.SaveChanges();
        }

        public List<Gastos> AddGasto(int idTipoGasto, string detalleGasto)
        {
            TipoGastos tipoGasto = context.TipoGastos.Where(x => x.ID == idTipoGasto).FirstOrDefault();
            Gastos gasto = new Gastos();

            gasto.Detalle = detalleGasto;
            gasto.TipoGastos = tipoGasto;

            context.AddToGastos(gasto);
            context.SaveChanges();

            return GetGastos(idTipoGasto);
        }

        public List<Gastos> DeleteGasto(int idGasto, int tipoGasto)
        {
            var gasto = context.Gastos.Where(x => x.ID == idGasto).FirstOrDefault();
            context.DeleteObject(gasto);
            context.SaveChanges();

            return GetGastos(tipoGasto);
        }
    }
}

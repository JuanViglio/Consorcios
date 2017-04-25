﻿using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servicios
{
    public class gastosServ
    {
        private ExpensasEntities context = new ExpensasEntities();

        public List<TipoGastos> GetTipoGastos()
        {
            List<TipoGastos> tipoGastos = new List<TipoGastos>();

            tipoGastos.Add(new TipoGastos { ID = 0, Detalle = "Seleccione un Tipo de Gasto" });
            TipoGastos tipo = new TipoGastos();

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
    }
}
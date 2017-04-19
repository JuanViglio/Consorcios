using DAO;
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
            var tipoGastos = context.TipoGastos.ToList();

            return tipoGastos;
        }

        public List<Gastos> GetGastos(int tipoGasto)
        {
            return context.Gastos.Where(x => x.TipoGastos.ID == tipoGasto).OrderBy(x => x.Detalle).ToList();
        }
    }
}

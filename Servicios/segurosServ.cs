using DAO;
using DAO.Models;
using Servicios.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class segurosServ : ISegurosServ
    {
        private ExpensasEntities _context;

        public segurosServ(ExpensasEntities context)
        {
            _context = context;
        }

        public IEnumerable<SegurosModel> GetSeguros()
        {
            var seguros = from s in _context.Seguros
                              join c in _context.Consorcios 
                              on s.Consorcios.ID equals c.ID 
                              select new SegurosModel {
                                  ID = s.ID,
                                  Compañia = s.Compañia,
                                  Tipo = s.Tipo,
                                  Poliza = s.Poliza, 
                                  FechaInicio = s.FechaInicio,
                                  FechaFin = s.FechaFin,
                                  CantCuotas = s.CantCuotas,
                                  CantCuotas0 = s.CantCuotasEn0,
                                  Estado = s.Estado,
                                  Consorcio = c.Direccion
                              };

            return seguros;
        }

        public void GuardarSeguros(Seguros seguro)
        {
            _context.AddToSeguros(seguro);
            _context.SaveChanges();
        }
    }
}

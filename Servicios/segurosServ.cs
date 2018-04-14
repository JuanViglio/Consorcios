using DAO;
using DAO.Models;
using Servicios.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class segurosServ
    {
        private ExpensasEntities _context;

        public segurosServ(ExpensasEntities context)
        {
            _context = context;
        }

        public IEnumerable<SegurosModel> GetSeguros()
        {
            var proveedores = _context.Seguros.ToList();

            return AutoMapper.MapToSegurosModel(proveedores);
        }
    }
}

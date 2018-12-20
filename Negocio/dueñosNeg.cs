using DAO;
using DAO.Models;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class dueñosNeg
    {
        readonly dueñosServ _dueñosServ;

        public dueñosNeg()
        {
            _dueñosServ = new dueñosServ();
        }

        public List<DueñosModel> GetDueños()
        {
            return _dueñosServ.GetDueños();
        }

        public void EliminarDueño(int idDueño)
        {
            _dueñosServ.EliminarDueño(idDueño);
        }
    }
}

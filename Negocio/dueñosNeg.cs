﻿using DAO;
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

        public List<Dueños> GetDueños()
        {
            return _dueñosServ.GetDueños();
        }
    }
}

﻿using DAO;
using System.Collections.Generic;

namespace Servicios.Interfaces
{
    public interface IConsorciosServ
    {
        List<Consorcios> GetConsorcios();

        Consorcios GetConsorcioById(string id);

        List<ConsorciosModel> GetConsorciosCombo();

        List<Consorcios> UpdateConsorcios(string id, string direccion, string vencimiento1, string vencimiento2, string interes);

        List<Consorcios> AddConsorcio(string id, string direccion, string vencimiento1, string vencimiento2, string interes);
    }
}

using DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using WebSistemmas.Common;

namespace Servicios.Tests
{
    [TestClass()]
    public class expensasServTest
    {
        ExpensasDbMock _context = new ExpensasDbMock();

        [TestMethod()]
        public void GetPeriodoNumerico_OK()
        {           
            expensasMockServ serv = new expensasMockServ(_context);
            var resultado = serv.GetPeriodoNumerico(_context.Expensas[0].ID);
            Assert.AreEqual(resultado, _context.Expensas[0].PeriodoNumerico);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetPeriodoNumerico_Error()
        {
            expensasMockServ serv = new expensasMockServ(_context);
            var resultado = serv.GetPeriodoNumerico(0);
            Assert.AreEqual(resultado, _context.Expensas[0].PeriodoNumerico);
        }

        [TestMethod()]
        public void CambiarEstadoExpensa_OK()
        {
            expensasMockServ serv = new expensasMockServ(_context);
            serv.CambiarEstadoExpensa(_context.Expensas[0].ID, Constantes.EstadoAceptado);
        }
    }
}
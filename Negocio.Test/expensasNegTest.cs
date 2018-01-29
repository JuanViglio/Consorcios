using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Servicios.Interfaces;
using DAO;
using System.Collections.Generic;
using WebSistemmas.Common;

namespace Negocio.Test
{
    [TestClass]
    public class expensasNegTest
    {
        private Mock<IExpensasServ> _mockExpensasServ;
        private Mock<IPagosServ> _mockPagosServ;
        private expensasNeg _expensasNeg;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockExpensasServ = new Mock<IExpensasServ>();
            _mockPagosServ = new Mock<IPagosServ>();
            _expensasNeg = new expensasNeg(_mockExpensasServ.Object, _mockPagosServ.Object);
        }

        [TestMethod]
        public void AceptarExpensa_Creando_Nuevo_Pago_OK()
        {
            int expensaId = 1000;
            ExpensaModel expensa = new ExpensaModel() { ConsorcioId = "1", PeriodoNumerico = 201801 };
            List<decimal> pagos = new List<decimal>();
            string gastosExtraordinarios = "100";
            string totalGastosOrdinarios = "600";
            UnidadesFuncionales uf = new UnidadesFuncionales()
            {
                ID = 1,
                Apellido = "Apellido Test 1",
                Nombre = "Nombre Test 1",
                UF = "1",
                Coeficiente = 5
            };
            List<UnidadesFuncionales> unidadesFuncionales = new List<UnidadesFuncionales>();
            unidadesFuncionales.Add(uf);

            _mockExpensasServ.Setup(x => x.GetDatosExpensa(expensaId)).Returns(expensa);
            _mockExpensasServ.Setup(x => x.GetUnidadesFuncionales(expensa.ConsorcioId)).Returns(unidadesFuncionales);
            _mockPagosServ.Setup(x => x.GetPagos(expensa.PeriodoNumerico, expensa.ConsorcioId)).Returns(pagos);
            _mockPagosServ.Setup(x => x.AddPagos(expensa.ConsorcioId, uf, gastosExtraordinarios, totalGastosOrdinarios, expensa.PeriodoNumerico));
            _mockExpensasServ.Setup(x => x.CambiarEstadoExpensa(expensaId, Constantes.EstadoAceptado)).Returns(true);

            var respuesta = _expensasNeg.AceptarExpensa(expensaId,"400", "50");

            Assert.AreEqual(respuesta, unidadesFuncionales.Count, "La cantidad de pagos no es correcta");
        }

        [TestMethod]
        public void AceptarExpensa_Modificando_Pagos_OK()
        {
            int expensaId = 1000;
            ExpensaModel expensa = new ExpensaModel() { ConsorcioId = "1", PeriodoNumerico = 201801 };
            List<decimal> pagos = new List<decimal>() { 100 };
            string gastosExtraordinarios = "100";
            string totalGastosOrdinarios = "600";
            UnidadesFuncionales uf = new UnidadesFuncionales()
            {
                ID = 1,
                Apellido = "Apellido Test 1",
                Nombre = "Nombre Test 1",
                UF = "1",
                Coeficiente = 5
            };
            List<UnidadesFuncionales> unidadesFuncionales = new List<UnidadesFuncionales>();
            unidadesFuncionales.Add(uf);

            _mockExpensasServ.Setup(x => x.GetDatosExpensa(expensaId)).Returns(expensa);
            _mockExpensasServ.Setup(x => x.GetUnidadesFuncionales(expensa.ConsorcioId)).Returns(unidadesFuncionales);
            _mockPagosServ.Setup(x => x.GetPagos(expensa.PeriodoNumerico, expensa.ConsorcioId)).Returns(pagos);
            _mockPagosServ.Setup(x => x.UpdatePagos(expensa.ConsorcioId, uf, gastosExtraordinarios, totalGastosOrdinarios, expensa.PeriodoNumerico));
            _mockExpensasServ.Setup(x => x.CambiarEstadoExpensa(expensaId, Constantes.EstadoAceptado)).Returns(true);

            var respuesta = _expensasNeg.AceptarExpensa(expensaId, "400", "50");

            Assert.AreEqual(respuesta, unidadesFuncionales.Count, "La cantidad de pagos no es correcta");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AceptarExpensa_Expensa_Null()
        {
            int expensaId = 1000;
            ExpensaModel expensa = null;

            _mockExpensasServ.Setup(x => x.GetDatosExpensa(expensaId)).Returns(expensa);

            var respuesta = _expensasNeg.AceptarExpensa(expensaId, "400", "50");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AceptarExpensa_UF_Null()
        {
            int expensaId = 1000;
            ExpensaModel expensa = new ExpensaModel() { ConsorcioId = "1", PeriodoNumerico = 201801 };
            List<UnidadesFuncionales> unidadesFuncionales = null;

            _mockExpensasServ.Setup(x => x.GetDatosExpensa(expensaId)).Returns(expensa);
            _mockExpensasServ.Setup(x => x.GetUnidadesFuncionales(expensa.ConsorcioId)).Returns(unidadesFuncionales);

            var respuesta = _expensasNeg.AceptarExpensa(expensaId, "400", "50");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AceptarExpensa_Cambiar_Estado_False()
        {
            int expensaId = 1000;
            ExpensaModel expensa = new ExpensaModel() { ConsorcioId = "1", PeriodoNumerico = 201801 };
            List<decimal> pagos = new List<decimal>() { 100 };
            string gastosExtraordinarios = "100";
            string totalGastosOrdinarios = "600";
            UnidadesFuncionales uf = new UnidadesFuncionales()
            {
                ID = 1,
                Apellido = "Apellido Test 1",
                Nombre = "Nombre Test 1",
                UF = "1",
                Coeficiente = 5
            };
            List<UnidadesFuncionales> unidadesFuncionales = new List<UnidadesFuncionales>();
            unidadesFuncionales.Add(uf);

            _mockExpensasServ.Setup(x => x.GetDatosExpensa(expensaId)).Returns(expensa);
            _mockExpensasServ.Setup(x => x.GetUnidadesFuncionales(expensa.ConsorcioId)).Returns(unidadesFuncionales);
            _mockPagosServ.Setup(x => x.GetPagos(expensa.PeriodoNumerico, expensa.ConsorcioId)).Returns(pagos);
            _mockPagosServ.Setup(x => x.UpdatePagos(expensa.ConsorcioId, uf, gastosExtraordinarios, totalGastosOrdinarios, expensa.PeriodoNumerico));
            _mockExpensasServ.Setup(x => x.CambiarEstadoExpensa(expensaId, Constantes.EstadoAceptado)).Returns(false);

            var respuesta = _expensasNeg.AceptarExpensa(expensaId, "400", "50");
        }
    }
}

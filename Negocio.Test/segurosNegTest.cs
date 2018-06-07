using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Servicios;
using Servicios.Interfaces;
using Moq;

namespace Negocio.Test
{
    [TestClass]
    public class segurosNegTest
    {
        private segurosNeg _segurosNeg;
        private Mock<ISegurosServ> _mockSegurosServ;
        private Mock<IConsorciosServ> mockConsorciosServ;
        private Mock<IExpensasServ> _mockExpensasServ;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockSegurosServ = new Mock<ISegurosServ>();
            mockConsorciosServ = new Mock<IConsorciosServ>();
            _segurosNeg = new segurosNeg(_mockSegurosServ.Object, mockConsorciosServ.Object, _mockExpensasServ.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validar_Compañia_Empty()
        {
            _segurosNeg.Validar(string.Empty, "poliza", "1", "12", "2", "400");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validar_Poliza_Empty()
        {
            _segurosNeg.Validar("Compañia", string.Empty, "1", "12", "2", "400");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validar_Consorcio_0_Empty()
        {
            _segurosNeg.Validar("Compañia", "poliza", "0", "12", "2", "400");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validar_CantCuotas_Empty()
        {
            _segurosNeg.Validar("Compañia", "poliza", "1", string.Empty, "2", "400");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validar_CuotasDeGracia_Empty()
        {
            _segurosNeg.Validar("Compañia", "poliza", "1", "12", string.Empty, "400");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Validar_Importe_Empty()
        {
            _segurosNeg.Validar("Compañia", "poliza", "1", "12", "2", string.Empty);
        }

        [TestMethod]
        public void GetSeguroDetalleModelo_OK()
        {
            var seguroDetalle = _segurosNeg.GetSeguroDetalleModelo("6", Convert.ToDateTime("1/1/18") , "2", "500");

            Assert.AreEqual(seguroDetalle.Count, 6);
            Assert.AreEqual(seguroDetalle[4].Importe, 0);
            Assert.AreEqual(seguroDetalle[5].Importe, 0);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Servicios.Interfaces;
using Servicios;
using DAO;
using System.Collections.Generic;

namespace Negocio.Test
{
    [TestClass]
    public class expensasNegTest
    {
        private Mock<IUnidadesServ> _mockUnidadesServ;
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
        public void Aceptar_Expensa_Creando_Nuevo_Pago()
        {
            int expensaId = 1000;
            string consorcioId = "1";
            int periodoNumerico = 201801;
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

            _mockExpensasServ.Setup(x => x.GetConsorcioId(expensaId)).Returns(consorcioId);
            _mockExpensasServ.Setup(x => x.GetPeriodoNumerico(expensaId)).Returns(periodoNumerico);
            _mockExpensasServ.Setup(x => x.GetUnidadesFuncionales(consorcioId)).Returns(unidadesFuncionales);
            _mockPagosServ.Setup(x => x.GetPagos(periodoNumerico, consorcioId)).Returns(pagos);
            _mockPagosServ.Setup(x => x.AddPagos(consorcioId, uf, gastosExtraordinarios, totalGastosOrdinarios, periodoNumerico));
            _mockExpensasServ.Setup(x => x.AceptarExpensa(expensaId));

            var respuesta = _expensasNeg.AceptarExpensa(expensaId,"400", "50");
        }
    }
}

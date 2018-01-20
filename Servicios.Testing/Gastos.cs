using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using System.Collections.Generic;
using System.Linq;
using WebSistemmas.Common;
using System.Data.Objects;
using System.Data.Entity;

namespace Servicios.Testing
{
    [TestClass]
    public class GastosTest
    {
        [TestMethod]
        public void AddGastos_Return_Gasto()
        {
            var data = new List<Gastos>()
            {
                new Gastos() {ID = 1, Detalle = "Gasto 1" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Gastos>>();  //DbSet
            
            //mockSet.As<IQueryable<Gastos>>().Setup(m => m.Provider).Returns(data.Provider);
            //mockSet.As<IQueryable<Gastos>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSet.As<IQueryable<Gastos>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSet.As<IQueryable<Gastos>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExpensasEntities>();

            var userContextMock = new Mock<ExpensasEntities>();
           
            //userContextMock.Setup(x => x.Gastos).Returns(mockSet);

            var service = new gastosServ(mockContext.Object);
            var gastos = service.GetDetalleGastos(Constantes.GastoTipoOrdinario);

            Assert.AreEqual(1, gastos.Count);
            Assert.AreEqual("Gasto 1", gastos[0].Detalle);

        }
    }
}

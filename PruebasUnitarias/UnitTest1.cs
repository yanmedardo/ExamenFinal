using FinalCalidad.Controllers;
using FinalCalidad.Models;
using FinalCalidad.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace PruebasUnitarias
{
    public class Tests
    {
        [Test]
        // TEST PARA LISTAR CUENTAS
        public void Test1()
        {
            
            List<Cuentas> cuentas = new List<Cuentas>();
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuentas()).Returns(cuentas);

            var controller = new CuentaController(cuentaMock.Object);
            var index = controller.Index();
            Assert.IsInstanceOf<ViewResult>(index);
        }

        [Test]
        // TEST PARA LISTAR CUENTAS, MODEL
        public void Test2()
        {
            List<Cuentas> cuentas = new List<Cuentas>();
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuentas()).Returns(cuentas);

            var controller = new CuentaController(cuentaMock.Object);
            var index = controller.Index() as ViewResult;
            Assert.AreEqual(index.Model, cuentas);
        }

        [Test]
        // TEST PARA REGISTRAR UNA CUENTA
        public void Test3()
        {
            var cuentaMock = new Mock<ICuentaService>();
            var controller = new CuentaController(cuentaMock.Object);
            var crear = controller.Crear() as ViewResult;
            Assert.IsInstanceOf<ViewResult>(crear);
        }

        [Test]
        // TEST PARA REGISTRAR UNA CUENTA Y REDIRECCIONAR
        public void Test4()
        {
            var cuenta = new Cuentas { Nombre = "BCP" };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.Registrar(cuenta));
            var controller = new CuentaController(cuentaMock.Object);
            var crear = controller.Crear(cuenta);
            Assert.IsInstanceOf<RedirectToActionResult>(crear);
        }

        [Test]
        // TEST PARA LISTAR LOS INGRESOS DE UNA CUENTA
        public void Test5()
        {
            IQueryable<Ingresos> ingresos = new List<Ingresos>() as IQueryable<Ingresos>;
            var cuenta = new Cuentas { Id = 10 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.ListarIngresos(cuenta)).Returns(ingresos);

            var controller = new CuentaController(cuentaMock.Object);
            var listarIngresos = controller.ListarIngresos(1);
            Assert.IsInstanceOf<ViewResult>(listarIngresos);
        }


        [Test]
        // TEST PARA LISTAR LOS INGRESOS DE UNA CUENTA, MODEL
        public void Test6()
        {
            IQueryable<Ingresos> ingresos = new List<Ingresos>() as IQueryable<Ingresos>;
            var cuenta = new Cuentas { Id = 10 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.ListarIngresos(cuenta)).Returns(ingresos);

            var controller = new CuentaController(cuentaMock.Object);
            var listarIngresos = controller.ListarIngresos(1) as ViewResult;
            Assert.AreEqual(listarIngresos.Model, ingresos);
        }


        [Test]
        // TEST PARA LISTAR LOS GASTOS DE UNA CUENTA
        public void Test7()
        {
            IQueryable<Gastos> gastos = new List<Gastos>() as IQueryable<Gastos>;
            var cuenta = new Cuentas { Id = 10 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.ListarGastos(cuenta)).Returns(gastos);

            var controller = new CuentaController(cuentaMock.Object);
            var listarGastos = controller.ListarGastos(1);
            Assert.IsInstanceOf<ViewResult>(listarGastos);
        }


        [Test]
        // TEST PARA LISTAR LOS GASTOS DE UNA CUENTA, MODEL
        public void Test8()
        {
            IQueryable<Gastos> gastos = new List<Gastos>() as IQueryable<Gastos>;
            var cuenta = new Cuentas { Id = 10 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.ListarGastos(cuenta)).Returns(gastos);

            var controller = new CuentaController(cuentaMock.Object);
            var listarGastos = controller.ListarGastos(1) as ViewResult;
            Assert.AreEqual(listarGastos.Model, gastos);
        }


        [Test]
        // TEST PARA REGISTRAR GASTOS DE UNA CUENTA VISTA
        public void Test9()
        {
            var cuentaMock = new Mock<ICuentaService>();
            var controller = new CuentaController(cuentaMock.Object);
            var registrarGasto = controller.RegistrarGasto() as ViewResult;
            Assert.IsInstanceOf<ViewResult>(registrarGasto);
        }


        [Test]
        // TEST PARA REGISTRAR GASTOS DE UNA CUENTA SI EL SALDO ES MENOR AL DEL GASTO NO DEBE REDIRIGIR
        public void Test10()
        {
            var gasto = new Gastos { Id = 10, CuentaId = 1, Monto = 100 };
            var cuenta = new Cuentas { Id = 1, Saldo = 90 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.RegistrarGasto(gasto));
            var controller = new CuentaController(cuentaMock.Object);
            var registrarGasto = controller.RegistrarGasto(gasto) as ViewResult;
            Assert.IsInstanceOf<ViewResult>(registrarGasto);
        }

        [Test]
        // TEST PARA REGISTRAR GASTOS DE UNA CUENTA SI EL SALDO ES MAYOR AL DEL GASTO DEBE REDIRIGIR
        public void Test11()
        {
            IQueryable<Gastos> gastos = new List<Gastos>() as IQueryable<Gastos>;
            var gasto = new Gastos { Id = 10, CuentaId = 1, Monto =100 };
            var cuenta = new Cuentas { Id = 1, Saldo = 1000 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.RegistrarGasto(gasto));
            cuentaMock.Setup(o => o.ListarGastos(cuenta)).Returns(gastos);
            var controller = new CuentaController(cuentaMock.Object);
            var registrarGasto = controller.RegistrarGasto(gasto);
            Assert.IsInstanceOf<RedirectToActionResult>(registrarGasto);
        }


        [Test]
        // TEST PARA REGISTRAR INGRESOS DE UNA CUENTA, VISTA
        public void Test12()
        {
            var cuentaMock = new Mock<ICuentaService>();
            var controller = new CuentaController(cuentaMock.Object);
            var crear = controller.RegistrarIngreso() as ViewResult;
            Assert.IsInstanceOf<ViewResult>(crear);
        }

        [Test]
        // TEST PARA REGISTRAR INGRESOS DE UNA CUENTA, MODEL
        public void Test13()
        {
            var ingreso = new Ingresos { Id = 10, Monto = 100, CuentaId = 1 };
            var cuenta = new Cuentas { Id = 1, Saldo = 1000 };
            var cuentaMock = new Mock<ICuentaService>();
            cuentaMock.Setup(o => o.GetCuenta(1)).Returns(cuenta);
            cuentaMock.Setup(o => o.RegistrarIngreso(ingreso));
            var controller = new CuentaController(cuentaMock.Object);
            var registrarIngreso = controller.RegistrarIngreso(ingreso);
            Assert.IsInstanceOf<RedirectToActionResult>(registrarIngreso);
        }
    }
}
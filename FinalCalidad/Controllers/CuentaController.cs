using FinalCalidad.DB;
using FinalCalidad.Models;
using FinalCalidad.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCalidad.Controllers
{

    public class CuentaController : Controller
    {
        private ICuentaService service;
        public CuentaController(ICuentaService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var cuentas = service.GetCuentas();
            ViewBag.Propio = service.CalcularSaldoPropio(cuentas);
            ViewBag.Credito = service.CalcularSaldoCredito(cuentas);
            return View(cuentas);
        }


        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Cuentas cuenta)
        {
            service.Registrar(cuenta);
            return RedirectToAction("Index");
        }

        public ActionResult ListarIngresos(int id)
        {
            var cuenta = service.GetCuenta(id);
            var ingresos = service.ListarIngresos(cuenta);
            ViewBag.Cuenta = cuenta;
            return View(ingresos);
        }

        public ActionResult ListarGastos(int id)
        {
            var cuenta = service.GetCuenta(id);
            var gastos = service.ListarGastos(cuenta);
            ViewBag.Cuenta = cuenta;
            return View(gastos);
        }

        [HttpGet]
        public ActionResult RegistrarGasto()
        {
            ViewBag.cuentas = service.GetCuentas();
            return View(new Gastos());
        }
        [HttpPost]
        public ActionResult RegistrarGasto(Gastos gasto)
        {
            var cuenta = service.GetCuenta(gasto.CuentaId);

            if (gasto.Monto <= cuenta.Saldo) {
                service.RegistrarGasto(gasto);
                return RedirectToAction("ListarGastos", new { id = gasto.CuentaId });
            }
            ViewBag.cuentas = service.GetCuentas();
            ModelState.AddModelError("Monto", "El monto ingresado supera el saldo de la cuenta");
            return View(gasto);

        }

        [HttpGet]
        public ActionResult RegistrarIngreso()
        {
            ViewBag.cuentas = service.GetCuentas();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarIngreso(Ingresos ingreso)
        {
            service.RegistrarIngreso(ingreso);
            return RedirectToAction("ListarIngresos",new {id = ingreso.CuentaId });
        }
        
    }
}

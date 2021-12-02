using FinalCalidad.DB;
using FinalCalidad.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalCalidad.Services
{
    public interface ICuentaService
    {
        public List<Cuentas> GetCuentas();
        public double CalcularSaldoPropio(List<Cuentas> cuentas);
        public double CalcularSaldoCredito(List<Cuentas> cuentas);
        public void Registrar(Cuentas cuenta);
        public Cuentas GetCuenta(int id);
        public IQueryable<Ingresos> ListarIngresos(Cuentas cuenta);
        public IQueryable<Gastos> ListarGastos(Cuentas cuenta);
        public void RegistrarGasto(Gastos gasto);
        public void RegistrarIngreso(Ingresos ingreso);
    }

    public class CuentaService : ICuentaService
    {
        private AppFinalContext context;

        public CuentaService(AppFinalContext context)
        {
            this.context = context;
        }

        public List<Cuentas> GetCuentas()
        {
            var cuentas = context.Cuentas.ToList();
            return cuentas;
        }

        public double CalcularSaldoPropio(List<Cuentas> cuentas)
        {
            double saldoPropio = 0;
            double deuda = 0;

            foreach (var item in cuentas)
            {
                if (item.Categoria == "Propia")
                {
                    saldoPropio = saldoPropio + item.Saldo;
                }
                else
                {
                    var gastos = context.Gastos.Where(o => o.CuentaId == item.Id).ToList();
                    var ingresos = context.Ingresos.Where(o => o.CuentaId == item.Id).ToList();
                    foreach (var ingreso in ingresos)
                    {
                        deuda = deuda + ingreso.Monto;
                    }
                    foreach (var gasto in gastos)
                    {
                        deuda = deuda - gasto.Monto;
                    }
                }
            }
            if (deuda < 0)
            {
                saldoPropio = saldoPropio + deuda;
            }
            return saldoPropio;
        }


        public double CalcularSaldoCredito(List<Cuentas> cuentas)
        {
            double saldo = 0;
            foreach (var item in cuentas)
            {
                saldo = saldo + item.Saldo;

            }
            return saldo;
        }


        public void Registrar(Cuentas cuenta)
        {
            var usuario = GetUsuario();
            cuenta.UsuarioId = usuario.Id;
            context.Cuentas.Add(cuenta);
            context.SaveChanges();
        }

       

        public Cuentas GetCuenta(int id)
        {
            return context.Cuentas.Where(o => o.Id == id).FirstOrDefault();
        }


        public IQueryable<Ingresos> ListarIngresos(Cuentas cuenta)
        {
            var ingresos = context.Ingresos.Where(o => o.CuentaId == cuenta.Id);
            return ingresos;
        }

        public IQueryable<Gastos> ListarGastos(Cuentas cuenta)
        {
            var gastos = context.Gastos.Where(o => o.CuentaId == cuenta.Id);
            return gastos;
        }

        public void RegistrarGasto(Gastos gasto)
        {
            var cuenta = context.Cuentas.Find(gasto.CuentaId);

            if (gasto.Monto <= cuenta.Saldo)
            {

                cuenta.Saldo = cuenta.Saldo - gasto.Monto;
                context.Gastos.Add(gasto);
                context.SaveChanges();
            }
        }

        public void RegistrarIngreso(Ingresos ingreso)
        {
            var cuenta = context.Cuentas.Find(ingreso.CuentaId);
            cuenta.Saldo = cuenta.Saldo + ingreso.Monto;
            context.Ingresos.Add(ingreso);
            context.SaveChanges();
        }

    }
}

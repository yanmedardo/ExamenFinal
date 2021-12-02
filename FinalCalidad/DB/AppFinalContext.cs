using FinalCalidad.DB.Maps;
using FinalCalidad.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCalidad.DB
{
    public class AppFinalContext : DbContext
    {
      
        public DbSet<Cuentas> Cuentas { get; set; }
        public DbSet<Ingresos> Ingresos { get; set; }
        public DbSet<Gastos> Gastos { get; set; }


        public AppFinalContext(DbContextOptions<AppFinalContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
            modelBuilder.ApplyConfiguration(new CuentaMaps());
            modelBuilder.ApplyConfiguration(new GastoMaps());
            modelBuilder.ApplyConfiguration(new IngresoMaps());

        }
    }
}

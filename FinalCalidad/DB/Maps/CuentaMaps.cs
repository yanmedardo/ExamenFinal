using FinalCalidad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCalidad.DB.Maps
{
    public class CuentaMaps : IEntityTypeConfiguration<Cuentas>
    {
        public void Configure(EntityTypeBuilder<Cuentas> builder)
        {
            builder.ToTable("Cuenta");
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Gastos).WithOne().HasForeignKey(o => o.CuentaId);
            builder.HasMany(o => o.Ingresos).WithOne().HasForeignKey(o => o.CuentaId);

        }
    }
}
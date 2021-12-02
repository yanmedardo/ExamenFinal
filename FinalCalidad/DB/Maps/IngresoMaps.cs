using FinalCalidad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCalidad.DB.Maps
{
    public class IngresoMaps : IEntityTypeConfiguration<Ingresos>
    {
        public void Configure(EntityTypeBuilder<Ingresos> builder)
        {
            builder.ToTable("Ingreso");
            builder.HasKey(o => o.Id);
        }
    }
}
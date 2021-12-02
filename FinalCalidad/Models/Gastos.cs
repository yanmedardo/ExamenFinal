using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCalidad.Models
{
    public class Gastos
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        
        public DateTime FechaHora { get; set; }
        public double Monto { get; set; }
        public string Descripcion { get; set; }
    }
}

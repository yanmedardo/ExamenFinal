using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCalidad.Models
{
    public class Cuentas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Saldo { get; set; }
        public int UsuarioId { get; set; }
        public string Categoria { get; set; }
        public Usuarios Usuario { get; set; }
        public List<Ingresos> Ingresos { get; set; }
        public List<Gastos> Gastos { get; set; }

    }
}

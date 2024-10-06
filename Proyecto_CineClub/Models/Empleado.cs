using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_CineClub.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public int IdPersona { get; set; }
        public DateTime FechaContratacion { get; set; }        
        public double Salario { get; set; }
        public int IdTipoEmpleado { get; set; }        
        public bool EstadoLaboral{ get; set; }
    }
}
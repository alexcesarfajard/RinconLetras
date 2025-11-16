using System;
using System.Collections.Generic;
using System.Linq;

namespace RinconLetras.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int Identificacion { get; set; }

        public int IdRol { get; set; }
        public int Activo { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
    }
}

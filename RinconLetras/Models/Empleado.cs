using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RinconLetras.Models
{
    public class Empleado
    {
        public int IdEmpleados { get; set; }

        public string NombreEmpleado { get; set; }

        public int NumeroCarnet { get; set; }

        public string CorreoElectronico { get; set; }

        public string Contrasenna { get; set; }

        public string Nombre_puesto { get; set; }


    }
}
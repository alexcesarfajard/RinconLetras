using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RinconLetras.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public int Identificacion {  get; set; }
        public int IdRol { get; set; }
        public int Activo { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }


    }
}
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

        public long TarjetaCliente { get; set; }

        public string Correo { get; set; }

    }
}
using RinconLetras.EF;
using RinconLetras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RinconLetras.Controllers
{
    public class ClientesController : Controller
    {

        [HttpGet]
        public ActionResult RegistrarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCliente(Cliente cliente)
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var resultado = context.RegistrarCliente(cliente.NombreCliente, cliente.FechaNacimiento, cliente.Identificacion, cliente.CorreoElectronico, cliente.Contrasenna);

                /* alex
                Sí se están guardando los clientes en la base de datos pero no aparece el mensaje "información registrada"
                en cambio, pasa directamente al Else y se muestra que hubo un error. REVISAR
                */

                if(resultado > 0)
                {
                    ViewBag.Mensaje = "Información registrada";
                    return RedirectToAction("RegistrarCliente", "ClientesController");
                } 
                else
                {
                    ViewBag.Mensaje = "Hubo un error al guardar";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult VerClientes()
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var lista = context.Tb_Usuarios
                    .Where(e => e.IdRol == 1) //Filtro para traer solo los usuarios tengan rol 1 (Usuario cliente)
                    .Select(e => new Cliente
                    {
                        IdCliente = e.IdUsuario,
                        NombreCliente = e.NombreUsuario,
                        FechaNacimiento = (System.DateTime)e.FechaNacimiento,
                        Identificacion = (int)e.Identificacion,
                        IdRol = (int)e.IdRol,
                        Activo = (int)e.Activo,
                        CorreoElectronico = e.CorreoElectronico,
                        Contrasenna = e.Contrasenna
                    }).ToList();

                return View(lista);
            }
        }


    }
}
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
            using (var context = new RinconLetrasBDEntities())
            {
                var resultado = context.RegistrarCliente(cliente.NombreCliente, cliente.TarjetaCliente, cliente.Correo);

                /* 
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
            using(var context = new RinconLetrasBDEntities())
            {
                //Traer el objeto de la BD
                var resultado = context.Tb_Clientes.ToList();

                //Objeto propio
                var datos = resultado.Select(p => new Cliente()
                {
                    IdCliente = p.IdCliente,
                    NombreCliente = p.NombreCliente,
                    TarjetaCliente = p.TarjetaCliente,
                    Correo = p.Correo
                }).ToList();

                return View(datos);
            }

            
        }

    }
}
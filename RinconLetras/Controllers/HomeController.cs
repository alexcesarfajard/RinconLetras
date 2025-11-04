using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using RinconLetras.EF;
using RinconLetras.Models;
using RinconLetras.Services;

namespace RinconLetras.Controllers
{
    public class HomeController : Controller
    {

        #region Iniciar sesion

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Empleado empleado)
        {

            using (var context = new RinconLetrasBDEntities())
            {
                var resultado = context.ValidarEmpleado(empleado.CorreoElectronico, empleado.Contrasenna).FirstOrDefault();

                if (resultado != null)
                {
                    Session["NombreEmpleado"] = resultado.NombreEmpleado;
                    Session["IdEmpleados"] = resultado.IdEmpleados;
                    return RedirectToAction("Principal", "Home");
                }

                ViewBag.Mensaje = "La informacion no se ha podido autenticar";
                return View();
            }

        }

        #endregion

        public ActionResult RecuperarAcceso()
        {
            return View();
        }

        [Seguridad]
        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}
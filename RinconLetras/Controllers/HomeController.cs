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

            using (var context = new RinconLetrasBDEntities1())
            {
                var resultado = context.Tb_Usuarios.Include("Tb_Roles")
                    .Where(x => x.CorreoElectronico == empleado.CorreoElectronico && x.Contrasenna == empleado.Contrasenna && x.Activo == 1)
                    .FirstOrDefault();

                //var resultado = context.ValidarUsuario(empleado.CorreoElectronico, empleado.Contrasenna).FirstOrDefault();

                if (resultado != null)
                {
                    Session["Nombre"] = resultado.NombreUsuario;
                    Session["IdUsuario"] = resultado.IdUsuario;
                    Session["IdRol"] = resultado.IdRol;
                    Session["NombreRol"] = resultado.Tb_Roles.NombreRol;
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


        [HttpGet]
        public ActionResult Usuario()
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                try
                {
                    var resultado = context.Tb_Libros
                        .Include("Tb_Editoriales")
                        .Include("Tb_Generos")
                        .ToList();

                    var datos = resultado.Select(l => new Libro
                    {
                        IdLibro = l.IdLibro,
                        Nombre = l.Nombre,
                        Precio = (decimal)l.Precio,
                        CantidadInventario = (int)l.CantidadInventario,
                        Editorial = l.Tb_Editoriales != null ? l.Tb_Editoriales.Nombre : "Sin editorial",
                        Genero = l.Tb_Generos != null ? l.Tb_Generos.Nombre : "Sin género",
                        Activo = (bool)l.Activo,
                        Imagen = l.Imagen
                    }).ToList();

                    return View(datos);
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error al cargar el catálogo: " + ex.Message;
                    return View(new List<Libro>());
                }
            }
        }
    }
}
using RinconLetras.EF;
using RinconLetras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RinconLetras.Controllers
{
    public class AutoresController : Controller
    {
        [HttpGet]
        public ActionResult VerAutores()
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var listaAutores = context.Tb_Autores
                    .Select(a => new Autor
                    {
                        IdAutor = a.IdAutor,
                        Nombre_Autor = a.Nombre_Autor,
                        Nacionalidad = a.Nacionalidad
                    }).ToList();

                return View(listaAutores);
            }
        }

        [HttpGet]
        public ActionResult RegistrarAutor()
        {
            CargarNacionalidades();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarAutor(Autor autor)
        {

            CargarNacionalidades(); //Volver a cargar por si hay algun error

            using (var context = new RinconLetrasBDEntities1())
            {
                var nuevoAutor = new Tb_Autores
                {
                    Nombre_Autor = autor.Nombre_Autor,
                    Nacionalidad = autor.Nacionalidad
                };

                context.Tb_Autores.Add(nuevoAutor);

                var resultado = context.SaveChanges();

                if (resultado > 0)
                {
                    TempData["Mensaje"] = "Información registrada correctamente";
                    return RedirectToAction("RegistrarAutor", "Autores");
                }

                TempData["Mensaje"] = "Ocurrio un error al registrar la informacion";
                return View();
            }
        }

        private void CargarNacionalidades()
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var lista = context.Tb_Nacionalidades
                    .Select(n => new SelectListItem
                    {
                        Value = n.Nacionalidad,
                        Text = n.Nacionalidad
                    }).ToList();

                lista.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione una nacionalidad"
                });

                ViewBag.Nacionalidades = lista;
            }
        }


    }
}
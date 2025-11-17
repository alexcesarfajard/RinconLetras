using Microsoft.Ajax.Utilities;
using RinconLetras.EF;
using RinconLetras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RinconLetras.Controllers
{
    public class GenerosController : Controller
    {
        [HttpGet]
        public ActionResult VerGeneros()
        {

            using (var context = new RinconLetrasBDEntities1())
            {
                var listaGeneros = context.Tb_Generos
                    .Select(g => new Genero
                    {
                        IdGenero = g.IdGenero,
                        Nombre = g.Nombre,
                        Descripcion = g.Descripcion,
                    }).ToList();

                return View(listaGeneros);
            }
            
        }

        [HttpGet]
        public ActionResult RegistrarGenero()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarGenero(Genero genero)
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var nuevoGenero = new Tb_Generos
                {
                    Nombre = genero.Nombre,
                    Descripcion = genero.Descripcion
                };

                context.Tb_Generos.Add(nuevoGenero);

                var resultado = context.SaveChanges();

                if (resultado > 0)
                {
                    ViewBag.Mensaje = ("Informacion registrada correctamente");
                    return RedirectToAction("RegistrarGenero", "Generos");
                }

                ViewBag.Mensaje = ("Error al guardar la informacion");
                return View();
            }

        }

    }
}
using RinconLetras.EF;
using RinconLetras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RinconLetras.Controllers
{
    public class EditorialesController : Controller
    {

        [HttpGet]
        public ActionResult VerEditoriales()
        {
            
            using (var context = new RinconLetrasBDEntities1())
            {
                var listaEditoriales = context.Tb_Editoriales.Select(e => new Editorial
                {
                    IdEditorial = e.IdEditorial,
                    Nombre = e.Nombre,
                    Direccion = e.Direccion
                }).ToList();

                return View(listaEditoriales);
            }
        }

        [HttpGet]
        public ActionResult RegistrarEditorial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEditorial(Editorial editorial)
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var nuevoEditorial = new Tb_Editoriales
                {
                    Nombre = editorial.Nombre,
                    Direccion = editorial.Direccion
                };

                context.Tb_Editoriales.Add(nuevoEditorial);

                var resultado = context.SaveChanges();

                if (resultado > 0)
                {
                    ViewBag.Mensaje = ("Informacion registrada correctamente");
                    return RedirectToAction("RegistrarEditorial", "Editoriales");
                }

                ViewBag.Mensaje = ("Error al guardar la informacion");
                return View();

            }
        }


    }
}
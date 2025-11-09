using RinconLetras.EF;
using RinconLetras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace RinconLetras.Controllers
{
    public class CatalogoController : Controller
    {
        [HttpGet]
        public ActionResult VerCatalogo()
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
                        Genero = l.Tb_Generos != null ? l.Tb_Generos.Nombre : "Sin género"
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
            [HttpGet]
            public ActionResult RegistrarLibro()
            {
                var context = new RinconLetrasBDEntities1();

                var listaEditoriales = context.Tb_Editoriales.ToList();
                ViewBag.Editoriales = new SelectList(listaEditoriales, "IdEditorial", "Nombre");

                var listaGeneros = context.Tb_Generos.ToList();
                ViewBag.Generos = new SelectList(listaGeneros, "IdGenero", "Nombre");

                return View();
            }

            // POST: RegistrarLibro
            [HttpPost]
            public ActionResult RegistrarLibro(Libro libro)
            {
                ViewBag.Mensaje = "No se pudo registrar el libro.";

                using (var context = new RinconLetrasBDEntities1())
                {
                    // Validar si existe un libro con el mismo nombre
                    var existe = context.Tb_Libros
                        .FirstOrDefault(x => x.Nombre == libro.Nombre);

                    if (existe == null)
                    {
                        var nuevoLibro = new Tb_Libros
                        {
                            Nombre = libro.Nombre,
                            Precio = libro.Precio,
                            CantidadInventario = libro.CantidadInventario,
                            IdEditorial = int.Parse(Request["IdEditorial"]),
                            IdGenero = int.Parse(Request["IdGenero"])
                        };

                        context.Tb_Libros.Add(nuevoLibro);
                        context.SaveChanges();

                        return RedirectToAction("VerCatalogo");
                    }

                    ViewBag.Mensaje = " El libro ya está registrado.";

                    
                    var listaEditoriales = context.Tb_Editoriales.ToList();
                    ViewBag.Editoriales = new SelectList(listaEditoriales, "IdEditorial", "Nombre");

                    var listaGeneros = context.Tb_Generos.ToList();
                    ViewBag.Generos = new SelectList(listaGeneros, "IdGenero", "Nombre");

                    return View(libro);
                }
            }
        }
    }

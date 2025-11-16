using RinconLetras.EF;
using RinconLetras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        // Registrar libro 
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

        [HttpPost]
        public ActionResult RegistrarLibro(Libro libro, HttpPostedFileBase archivoImagen)
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                try
                {
                    if (archivoImagen != null && archivoImagen.ContentLength > 0)
                    {
                       
                        string rutaCarpeta = Server.MapPath("~/Images/libros/");
                        if (!System.IO.Directory.Exists(rutaCarpeta))
                            System.IO.Directory.CreateDirectory(rutaCarpeta);

                   
                        string nombreArchivo = System.IO.Path.GetFileName(archivoImagen.FileName);
                        string rutaCompleta = System.IO.Path.Combine(rutaCarpeta, nombreArchivo);
                        archivoImagen.SaveAs(rutaCompleta);

                        libro.Imagen = "/Images/libros/" + nombreArchivo;
                    }

                    var nuevoLibro = new Tb_Libros
                    {
                        Nombre = libro.Nombre,
                        Precio = libro.Precio,
                        CantidadInventario = libro.CantidadInventario,
                        IdEditorial = int.Parse(Request["IdEditorial"]),
                        IdGenero = int.Parse(Request["IdGenero"]),
                        Imagen = libro.Imagen,
                        Activo = true 
                    };

                    context.Tb_Libros.Add(nuevoLibro);
                    context.SaveChanges();

                    return RedirectToAction("VerCatalogo");
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error al registrar el libro: " + ex.Message;
                    return View(libro);
                }
            }
        }


        // Editar libro 
        [HttpGet]
        public ActionResult EditarLibro(int id)
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var libro = context.Tb_Libros.Find(id);

                if (libro == null)
                {
                    return RedirectToAction("VerCatalogo");
                }

                var modelo = new Libro
                {
                    IdLibro = libro.IdLibro,
                    Nombre = libro.Nombre,
                    Precio = (decimal)libro.Precio,
                    CantidadInventario = (int)libro.CantidadInventario,
                    Editorial = libro.IdEditorial.ToString(),
                    Genero = libro.IdGenero.ToString(),
                    Imagen = libro.Imagen // <- importante
                };

                ViewBag.Editoriales = new SelectList(context.Tb_Editoriales.ToList(), "IdEditorial", "Nombre", libro.IdEditorial);
                ViewBag.Generos = new SelectList(context.Tb_Generos.ToList(), "IdGenero", "Nombre", libro.IdGenero);

                return View(modelo);
            }
        }


        [HttpPost]
        public ActionResult EditarLibro(Libro libro, HttpPostedFileBase archivoImagen)
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                try
                {
                    var existente = context.Tb_Libros.Find(libro.IdLibro);

                    if (existente != null)
                    {
                        existente.Nombre = libro.Nombre;
                        existente.Precio = libro.Precio;
                        existente.CantidadInventario = libro.CantidadInventario;
                        existente.IdEditorial = int.Parse(Request["IdEditorial"]);
                        existente.IdGenero = int.Parse(Request["IdGenero"]);

                        // Si se sube una nueva imagen
                        if (archivoImagen != null && archivoImagen.ContentLength > 0)
                        {
                            string rutaCarpeta = Server.MapPath("~/Images/libros/");
                            if (!System.IO.Directory.Exists(rutaCarpeta))
                                System.IO.Directory.CreateDirectory(rutaCarpeta);

                            string nombreArchivo = System.IO.Path.GetFileName(archivoImagen.FileName);
                            string rutaCompleta = System.IO.Path.Combine(rutaCarpeta, nombreArchivo);
                            archivoImagen.SaveAs(rutaCompleta);

                            existente.Imagen = "/Images/libros/" + nombreArchivo;
                        }

                        context.SaveChanges();
                    }

                    return RedirectToAction("VerCatalogo");
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Error al actualizar el libro: " + ex.Message;

                    // Recargar listas desplegables en caso de error
                    ViewBag.Editoriales = new SelectList(context.Tb_Editoriales.ToList(), "IdEditorial", "Nombre");
                    ViewBag.Generos = new SelectList(context.Tb_Generos.ToList(), "IdGenero", "Nombre");

                    return View(libro);
                }
            }
        }


        //  Activar/Inactivar libro
        [HttpGet]
        public ActionResult CambiarEstado(int id)
        {
            using (var db = new RinconLetrasBDEntities1())
            {
                var libro = db.Tb_Libros.Find(id);

                if (libro != null)
                {
                    libro.Activo = !libro.Activo;
                    db.SaveChanges();
                }

                return RedirectToAction("VerCatalogo");
            }
        }
    }
}

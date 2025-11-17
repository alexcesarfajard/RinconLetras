using RinconLetras.EF;
using RinconLetras.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RinconLetras.Controllers
{
    public class EmpleadosController : Controller
    {

        [HttpGet]
        public ActionResult VerEmpleados()
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                var lista = context.Tb_Usuarios
                    .Where(e => e.IdRol == 2 || e.IdRol == 3) //Filtro para traer solo los usuarios tengan rol 2 o 3 (vendedor o administrador)
                    .Select(e => new Empleado
                    {
                        IdEmpleado = e.IdUsuario,
                        NombreEmpleado = e.NombreUsuario,
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

        [HttpGet]
        public ActionResult RegistrarEmpleado()
        {
            CargarRoles();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEmpleado(Empleado empleado)
        {

            CargarRoles(); // volver a cargar en caso de que haya algun error

            using (var context = new RinconLetrasBDEntities1())
            {
                var resultado = context.RegistrarEmpleado(
                    empleado.NombreEmpleado,
                    empleado.FechaNacimiento,
                    empleado.Identificacion,
                    empleado.CorreoElectronico,
                    empleado.Contrasenna,
                    empleado.IdRol
                );

                // se esta guardando pero muestra error al guardar la informacion REVISAR

                if (resultado > 0)
                {
                    ViewBag.Mensaje = "Informacion registrada correctamente";
                    return RedirectToAction("RegistrarEmpleado", "Empleados");
                }
                else
                {
                    ViewBag.Mensaje = "Error al guardar la informacion";
                    return View();
                }
            }
        }

        private void CargarRoles()
        {
            using (var context = new RinconLetrasBDEntities1())
            {
                //Consulta a la BD
                var resultado = context.Tb_Roles
                    .Where(r => r.IdRol == 2 || r.IdRol == 3)
                    .ToList();

                //Convertirlo en un objeto SelectListItem
                var datos = resultado.Select(c => new SelectListItem
                {
                    Value = c.IdRol.ToString(),
                    Text = c.NombreRol
                }).ToList();

                datos.Insert(0, new SelectListItem
                {
                    Value = string.Empty,
                    Text = "Seleccione"
                });

                ViewBag.ListaRoles = datos;
            }
        }


    }
}

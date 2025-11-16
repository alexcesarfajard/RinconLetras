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
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEmpleado(Empleado empleado)
        {
            return View();
        }


    }
}

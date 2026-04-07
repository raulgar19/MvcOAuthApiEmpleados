using Microsoft.AspNetCore.Mvc;
using MvcOAuthApiEmpleados.Models;
using MvcOAuthApiEmpleados.Services;

namespace MvcOAuthApiEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadosController(ServiceEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();

            return View(empleados);
        }

        public async Task<IActionResult> Details(int idempleado)
        {
            string token = HttpContext.Session.GetString("TOKEN");

            if (token == null)
            {
                ViewData["MENSAJE"] = "Debe hacer Login";
            }
            else
            {
                Empleado empleado = await this.service.FindEmpleadoAsync(idempleado, token);
                return View(empleado);
            }
        }
    }
}
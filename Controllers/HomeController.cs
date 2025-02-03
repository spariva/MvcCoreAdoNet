using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;

namespace MVCCoreNetEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EjemploSection()
        {
            return View();  
        }

        public IActionResult VistaPersona()
        {
            Persona persona = new Persona();
            persona.Nombre = "Alek";
            persona.Email = "email";
            persona.Edad = 32;
            return View(persona);
        }
    }
}

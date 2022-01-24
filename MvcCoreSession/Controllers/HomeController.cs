using MvcCoreSession.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSession.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult EjemploSession(string accion)
        {
            if (accion == "almacenar")
            {
                Persona persona = new Persona();
                persona.Nombre = "Alumno";
                persona.Edad = 22;
                persona.Hora = DateTime.Now.ToLongTimeString();
                //ALMACENAMOS EL OBJETO EN SESSION EN SESSION
                HttpContext.Session.SetObject("PERSONA", persona);
                ViewData["MENSAJE"] = "Datos almacenados en Session";
            }
            else if (accion == "mostrar")
            {
                Persona persona =
                    HttpContext.Session.GetObject<Persona>("PERSONA");
                ViewData["usuario"] = persona.Nombre + ", Edad: " + persona.Edad;
                ViewData["hora"] = persona.Hora;
            }
            return View();
        }

        public IActionResult ColeccionSession(string accion)
        {
            if (accion == "almacenar")
            {
                List<Persona> personas = new List<Persona>
                {
                new Persona { Nombre = "Antonia", Edad = 46
                , Hora = DateTime.Now.ToLongTimeString()},
                new Persona { Nombre = "Andres", Edad = 39
                , Hora = DateTime.Now.ToLongTimeString()},
                new Persona { Nombre = "Carlos", Edad = 41
                , Hora = DateTime.Now.ToLongTimeString()}
                };
                List<int> numeros =
                    new List<int> { 4, 5, 6, 7, 8, 8, 99 };
                HttpContext.Session.SetObject("NUMEROS", numeros);
                HttpContext.Session.SetObject("PERSONAS", personas);
                ViewData["MENSAJE"] = "Datos almacenados";
                return View();
            }
            else if (accion == "mostrar")
            {
                List<Persona> personas =
                    HttpContext.Session.GetObject<List<Persona>>("PERSONAS");
                return View(personas);
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
                //MEDIANTE EL HELPER CONVERTIMOS EL OBJETO PERSONA
                //A STRING JSON
                string data =
                    HelperSession.SerializeObject(persona);
                //ALMACENAMOS EL OBJETO COMO STRING EN SESSION
                HttpContext.Session.SetString("PERSONA", data);
                ViewData["MENSAJE"] = "Datos almacenados en Session";
            }
            else if (accion == "mostrar")
            {
                //EXTRAEMOS LA INFORMACION DE STRING JSON DE SESSION
                string data = HttpContext.Session.GetString("PERSONA");
                //CONVERTIMOS EL JSON A OBJETO
                Persona persona = 
                    HelperSession.DeserializeObject<Persona>(data);

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
                string jsonnumeros =
                    HelperSession.SerializeObject(numeros);
                HttpContext.Session.SetString("NUMEROS", jsonnumeros);
                string data =
                    HelperSession.SerializeObject(personas);
                HttpContext.Session.SetString("PERSONAS", data);
                ViewData["MENSAJE"] = "Datos almacenados";
                return View();
            }
            else if (accion == "mostrar")
            {
                string data =
                    HttpContext.Session.GetString("PERSONAS");
                List<Persona> personas = 
                    HelperSession.DeserializeObject<List<Persona>>(data);
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

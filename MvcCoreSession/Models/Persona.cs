using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSession.Models
{
    [Serializable]
    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Hora { get; set; }
    }
}

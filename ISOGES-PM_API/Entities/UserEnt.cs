using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class UserEnt //Empleado
    {
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Identificacion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public int TipoUsuario { get; set; }
        public bool Estado { get; set; }
    }
}
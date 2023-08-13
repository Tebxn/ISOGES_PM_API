using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class UsuarioEnt //Empleado
    {
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Identificacion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public int TipoUsuario { get; set; }
        public bool Estado { get; set; }
        public string Contrasena { get; set; }
        public int Puesto { get; set; }
        public bool PassIsTemp { get; set; }
        public string NombreTipoUsuario { get; set; } //tipo usuario
        public string NombrePuesto { get; set; } //puesto
        public string NuevaContrasena { get; set; } //local
        public string Token { get; set; } //local
    }

    public class UsuarioResponse : Response
    {
        public UsuarioEnt ObjectSingle { get; set; } = new UsuarioEnt();
        public List<UsuarioEnt> ObjectList { get; set; } = new List<UsuarioEnt>();
    }
}
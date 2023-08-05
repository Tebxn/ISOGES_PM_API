using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class CorreoEnt
    {
        public long IdCorreo { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public DateTime Fecha { get; set; }
        public string Remitente { get; set; }
        public long ClienteCorreo { get; set; }
        public string NombreCorreo { get; set; }
        public bool Estado { get; set; }
    }

    public class CorreoResp
    {
        public CorreoEnt ObjectSingle { get; set; } = new CorreoEnt();
        public List<CorreoEnt> ObjectList { get; set; } = new List<CorreoEnt>();

        public string mensajeUsuario { get; set; }

        public int booleano { get; set; }
    }
}
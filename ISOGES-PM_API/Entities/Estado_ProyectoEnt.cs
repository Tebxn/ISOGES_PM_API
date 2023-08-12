using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class Estado_ProyectoEnt
    {
        public long IdEstadoProyecto { get; set; }
        public string NombreEstado { get; set; }
    }

    public class Estado_ProyectoResp
    {
        public Estado_ProyectoEnt ObjectSingle { get; set; } = new Estado_ProyectoEnt();
        public List<Estado_ProyectoEnt> ObjectList { get; set; } = new List<Estado_ProyectoEnt>();

        public string mensajeUsuario { get; set; }
        public int booleano { get; set; }
    }
}
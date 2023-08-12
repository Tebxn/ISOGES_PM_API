using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class ProyectoEnt
    {

        public long IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public long Cliente { get; set; }
        public string NombreCliente { get; set; }
        public bool Estado { get; set; }
        public double MontoEstimado { get; set; }
        public long EstadoGeneral { get; set; }
        public string NombreEstado { get; set; }
    }

    public class ProyectoResponse : Response
    {
        public ProyectoEnt ObjectSingle { get; set; } = new ProyectoEnt();
        public List<ProyectoEnt> ObjectList { get; set; } = new List<ProyectoEnt>();
    }
}
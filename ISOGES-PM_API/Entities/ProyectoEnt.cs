using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class ProyectoEnt
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cliente { get; set; }
        public bool Estado { get; set; }
        public float MontoEstimado { get; set; }

    }
}
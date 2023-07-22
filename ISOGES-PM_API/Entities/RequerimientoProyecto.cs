using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class RequerimientoProyecto
    {
        public string IdProyecto { get; set; }
        public string IdRequerimiento { get; set; }
        public string EmpleadoAsignado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaLimite { get; set; }

    }
}
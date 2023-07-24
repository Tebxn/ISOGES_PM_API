using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class RequerimientoEnt
    {
        public long IdRequerimiento { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string URL { get; set; }
        public bool Estado { get; set; }

    }

    public class RequerimientoResponse : Response
    {
        public RequerimientoEnt ObjectSingle { get; set; } = new RequerimientoEnt();
        public List<RequerimientoEnt> ObjectList { get; set; } = new List<RequerimientoEnt>();
    }
}
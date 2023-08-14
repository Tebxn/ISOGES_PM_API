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
        public string CodigoRequerimiento { get; set; }
        public string NombreRequerimiento { get; set; }
        public string DescripcionRequerimiento { get; set; }
        public string URLRequerimiento { get; set; }
        public bool Estado { get; set; }

    }

    public class RequerimientoResponse : Response
    {
        public RequerimientoEnt ObjectSingle { get; set; } = new RequerimientoEnt();
        public List<RequerimientoEnt> ObjectList { get; set; } = new List<RequerimientoEnt>();
    }
}
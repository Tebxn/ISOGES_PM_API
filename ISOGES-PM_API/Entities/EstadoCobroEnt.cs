using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class EstadoCobroResponse : Response
    {
        public EstadoCobroEnt ObjectSingle { get; set; } = new EstadoCobroEnt();
        public List<EstadoCobroEnt> ObjectList { get; set; } = new List<EstadoCobroEnt>();
    }

    public class EstadoCobroEnt
    {
        public long IdEstadoCobro { get; set; }
        public string NombreEstado { get; set; } 
    }

    
}
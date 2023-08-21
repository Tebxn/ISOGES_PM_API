using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class CobroResponse : Response
    {
        public CobroEnt ObjectSingle { get; set; } = new CobroEnt();
        public List<CobroEnt> ObjectList { get; set; } = new List<CobroEnt>();
    }

    public class CobroEnt
    {
        public long IdCobro { get; set; }
        public int TipoCobro { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstadoCobro { get; set; }
        public double Monto { get; set; }
        public string NombreTipoCobro { get; set; } //TipoCobro
        public string NombreEstado { get; set; } //EstadoCobro
        public string FechaSola { get; set; } //local
        public string NombreProyecto { get; set; }
        public long IdProyecto { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class CobroEnt
    {
        public int TipoCobro { get; set; }
        public DateTime Fecha { get; set; }
        public bool EstadoCobro { get; set; }
        public float Monto { get; set; }
    }
}
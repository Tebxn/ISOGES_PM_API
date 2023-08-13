using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class ChartEnt
    {

    }
    public class ChartAnualIngresoEnt
    {
        public string Anio { get; set; }
        public string Mes { get; set; }
        public string DiaMes { get; set; }
        public double IngresosTotales { get; set; }
    }
    public class ChartMensualIngreso
    {
        public string Mes { get; set; }
        public double MesTotalIngreso { get; set; }
    }
}
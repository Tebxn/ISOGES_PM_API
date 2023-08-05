using ISOGES_PM_API.Entities;
using ISOGES_PM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISOGES_PM_API.Controllers
{
    public class ChartController : ApiController
    {
        [HttpGet]
        [Route("api/CargarChartAnualIngreso")]
        public List<ChartAnualIngresoEnt> CargarChartAnualIngreso()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Cobro
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<ChartAnualIngresoEnt>();

                    foreach (var item in datos)
                    {
                        DateTime dateTime = item.Fecha;
                        DateTime anioSolo = dateTime.Date;
                        DateTime diaMesSolo = dateTime.Date;
                        DateTime mesSolo = dateTime.Date;
                        string anioEnviar = anioSolo.ToString("yyyy");
                        string diaMesEnviar = diaMesSolo.ToString("dd/MM");
                        string mesSoloEnviar = mesSolo.ToString("MM");

                        resp.Add(new ChartAnualIngresoEnt
                        {
                            Anio = anioEnviar,
                            Mes = mesSoloEnviar,
                            DiaMes = diaMesEnviar,
                            IngresosTotales = item.Monto,
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<ChartAnualIngresoEnt>();
                }
            }
        }
    }
}


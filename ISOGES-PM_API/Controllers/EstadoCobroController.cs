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
    [Authorize]
    public class EstadoCobroController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarEstadoCobros")]
        public List<EstadoCobroEnt> ConsultarEstadoCobros()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Estado_Cobro
                             select x).ToList();


                if (datos.Count > 0)
                {
                    var resp = new List<EstadoCobroEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new EstadoCobroEnt
                        {
                            IdEstadoCobro = item.IdEstadoCobro,
                            NombreEstado = item.NombreEstado
                            
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<EstadoCobroEnt>();
                }
            }
        }

    }
}


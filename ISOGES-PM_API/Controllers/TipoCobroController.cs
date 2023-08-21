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
    public class TipoCobroController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarTipoCobros")]
        public List<TipoCobroEnt> ConsultarTipoCobros()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.TipoCobro
                             select x).ToList();


                if (datos.Count > 0)
                {
                    var resp = new List<TipoCobroEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new TipoCobroEnt
                        {
                            IdTipoCobro = item.IdTipoCobro,
                            NombreTipoCobro = item.TipoCobro1
                            
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<TipoCobroEnt>();
                }
            }
        }

    }
}


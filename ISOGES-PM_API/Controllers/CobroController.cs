using ISOGES_PM_API.Entities;
using ISOGES_PM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace ISOGES_PM_API.Controllers
{
     public class CobroController : ApiController
     {
        [HttpGet]
        [Route("api/ListadoCobros")]
        public List<CobroEnt> ListadoCobros()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from r in bd.Cobro
                             join p in bd.Proyecto on r.IdProyecto equals p.IdProyecto
                             join x in bd.Estado_Cobro on r.IdEstadoCobro equals x.IdEstadoCobro
                             join e in bd.TipoCobro on r.TipoCobro equals e.IdTipoCobro
                             select new
                             {
                                 r.IdCobro,
                                 e.TipoCobro1,
                                 r.Fecha,
                                 x.NombreEstado,
                                 r.Monto,
                                 p.NombreProyecto


                             }).ToList();

                if (datos.Count > 0)
                {
                    var lista = new List<CobroEnt>();
                    foreach (var item in datos)
                    {
                        lista.Add(new CobroEnt
                        {
                            IdCobro = item.IdCobro,
                            NombreTipoCobro = item.TipoCobro1,
                            Fecha = item.Fecha,
                            NombreEstado = item.NombreEstado,
                            Monto = item.Monto,
                            NombreProyecto = item.NombreProyecto
                        });
                    }

                    return lista;
                }
                else
                {

                    return new List<CobroEnt>();
                }
            }
        }
    }
}




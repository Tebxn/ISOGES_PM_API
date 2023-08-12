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
   /*  public class CobroController : ApiController
     {
         [HttpGet]
         [Route("api/ConsultarCobrosPorProyecto")]
         public List<CobroEnt> ConsultarCobros(long q)//q=idProyecto
         {
             using (var bd = new ISOGES_PMEntities())
             {
                 var datos = (from x in bd.Proyectos_Cobros
                              join y in bd.Cobro on x.IdCobro equals y.IdCobro
                              join n in bd.Estado_Cobro on y.IdEstadoCobro equals n.IdEstadoCobro
                              join z in bd.TipoCobro on y.TipoCobro equals z.IdTipoCobro
                              where x.IdProyecto == q
                              select new
                              {
                                  y.IdCobro,
                                  y.TipoCobro,
                                  z.TipoCobro1,
                                  y.Fecha,
                                  y.IdEstadoCobro,
                                  n.NombreEstado,
                                  y.Monto
                              }).ToList();

                 if (datos.Count > 0)
                 {
                     var resp = new List<CobroEnt>();
                     foreach (var item in datos)
                     {
                         DateTime dateTime = item.Fecha;
                         DateTime fechaSola = dateTime.Date;
                         TimeSpan horaSola = dateTime.TimeOfDay;
                         string fechaEnviar = fechaSola.ToString("yyyy-MM-dd");

                         resp.Add(new CobroEnt
                         {
                             IdCobro = item.IdCobro,
                             NombreTipoCobro = item.TipoCobro1,
                             TipoCobro = item.TipoCobro,
                             Fecha = item.Fecha,
                             IdEstadoCobro = item.IdEstadoCobro,
                             NombreEstado = item.NombreEstado,
                             Monto = item.Monto,
                             FechaSola = fechaEnviar
                         });
                     }
                     return resp;
                 }
                 else
                 {
                     return new List<CobroEnt>();
                 }
             }
         }


     }*/
}




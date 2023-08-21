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
    public class Requerimiento_ProyectoController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarAgenda")]
        public List<Requerimiento_ProyectoEnt> ConsultarAgenda(long q)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento_Proyecto
                             join y in bd.Proyecto on x.IdProyecto equals y.IdProyecto
                             join z in bd.Requerimiento on x.IdRequerimiento equals z.IdRequerimiento
                             join w in bd.Usuario on x.EmpleadoAsignado equals w.IdUsuario
                             join v in bd.Cliente on y.Cliente equals v.IdCliente
                             where x.EmpleadoAsignado == q
                             select new
                             {
                                 x.IdProyecto,
                                 x.IdRequerimiento,
                                 x.EmpleadoAsignado,
                                 x.FechaInicio,
                                 x.FechaLimite,
                                 x.Estado,

                                 y.NombreProyecto,
                                 y.DescripcionProyecto,
                                 y.MontoEstimado,
                                 
                                 z.NombreRequerimiento,
                                 z.CodigoRequerimiento,
                                 z.DescripcionRequerimiento,
                                 z.URLRequerimiento,

                                 v.NombreCliente
                                 

                             }).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<Requerimiento_ProyectoEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new Requerimiento_ProyectoEnt
                        {
                            IdProyecto = item.IdProyecto,
                            IdRequerimiento = item.IdRequerimiento,
                            EmpleadoAsignado = item.EmpleadoAsignado,
                            FechaInicio = item.FechaInicio,
                            FechaLimite = item.FechaLimite,
                            Estado = (bool)item.Estado,

                            NombreProyecto = item.NombreProyecto,
                            DescripcionProyecto = item.DescripcionProyecto,
                            MontoEstimadoProyecto = (float)item.MontoEstimado,
                            
                            NombreRequerimiento = item.NombreRequerimiento,
                            DescripcionRequerimiento = item.DescripcionRequerimiento,
                            CodigoRequerimiento = item.CodigoRequerimiento,
                            URLRequerimiento = item.URLRequerimiento,

                            NombreClienteProyecto = item.NombreCliente

                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<Requerimiento_ProyectoEnt>();
                }
            }
        }

        [HttpPut]
        [Route("api/CambiarEstadoAgenda")]
        public int CambiarEstadoAgenda(Requerimiento_ProyectoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento_Proyecto
                             where x.IdRequerimiento == entidad.IdRequerimiento
                             && x.IdProyecto == entidad.IdProyecto
                             && x.EmpleadoAsignado == entidad.EmpleadoAsignado
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    bool estadoActual = (bool)datos.Estado;
                    estadoActual = (estadoActual == true ? false : true);

                    datos.Estado = estadoActual;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }
    }
}

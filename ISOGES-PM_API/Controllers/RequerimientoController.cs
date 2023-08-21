
using System.Threading.Tasks;
using ISOGES_PM_API.Entities;
using ISOGES_PM_API.Models;
using ISOGES_PM_API.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

namespace ISOGES_PM_API.Controllers
{
    [Authorize]
    public class RequerimientoController : ApiController
    {

        [HttpPost]
        [Route("api/CrearRequerimiento")]
        public int CrearUsuario(RequerimientoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {

                Requerimiento tabla = new Requerimiento();
                tabla.CodigoRequerimiento = entidad.CodigoRequerimiento; 
                tabla.NombreRequerimiento = entidad.NombreRequerimiento;
                tabla.DescripcionRequerimiento = entidad.DescripcionRequerimiento;
                tabla.Estado = true;
     
                bd.Requerimiento.Add(tabla);
               
                return bd.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/ConsultarRequerimientos")]
        public List<RequerimientoEnt> ConsultarRequerimientos()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento
                             select new
                             {
                                 x.IdRequerimiento,
                                 x.CodigoRequerimiento,
                                 x.NombreRequerimiento,
                                 x.DescripcionRequerimiento,
                                 x.URLRequerimiento,
                                 x.Estado

                             }).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<RequerimientoEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new RequerimientoEnt
                        {
                            IdRequerimiento = item.IdRequerimiento,
                            CodigoRequerimiento = item.CodigoRequerimiento,
                            NombreRequerimiento = item.NombreRequerimiento,
                            DescripcionRequerimiento = item.DescripcionRequerimiento,
                            URLRequerimiento = item.URLRequerimiento,
                            Estado = item.Estado
                            
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<RequerimientoEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultarRequerimientoPorId")]
        public RequerimientoEnt ConsultaRequerimiento(long q)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento
                             where x.IdRequerimiento == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    RequerimientoEnt resp = new RequerimientoEnt();
                    resp.IdRequerimiento = datos.IdRequerimiento;
                    resp.CodigoRequerimiento = datos.CodigoRequerimiento;
                    resp.NombreRequerimiento = datos.NombreRequerimiento;
                    resp.DescripcionRequerimiento = datos.DescripcionRequerimiento;
                    resp.URLRequerimiento = datos.URLRequerimiento;
                    resp.Estado = datos.Estado;

                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPut]
        [Route("api/InactivarRequerimiento")]
        public int InactivarRequerimiento(RequerimientoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento
                             where x.IdRequerimiento == entidad.IdRequerimiento
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    bool estadoActual = datos.Estado;

                    datos.Estado = (estadoActual == true ? false : true);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpPut]
        [Route("api/EditarRequerimiento")]
        public int EditarRequerimiento(RequerimientoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento
                             where x.IdRequerimiento == entidad.IdRequerimiento
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.CodigoRequerimiento = entidad.CodigoRequerimiento;
                    datos.NombreRequerimiento = entidad.NombreRequerimiento;
                    datos.DescripcionRequerimiento = entidad.DescripcionRequerimiento;
                    datos.URLRequerimiento = entidad.URLRequerimiento;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }
        [HttpPost]
        [Route("api/NuevoTask")]
        public int NuevoTask(Requerimiento_ProyectoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {

                Requerimiento_Proyecto tabla = new Requerimiento_Proyecto();
                tabla.IdProyecto = entidad.IdProyecto;
                tabla.IdRequerimiento = entidad.IdRequerimiento;
                tabla.EmpleadoAsignado = entidad.EmpleadoAsignado;
                tabla.FechaInicio = entidad.FechaInicio;
                tabla.FechaLimite = entidad.FechaLimite;
                tabla.Estado = false;

                bd.Requerimiento_Proyecto.Add(tabla);

                return bd.SaveChanges();
            }
        }

    }
}

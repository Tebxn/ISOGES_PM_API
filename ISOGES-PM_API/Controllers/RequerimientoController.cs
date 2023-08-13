
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
                tabla.Codigo = entidad.Codigo; 
                tabla.Nombre = entidad.Nombre;
                tabla.Descripcion = entidad.Descripcion;
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
                                 x.Codigo,
                                 x.Nombre,
                                 x.Descripcion,
                                 x.URL,
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
                            Codigo = item.Codigo,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            URL = item.URL,
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
                    resp.Codigo = datos.Codigo;
                    resp.Nombre = datos.Nombre;
                    resp.Descripcion = datos.Descripcion;
                    resp.URL = datos.URL;
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
                    datos.Codigo = entidad.Codigo;
                    datos.Nombre = entidad.Nombre;
                    datos.Descripcion = entidad.Descripcion;
                    datos.URL = entidad.URL;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }


    }
}

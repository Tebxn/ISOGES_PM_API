
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



namespace ISOGES_PM_API.Controllers
{
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
                                 x.Descripcion

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
                            Descripcion = item.Descripcion
                            
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

                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpDelete]
        [Route("api/EliminarRequerimiento")]
        public int InactivarRequerimiento(RequerimientoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Requerimiento
                             where x.IdRequerimiento == entidad.IdRequerimiento
                             select x).FirstOrDefault();

                if (datos != null)
                {


                    bd.Requerimiento.Remove(datos);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpPut]
        [Route("api/EditarRequerimiento")]
        public int CambiarClave(RequerimientoEnt entidad)
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
                    return bd.SaveChanges();
                }

                return 0;
            }
        }


    }
}

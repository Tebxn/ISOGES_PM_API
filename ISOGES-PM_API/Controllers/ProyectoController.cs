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
    public class ProyectoController : ApiController
    {

        [HttpGet]
        [Route("api/ConsultarProyectos")]
        public List<ProyectoEnt> ConsultarProyectos()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Proyecto
                             join y in bd.Cliente on x.Cliente equals y.IdCliente
                             select new
                             {
                                 x.IdProyecto,
                                 x.NombreProyecto,
                                 x.Descripcion,
                                 x.Estado,
                                 y.Nombre,
                                 x.MontoEstimado
                             }).ToList();

                if (datos.Count > 0)
                {
                    var lista = new List<ProyectoEnt>();
                    foreach (var item in datos)
                    {
                        lista.Add(new ProyectoEnt
                        {
                            IdProyecto = item.IdProyecto,
                            NombreProyecto = item.NombreProyecto,
                            Descripcion = item.Descripcion,
                            NombreCliente = item.Nombre,
                            Estado = item.Estado,
                            MontoEstimado = (double)item.MontoEstimado

                        });
                    }

                    return lista;
                }
                else
                {

                    return new List<ProyectoEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultarProyectoPorId")]
        public ProyectoEnt ConsultarProyecto(long q)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Proyecto
                             where x.IdProyecto == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    ProyectoEnt resp = new ProyectoEnt();
                    resp.IdProyecto = datos.IdProyecto;
                    resp.NombreProyecto = datos.NombreProyecto;
                    resp.Estado = datos.Estado;
                    resp.Descripcion = datos.Descripcion;
                    resp.Cliente = datos.Cliente;
                    resp.MontoEstimado = (double)datos.MontoEstimado;

                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }


        [HttpPost]
        [Route("api/CrearProyecto")]
        public int CrearProyecto(ProyectoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {

                Proyecto tabla = new Proyecto();
                tabla.NombreProyecto = entidad.NombreProyecto;
                tabla.Estado = true;
                tabla.Descripcion = entidad.Descripcion;
                tabla.Cliente = entidad.Cliente;
                tabla.MontoEstimado = entidad.MontoEstimado;

              

                bd.Proyecto.Add(tabla);

                return bd.SaveChanges();
            }
        }

        [HttpPut]
        [Route("api/EditarProyecto")]
        public int EditarProyecto(ProyectoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Proyecto
                             where x.IdProyecto == entidad.IdProyecto
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.NombreProyecto = entidad.NombreProyecto;
                    datos.Descripcion = entidad.Descripcion;
                    datos.Cliente = entidad.Cliente;
                    datos.MontoEstimado = entidad.MontoEstimado;
                    datos.Estado = true;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpPut]
        [Route("api/InactivarProyecto")]
        public int InactivarUsuario(ProyectoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Proyecto
                             where x.IdProyecto == entidad.IdProyecto
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
    }
}

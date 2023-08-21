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
                             join z in bd.Estado_Proyecto on x.EstadoGeneral equals z.IdEstadoProyecto
                             select new
                             {
                                 x.IdProyecto,
                                 x.NombreProyecto,
                                 x.DescripcionProyecto,
                                 x.Estado,
                                 y.NombreCliente,
                                 x.MontoEstimado,
                                 z.NombreEstado
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
                            DescripcionProyecto = item.DescripcionProyecto,
                            NombreCliente = item.NombreCliente,
                            Estado = item.Estado,
                            MontoEstimado = (double)item.MontoEstimado,
                            NombreEstado = item.NombreEstado

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
                    resp.DescripcionProyecto = datos.DescripcionProyecto;
                    resp.Cliente = datos.Cliente;
                    resp.MontoEstimado = (double)datos.MontoEstimado;
                    resp.EstadoGeneral = (long)datos.EstadoGeneral;


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
                tabla.DescripcionProyecto = entidad.DescripcionProyecto;
                tabla.Cliente = entidad.Cliente;
                tabla.MontoEstimado = entidad.MontoEstimado;
                tabla.EstadoGeneral = entidad.EstadoGeneral;

              

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
                    datos.DescripcionProyecto = entidad.DescripcionProyecto;
                    datos.Cliente = entidad.Cliente;
                    datos.MontoEstimado = entidad.MontoEstimado;
                    datos.Estado = true;
                    datos.EstadoGeneral = entidad.EstadoGeneral;
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
        [HttpGet]
        [Route("api/ConsultarEstadosProyecto")]
        public List<Estado_ProyectoEnt> ConsultarEstadosProyecto()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Estado_Proyecto
                             select x).ToList();


                if (datos.Count > 0)
                {
                    var resp = new List<Estado_ProyectoEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new Estado_ProyectoEnt
                        {
                            IdEstadoProyecto = item.IdEstadoProyecto,
                            NombreEstado = item.NombreEstado,
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<Estado_ProyectoEnt>();
                }
            }
        }
        [HttpGet]
        [Route("api/ConsultaRequerimientosPorProyecto")]
        public List<Requerimiento_ProyectoEnt> ConsultaRequerimientosPorProyecto(long q)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from r in bd.Requerimiento_Proyecto
                             join p in bd.Proyecto on r.IdProyecto equals p.IdProyecto
                             join x in bd.Requerimiento on r.IdRequerimiento equals x.IdRequerimiento
                             join e in bd.Usuario on r.EmpleadoAsignado equals e.IdUsuario
                             where r.IdProyecto == q
                             select new
                             {
                                 x.NombreRequerimiento,
                                 e.Nombre,
                                 r.FechaInicio,
                                 r.FechaLimite,
                                 r.Estado

                             }).ToList();

                if (datos.Count > 0)
                {
                    var lista = new List<Requerimiento_ProyectoEnt>();
                    foreach (var item in datos)
                    {
                        lista.Add(new Requerimiento_ProyectoEnt
                        {
                            NombreRequerimiento = item.NombreRequerimiento,
                            NombreEmpleadoAsignado = item.Nombre,
                            FechaInicio = item.FechaInicio,
                            FechaLimite = item.FechaLimite,
                            Estado = (bool)item.Estado
                        });
                    }

                    return lista;
                }
                else
                {

                    return new List<Requerimiento_ProyectoEnt>();
                }
            }
        }
        [HttpGet]
        [Route("api/ConsultaCobrosProyectoPorId")]
        public List<CobroEnt> ConsultaCobrosProyectoPorId(long q)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from r in bd.Cobro
                             join p in bd.Proyecto on r.IdProyecto equals p.IdProyecto
                             join x in bd.Estado_Cobro on r.IdEstadoCobro equals x.IdEstadoCobro
                             join e in bd.TipoCobro on r.TipoCobro equals e.IdTipoCobro
                             where r.IdProyecto == q
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

        [HttpPost]
        [Route("api/CrearRequerimientoPorProyecto")]
        public int CrearRequerimientoPorProyecto(Requerimiento_ProyectoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {


                Requerimiento_Proyecto tabla = new Requerimiento_Proyecto();
                tabla.IdProyecto = entidad.IdProyecto;
                tabla.IdRequerimiento = entidad.IdRequerimiento;
                tabla.EmpleadoAsignado = entidad.EmpleadoAsignado;
                tabla.Estado = entidad.Estado;
                tabla.FechaInicio = entidad.FechaInicio;
                tabla.FechaLimite = entidad.FechaLimite;

                bd.Requerimiento_Proyecto.Add(tabla);


                return bd.SaveChanges();
            }
        }


    }
}

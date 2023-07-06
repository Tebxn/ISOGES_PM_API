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
        public ProyectoResponse ConsultarProyectos()
        {
            ProyectoResponse APIresponse = new ProyectoResponse();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Proyecto
                                 select x).ToList();

                    if (datos.Count > 0)
                    {
                        var lista = new List<ProyectoEnt>();
                        foreach (var item in datos)
                        {
                            lista.Add(new ProyectoEnt
                            {
                                Nombre = item.Nombre,
                                Descripcion = item.Descripcion,
                                Cliente = item.Cliente,
                                Estado = item.Estado,
                                MontoEstimado = (double)item.MontoEstimado

                            });
                        }

                        APIresponse.ObjectList = lista;
                        return APIresponse;
                    }
                    else
                    {
                        APIresponse.ReturnMessage = "No existen datos por mostrar";
                        return APIresponse;
                    }
                }
            }
            catch (Exception ex)
            {
                APIresponse.ReturnMessage = ex.Message;
                return APIresponse;
            }
        }

        [HttpGet]
        [Route("api/ConsultarProyectoPorId")]
        public ProyectoResponse ConsultarCobroPorId(long id)
        {
            ProyectoResponse APIresponse = new ProyectoResponse();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Proyecto
                                 where x.IdProyecto == id
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        ProyectoEnt proyectoEncontrado = new ProyectoEnt
                        {
                            Nombre = datos.Nombre,
                            Descripcion = datos.Descripcion,
                            Cliente = datos.Cliente,
                            Estado = datos.Estado,
                            MontoEstimado = (double)datos.MontoEstimado
                        };


                        APIresponse.ObjectSingle = proyectoEncontrado;
                        return APIresponse;
                    }
                    else
                    {
                        APIresponse.ReturnMessage = "No se ha encontrado un proyecto con el id ingresado";
                        return APIresponse;
                    }
                }
            }
            catch (Exception ex)
            {
                APIresponse.ReturnMessage = ex.Message;
                return APIresponse;
            }
        }

        [HttpPost]
        [Route("api/CrearProyecto")]
        public ProyectoResponse CrearProyecto(ProyectoEnt entity)
        {
            ProyectoResponse APIresponse = new ProyectoResponse();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    Proyecto tabla = new Proyecto();
                    tabla.Nombre = entity.Nombre;
                    tabla.Descripcion = entity.Descripcion;
                    tabla.Cliente = entity.Cliente;
                    tabla.Estado = entity.Estado;
                    tabla.MontoEstimado = entity.MontoEstimado;
                    bd.Proyecto.Add(tabla);
                    bd.SaveChanges();

                    APIresponse.ReturnMessage = "Proyecto Creado";
                    return APIresponse;
                }
            }
            catch (Exception ex)
            {
                APIresponse.ReturnMessage = ex.Message;
                return APIresponse;
            }
        }
    }
}

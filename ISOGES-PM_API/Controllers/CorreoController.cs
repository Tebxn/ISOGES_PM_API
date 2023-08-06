using ISOGES_PM_API.Entities;
using ISOGES_PM_API.Models.Utilities;
using ISOGES_PM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ISOGES_PM_API.Controllers
{
    public class CorreoController : ApiController
    {
        [HttpPost]
        [Route("api/CrearCorreo")]
        public int CrearCorreo(CorreoEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {

                Correo tabla = new Correo();
                tabla.Asunto = entidad.Asunto;
                tabla.Estado = true;
                tabla.Cuerpo = entidad.Cuerpo;
                tabla.Remitente = entidad.Remitente;
                tabla.ClienteCorreo = entidad.ClienteCorreo;
                tabla.Fecha = entidad.Fecha;

                bd.Correo.Add(tabla);



                UtilFunctions utilFunctions = new UtilFunctions();


                if (tabla != null)
                {

                    utilFunctions.SendMail(entidad.NombreCorreo, entidad.Asunto, entidad.Cuerpo);

                    return bd.SaveChanges();
                }
                return 0;

            }
        }


        [HttpGet]
        [Route("api/ConsultarCorreos")]
        public List<CorreoEnt> ConsultarCorreos()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Correo
                             join y in bd.Cliente on x.ClienteCorreo equals y.IdCliente
                             select new
                             {
                                 x.IdCorreo,
                                 x.Asunto,
                                 x.Cuerpo,
                                 x.Estado,
                                 y.CorreoElectronico,
                                 x.Remitente,
                                 x.Fecha
                             }).ToList();

                if (datos.Count > 0)
                {
                    var lista = new List<CorreoEnt>();
                    foreach (var item in datos)
                    {
                        lista.Add(new CorreoEnt
                        {
                            IdCorreo = item.IdCorreo,
                            Asunto = item.Asunto,
                            Cuerpo = item.Cuerpo,
                            Fecha = item.Fecha,
                            NombreCorreo = item.CorreoElectronico,
                            Estado = item.Estado,
                            Remitente = item.Remitente
                            

                        });
                    }

                    return lista;
                }
                else
                {

                    return new List<CorreoEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultarCorreoPorId")]
        public CorreoEnt ConsultarCorreo(long q)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Correo
                             where x.IdCorreo == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    CorreoEnt resp = new CorreoEnt();
                    resp.IdCorreo = datos.IdCorreo;
                    resp.Asunto = datos.Asunto;
                    resp.Cuerpo = datos.Cuerpo;
                    resp.ClienteCorreo = datos.ClienteCorreo;
                    resp.Remitente = datos.Remitente;
                    resp.Fecha = datos.Fecha;

                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPut]
        [Route("api/EliminarCorreoPorId")]
        public CorreoResp EliminarCorreoPorId(long q)
        {
            CorreoResp APIresponse = new CorreoResp();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Correo
                                 where x.IdCorreo == q
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        CorreoEnt correoEncontrado = new CorreoEnt
                        {
                            Asunto = datos.Asunto,
                            Cuerpo = datos.Cuerpo,
                            ClienteCorreo = datos.ClienteCorreo,
                            Remitente = datos.Remitente,
                            Fecha = datos.Fecha
                        };

                        APIresponse.ObjectSingle = correoEncontrado;

                        bd.Correo.Remove(datos); 
                        bd.SaveChanges(); 

                        return APIresponse;
                    }
                    else
                    {
                        APIresponse.mensajeUsuario = "No se ha encontrado un correo con el id ingresado";
                        return APIresponse;
                    }
                }
            }
            catch (Exception ex)
            {
                APIresponse.mensajeUsuario = ex.Message;
                return APIresponse;
            }
        }

    }
}

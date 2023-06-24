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
    public class CobroController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultarCobros")]
        public List<CobroEnt> ConsultarCobros(CobroEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Cobro
                             where x.IdProyecto == entidad.IdProyecto
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<CobroEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new CobroEnt
                        {
                            TipoCobro = item.TipoCobro,
                            Fecha = item.Fecha,
                            Monto = item.Monto,
                            IdProyecto = item.IdProyecto
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

        [HttpGet]
        [Route("api/ConsultarCobroPorId")]
        public Respuesta ConsultarCobroPorId(long id)
        {
            Respuesta resultado = new Respuesta();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Cobro
                                 where x.IdProyecto == id
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        CobroEnt cobro = new CobroEnt
                        {
                            TipoCobro = datos.TipoCobro,
                            Fecha = datos.Fecha,
                            EstadoCobro = datos.EstadoCobro,
                            Monto = datos.Monto,
                            IdProyecto = datos.IdProyecto
                        };


                        resultado.CobroUnico = cobro;
                        return resultado;
                    }
                    else
                    {
                        resultado.Mensaje = "No se ha encontrado un cobro con el id ingresado";
                        return resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                return resultado;
            }
        }
    }
}




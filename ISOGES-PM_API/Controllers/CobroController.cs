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
        public CobroResponse ConsultarCobros()
        {
            CobroResponse APIresponse = new CobroResponse();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Cobro
                                 select x).ToList();

                    if (datos.Count > 0)
                    {
                        var lista = new List<CobroEnt>();
                        foreach (var item in datos)
                        {
                            lista.Add(new CobroEnt
                            {
                                TipoCobro = item.TipoCobro,
                                Fecha = item.Fecha,
                                Monto = item.Monto,
                                IdProyecto = item.IdProyecto
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
        [Route("api/ConsultarCobroPorId")]
        public CobroResponse ConsultarCobroPorId(long id)
        {
            CobroResponse APIresponse = new CobroResponse();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Cobro
                                 where x.IdProyecto == id
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        CobroEnt cobroEncontrado = new CobroEnt
                        {
                            TipoCobro = datos.TipoCobro,
                            Fecha = datos.Fecha,
                            EstadoCobro = datos.EstadoCobro,
                            Monto = datos.Monto,
                            IdProyecto = datos.IdProyecto
                        };


                        APIresponse.ObjectSingle = cobroEncontrado;
                        return APIresponse;
                    }
                    else
                    {
                        APIresponse.ReturnMessage = "No se ha encontrado un cobro con el id ingresado";
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

        //[HttpPut]
        //[Route("api/EditarCobro")]
        //public CobroResponse EditarCobro(CobroEnt entity)
        //{
        //    CobroResponse APIresponse = new CobroResponse();
        //    try
        //    {
        //        //code
        //    }
        //    catch (Exception ex)
        //    {
        //        APIresponse.ReturnMessage = ex.Message;
        //        return APIresponse;
        //    }
        //}
    }
}




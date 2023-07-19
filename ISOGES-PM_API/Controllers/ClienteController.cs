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
    public class ClienteController : ApiController
    {


        [HttpPost]
        [Route("api/CrearCliente")]
        public int CrearCliente(ClienteEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {


                Cliente tabla = new Cliente();
                tabla.Nombre = entidad.Nombre;
                tabla.Identificacion = entidad.Identificacion;
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Telefono = entidad.Telefono;
                bd.Cliente.Add(tabla);


                return bd.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/ConsultarClientes")]
        public List<ClienteEnt> ConsultarClientes()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Cliente
                             select x).ToList();
                             

                if (datos.Count > 0)
                {
                    var resp = new List<ClienteEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new ClienteEnt
                        {
                            IdCliente = item.IdCliente,
                            Nombre = item.Nombre,
                            Identificacion = item.Identificacion,
                            CorreoElectronico = item.CorreoElectronico,
                            Telefono = item.Telefono,
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<ClienteEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultarClientePorId")]
        public ClienteResp ConsultarClientePorId(long id)
        {
            ClienteResp APIresponse = new ClienteResp();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Cliente
                                 where x.IdCliente == id
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        ClienteEnt clienteEncontrado = new ClienteEnt
                        {
                            Nombre = datos.Nombre,
                            Identificacion = datos.Identificacion,
                            CorreoElectronico = datos.CorreoElectronico,
                            Telefono = datos.Telefono
                        };


                        APIresponse.ObjectSingle = clienteEncontrado;
                        return APIresponse;
                    }
                    else
                    {
                        APIresponse.mensajeUsuario = "No se ha encontrado un usuario con el id ingresado";
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

        [HttpPost]
        [Route("api/EliminarClientePorId")]
        public int EliminarClientePorId(long id)
        {
            ClienteResp APIresponse = new ClienteResp();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Cliente
                                 where x.IdCliente == id
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        ClienteEnt clienteEncontrado = new ClienteEnt
                        {
                            Nombre = datos.Nombre,
                            Identificacion = datos.Identificacion,
                            CorreoElectronico = datos.CorreoElectronico,
                            Telefono = datos.Telefono
                        };


                        APIresponse.ObjectSingle = clienteEncontrado;
                        bd.Cliente.Remove(datos);
                       return bd.SaveChanges();

                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                return 1;
            }
        }


    }
}

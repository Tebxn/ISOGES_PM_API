using ISOGES_PM_API.Entities;
using ISOGES_PM_API.Models;
using ISOGES_PM_API.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace ISOGES_PM_API.Controllers
{
    public class UsuariosController : ApiController
    {
        [HttpPost]
        [Route("api/IniciarSesion")]
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Usuario
                             join y in bd.TipoUsuario on x.TipoUsuario equals y.IdTipoUsuario
                             where x.CorreoElectronico == entidad.CorreoElectronico
                                      && x.Contrasena == entidad.Contrasena
                                      && x.Estado == true

                             select new {
                                x.IdUsuario,
                                x.Nombre,
                                x.Apellido1,
                                x.Apellido2,
                                x.Identificacion,
                                x.CorreoElectronico,
                                x.Telefono,
                                x.TipoUsuario,
                                y.NombreTipo,
                                x.Estado,
                                x.Puesto
                             }).FirstOrDefault();

                if (datos != null)
                {
                    UsuarioEnt resp = new UsuarioEnt();
                    resp.IdUsuario = datos.IdUsuario;
                    resp.Nombre = datos.Nombre;
                    resp.Apellido1 = datos.Apellido1;
                    resp.Apellido2 = datos.Apellido2;
                    resp.Identificacion = datos.Identificacion;
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Telefono = datos.Telefono;
                    resp.TipoUsuario = datos.TipoUsuario;
                    resp.NombreTipoUsuario = datos.NombreTipo;
                    resp.Estado = datos.Estado;
                    resp.Puesto = datos.Puesto;

                    return resp;
                }
                else
                {
                    return null;
                }
            } 
        }

        [HttpPost]
        [Route("api/CrearUsuario")]
        public int CrearUsuario(UsuarioEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                EmailHelper emailHelper = new EmailHelper();
                string tempPassword = emailHelper.CreatePassword();

                Usuario tabla = new Usuario();
                tabla.Nombre = entidad.Nombre;
                tabla.Apellido1 = entidad.Apellido1;
                tabla.Apellido2 = entidad.Apellido2;
                tabla.Identificacion = entidad.Identificacion;
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Telefono = entidad.Telefono;
                tabla.TipoUsuario = entidad.TipoUsuario;
                tabla.Estado = true;
                tabla.TipoUsuario = entidad.TipoUsuario;
                tabla.Contrasena = tempPassword;
                tabla.Puesto = entidad.Puesto;
                tabla.PassIsTemp = true;
                bd.Usuario.Add(tabla);

                emailHelper.SendEmail(entidad.CorreoElectronico, "Nueva Cuenta ISOGES-PM", "Bienvenido a Isoges Project Management Sr(a) " + entidad.Nombre +" "+ entidad.Apellido1 +" "+ entidad.Apellido2 + "." +
                     "\n\nSu correo electronico asignado es el: "+entidad.CorreoElectronico+
                     "\n\nSu Contraseña temporal es: "+tempPassword+
                     "\n\nPor favor dirigase a la página de ISOGES-PM ingrese a su cuenta y cambie la contraseña temporal por una secreta.");

                return bd.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/ConsultarUsuarios")]
        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Usuario
                             join y in bd.TipoUsuario on x.TipoUsuario equals y.IdTipoUsuario
                             select new {
                                 x.IdUsuario,
                                 x.Nombre,
                                 x.Apellido1,
                                 x.Apellido2,
                                 x.Identificacion,
                                 x.CorreoElectronico,
                                 x.Telefono,
                                 x.TipoUsuario,
                                 y.NombreTipo,
                                 x.Estado,
                                 x.Puesto
                             }).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<UsuarioEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new UsuarioEnt
                        {
                        IdUsuario = item.IdUsuario,
                        Nombre = item.Nombre,
                        Apellido1 = item.Apellido1,
                        Apellido2 = item.Apellido2,
                        Identificacion = item.Identificacion,
                        CorreoElectronico = item.CorreoElectronico,
                        Telefono = item.Telefono,
                        TipoUsuario = item.TipoUsuario,
                        NombreTipoUsuario = item.NombreTipo,
                        Estado = item.Estado,
                        Puesto = item.Puesto
                    });
                    }
                    return resp;
                }
                else
                {
                    return new List<UsuarioEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultarUsuarioPorId")]
        public UserResponse ConsultarUsuarioPorId(long id)
        {
            UserResponse APIresponse = new UserResponse();
            try
            {
                using (var bd = new ISOGES_PMEntities())
                {
                    var datos = (from x in bd.Usuario
                                 where x.IdUsuario == id
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        UsuarioEnt usuarioEncontrado = new UsuarioEnt
                        {
                            Nombre = datos.Nombre,
                            Apellido1 = datos.Apellido1,
                            Apellido2 = datos.Apellido2,
                            Identificacion = datos.Identificacion,
                            CorreoElectronico = datos.CorreoElectronico,
                            Telefono = datos.Telefono,
                            TipoUsuario = datos.TipoUsuario,
                            Estado = datos.Estado,
                            Contrasena = datos.Contrasena
                        };


                        APIresponse.ObjectSingle = usuarioEncontrado;
                        return APIresponse;
                    }
                    else
                    {
                        APIresponse.ReturnMessage = "No se ha encontrado un usuario con el id ingresado";
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

        [HttpPut]
        [Route("api/InactivarUsuario")]
        public int InactivarUsuario(UsuarioEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
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

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
    public class UsersController : ApiController
    {
        [HttpPost]
        [Route("api/IniciarSesion")]
        public UserEnt IniciarSesion(UserEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.CorreoElectronico == entidad.CorreoElectronico
                                      && x.Contrasena == entidad.Contrasena
                                      && x.Estado == true
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    UserEnt resp = new UserEnt();
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.TipoUsuario = (int)datos.TipoUsuario;
                    return resp;
                }
                else
                {
                    return null;
                }
            } 
        }

        [HttpPost]
        [Route("api/RegistrarUsuario")]
        public int RegistrarUsuario(UserEnt entidad)
        {
            using (var bd = new ISOGES_PMEntities())
            {
                Usuario tabla = new Usuario();
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Contrasena = entidad.Contrasena;
                tabla.Identificacion = entidad.Identificacion;
                tabla.Nombre = entidad.Nombre;
                tabla.Estado = entidad.Estado;
                tabla.TipoUsuario = entidad.TipoUsuario;

                bd.Usuario.Add(tabla);
                return bd.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/ConsultarUsuarios")]
        public List<UserEnt> ConsultarUsuarios()
        {
            using (var bd = new ISOGES_PMEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.Estado == true
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<UserEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new UserEnt
                        {
                            CorreoElectronico = item.CorreoElectronico,
                            Nombre = item.Nombre,
                            Identificacion = item.Identificacion,
                            Estado = item.Estado,
                            TipoUsuario = (int)item.TipoUsuario
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<UserEnt>();
                }
            }
        }

    }
}

using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class ClienteEnt
    {
        public long IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Identificacion { get; set; }
        public bool Estado { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
    }

    public class ClienteResp
    {
        public ClienteEnt ObjectSingle { get; set; } = new ClienteEnt();
        public List<ClienteEnt> ObjectList { get; set; } = new List<ClienteEnt>();

        public string mensajeUsuario { get; set; }

        public int booleano { get; set; }
    }
}
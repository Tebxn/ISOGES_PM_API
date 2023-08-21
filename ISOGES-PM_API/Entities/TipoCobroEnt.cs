using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class TipoCobroResponse : Response
    {
        public TipoCobroEnt ObjectSingle { get; set; } = new TipoCobroEnt();
        public List<TipoCobroEnt> ObjectList { get; set; } = new List<TipoCobroEnt>();
    }

    public class TipoCobroEnt
    {
        public int IdTipoCobro { get; set; }
        public string NombreTipoCobro { get; set; }
      
    }
}
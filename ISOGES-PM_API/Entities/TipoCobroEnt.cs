using ISOGES_PM_API.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISOGES_PM_API.Entities
{
    public class TipoCobroResponse : Response
    {
        public CobroEnt ObjectSingle { get; set; } = new CobroEnt();
        public List<CobroEnt> ObjectList { get; set; } = new List<CobroEnt>();
    }

    public class TipoCobroEnt
    {
        public int IdTipoCobro { get; set; }
        public string NombreTipoCobro { get; set; }
      
    }
}
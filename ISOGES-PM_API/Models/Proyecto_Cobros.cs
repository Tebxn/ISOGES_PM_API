//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISOGES_PM_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proyecto_Cobros
    {
        public long IdProyecto { get; set; }
        public long IdCobro { get; set; }
    
        public virtual Cobro Cobro { get; set; }
        public virtual Proyecto Proyecto { get; set; }
    }
}

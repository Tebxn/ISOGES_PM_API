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
    
    public partial class Requerimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Requerimiento()
        {
            this.Proyecto = new HashSet<Proyecto>();
        }
    
        public long IdRequerimiento { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaConclusion { get; set; }
        public long EmpleadoAsignado { get; set; }
        public bool Estado { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyecto { get; set; }
    }
}
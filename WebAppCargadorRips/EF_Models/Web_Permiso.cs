//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppCargadorRips.EF_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Web_Permiso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Web_Permiso()
        {
            this.Web_RolHasPermiso = new HashSet<Web_RolHasPermiso>();
            this.Web_RolHasPermiso1 = new HashSet<Web_RolHasPermiso>();
            this.Web_RolHasPermiso2 = new HashSet<Web_RolHasPermiso>();
        }
    
        public long permiso_id { get; set; }
        public string nombre { get; set; }
        public long FK_permiso_estado_rips { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    
        public virtual Estado_RIPS Estado_RIPS { get; set; }
        public virtual Estado_RIPS Estado_RIPS1 { get; set; }
        public virtual Estado_RIPS Estado_RIPS2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_RolHasPermiso> Web_RolHasPermiso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_RolHasPermiso> Web_RolHasPermiso1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_RolHasPermiso> Web_RolHasPermiso2 { get; set; }
    }
}

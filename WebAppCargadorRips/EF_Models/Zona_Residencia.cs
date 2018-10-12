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
    
    public partial class Zona_Residencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zona_Residencia()
        {
            this.SDS_Consulta = new HashSet<SDS_Consulta>();
            this.SDS_Hospitalizacion = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Medicamento = new HashSet<SDS_Medicamento>();
            this.SDS_Otro_Servicio = new HashSet<SDS_Otro_Servicio>();
            this.SDS_Procedimiento = new HashSet<SDS_Procedimiento>();
            this.SDS_Recien_Nacido = new HashSet<SDS_Recien_Nacido>();
            this.SDS_Urgencia = new HashSet<SDS_Urgencia>();
        }
    
        public long zona_residencia_id { get; set; }
        public byte numero { get; set; }
        public string acronimo { get; set; }
        public string nombre { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public long estado_rips_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Consulta> SDS_Consulta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Medicamento> SDS_Medicamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Otro_Servicio> SDS_Otro_Servicio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Procedimiento> SDS_Procedimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Recien_Nacido> SDS_Recien_Nacido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Urgencia> SDS_Urgencia { get; set; }
        public virtual Estado_RIPS Estado_RIPS { get; set; }
    }
}

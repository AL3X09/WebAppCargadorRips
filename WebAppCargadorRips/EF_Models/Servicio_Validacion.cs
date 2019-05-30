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
    
    public partial class Servicio_Validacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servicio_Validacion()
        {
            this.Servicio_Radicacion = new HashSet<Servicio_Radicacion>();
            this.Servicio_Validacion_Error = new HashSet<Servicio_Validacion_Error>();
        }
    
        public long servicio_validacion_id { get; set; }
        public string usuario_validacion { get; set; }
        public Nullable<System.DateTime> fecha_validacion { get; set; }
        public Nullable<System.DateTime> periodo_inicio { get; set; }
        public Nullable<System.DateTime> periodo_fin { get; set; }
        public Nullable<long> categoria_id { get; set; }
        public string entidad_reporta { get; set; }
        public string prestador { get; set; }
        public Nullable<long> administradora_id { get; set; }
        public Nullable<long> tipo_usuario_id { get; set; }
        public int us { get; set; }
        public int af { get; set; }
        public int ac { get; set; }
        public int ap { get; set; }
        public int au { get; set; }
        public int ah { get; set; }
        public int an { get; set; }
        public int am { get; set; }
        public int at { get; set; }
        public Nullable<long> prestador_id { get; set; }
        public Nullable<long> estado_rips_id { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio_Radicacion> Servicio_Radicacion { get; set; }
        public virtual Administradora Administradora { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Estado_RIPS Estado_RIPS { get; set; }
        public virtual Prestador Prestador1 { get; set; }
        public virtual Tipo_Usuario Tipo_Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio_Validacion_Error> Servicio_Validacion_Error { get; set; }
    }
}

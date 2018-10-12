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
    
    public partial class Prestador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prestador()
        {
            this.MSYPS_Consulta = new HashSet<MSYPS_Consulta>();
            this.SDS_Consulta = new HashSet<SDS_Consulta>();
            this.SDS_Factura = new HashSet<SDS_Factura>();
            this.SDS_Hospitalizacion = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Medicamento = new HashSet<SDS_Medicamento>();
            this.SDS_Otro_Servicio = new HashSet<SDS_Otro_Servicio>();
            this.SDS_Procedimiento = new HashSet<SDS_Procedimiento>();
            this.SDS_Recien_Nacido = new HashSet<SDS_Recien_Nacido>();
            this.SDS_Urgencia = new HashSet<SDS_Urgencia>();
            this.Web_Usuario = new HashSet<Web_Usuario>();
            this.Web_Usuario1 = new HashSet<Web_Usuario>();
            this.Web_Usuario2 = new HashSet<Web_Usuario>();
        }
    
        public long prestador_id { get; set; }
        public string codigo { get; set; }
        public bool habilitado { get; set; }
        public string codigo_habilitacion { get; set; }
        public string codigo_sede { get; set; }
        public string nombre { get; set; }
        public string nombre_sede { get; set; }
        public bool sede_principal { get; set; }
        public string numero_sede_principal { get; set; }
        public string clase_prestador { get; set; }
        public string clase_persona { get; set; }
        public string naturaleza_juridica { get; set; }
        public string caracter { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public string zona { get; set; }
        public string poblado_codigo { get; set; }
        public string poblado_nombre { get; set; }
        public bool ese { get; set; }
        public Nullable<byte> nivel { get; set; }
        public Nullable<bool> red_adscrita_bogota { get; set; }
        public string nit_prestador { get; set; }
        public Nullable<byte> digito_verificacion { get; set; }
        public string gerente { get; set; }
        public string direccion { get; set; }
        public string barrio { get; set; }
        public string telefono { get; set; }
        public string fax { get; set; }
        public string email_pre { get; set; }
        public string fecha_apertura { get; set; }
        public string fecha_cierre { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public Nullable<long> localidad_id { get; set; }
        public Nullable<long> upz_id { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    
        public virtual Localidad Localidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MSYPS_Consulta> MSYPS_Consulta { get; set; }
        public virtual UPZ UPZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Consulta> SDS_Consulta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Factura> SDS_Factura { get; set; }
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Usuario> Web_Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Usuario> Web_Usuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Web_Usuario> Web_Usuario2 { get; set; }
    }
}

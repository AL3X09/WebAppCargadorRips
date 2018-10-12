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
    
    public partial class CIE10
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CIE10()
        {
            this.MSYPS_Consulta = new HashSet<MSYPS_Consulta>();
            this.MSYPS_Consulta1 = new HashSet<MSYPS_Consulta>();
            this.MSYPS_Consulta2 = new HashSet<MSYPS_Consulta>();
            this.MSYPS_Consulta3 = new HashSet<MSYPS_Consulta>();
            this.SDS_Consulta = new HashSet<SDS_Consulta>();
            this.SDS_Consulta1 = new HashSet<SDS_Consulta>();
            this.SDS_Consulta2 = new HashSet<SDS_Consulta>();
            this.SDS_Consulta3 = new HashSet<SDS_Consulta>();
            this.SDS_Hospitalizacion = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Hospitalizacion1 = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Hospitalizacion2 = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Hospitalizacion3 = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Hospitalizacion4 = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Hospitalizacion5 = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Hospitalizacion6 = new HashSet<SDS_Hospitalizacion>();
            this.SDS_Procedimiento = new HashSet<SDS_Procedimiento>();
            this.SDS_Procedimiento1 = new HashSet<SDS_Procedimiento>();
            this.SDS_Procedimiento2 = new HashSet<SDS_Procedimiento>();
            this.SDS_Recien_Nacido = new HashSet<SDS_Recien_Nacido>();
            this.SDS_Recien_Nacido1 = new HashSet<SDS_Recien_Nacido>();
            this.SDS_Urgencia = new HashSet<SDS_Urgencia>();
            this.SDS_Urgencia1 = new HashSet<SDS_Urgencia>();
            this.SDS_Urgencia2 = new HashSet<SDS_Urgencia>();
            this.SDS_Urgencia3 = new HashSet<SDS_Urgencia>();
            this.SDS_Urgencia4 = new HashSet<SDS_Urgencia>();
        }
    
        public long cie10_id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public long sexo_id { get; set; }
        public short edad_minima_años { get; set; }
        public short edad_maxima_años { get; set; }
        public int edad_minima_dias { get; set; }
        public int edad_maxima_dias { get; set; }
        public bool mortalidad { get; set; }
        public bool morbilidad { get; set; }
        public string categoria { get; set; }
        public string clase { get; set; }
        public byte numero_capitulo { get; set; }
        public string capitulo { get; set; }
        public string agrupacion1 { get; set; }
        public string codigo_agrupacion2 { get; set; }
        public string agrupacion2 { get; set; }
        public string causas { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public bool cie10_2002 { get; set; }
        public bool cie10_2005 { get; set; }
        public bool cie10_2009 { get; set; }
        public bool cie10_2012 { get; set; }
        public bool cie10_2014 { get; set; }
    
        public virtual Sexo Sexo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MSYPS_Consulta> MSYPS_Consulta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MSYPS_Consulta> MSYPS_Consulta1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MSYPS_Consulta> MSYPS_Consulta2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MSYPS_Consulta> MSYPS_Consulta3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Consulta> SDS_Consulta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Consulta> SDS_Consulta1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Consulta> SDS_Consulta2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Consulta> SDS_Consulta3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Hospitalizacion> SDS_Hospitalizacion6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Procedimiento> SDS_Procedimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Procedimiento> SDS_Procedimiento1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Procedimiento> SDS_Procedimiento2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Recien_Nacido> SDS_Recien_Nacido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Recien_Nacido> SDS_Recien_Nacido1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Urgencia> SDS_Urgencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Urgencia> SDS_Urgencia1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Urgencia> SDS_Urgencia2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Urgencia> SDS_Urgencia3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDS_Urgencia> SDS_Urgencia4 { get; set; }
    }
}

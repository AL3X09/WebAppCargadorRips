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
    
    public partial class VW_Listado_Estado_Rips
    {
        public long validacion_id { get; set; }
        public long codigo { get; set; }
        public string tipo_usuario { get; set; }
        public string categoria { get; set; }
        public Nullable<System.DateTime> periodo_fecha_inicio { get; set; }
        public Nullable<System.DateTime> periodo_fecha_fin { get; set; }
        public Nullable<System.DateTime> fecha_cargo { get; set; }
        public long FK_usuario { get; set; }
        public long FK_estado_web_validacion { get; set; }
        public string estado_web_validacion { get; set; }
        public string desc_estado_web_validacion { get; set; }
        public long FK_estado_web_preradicacion { get; set; }
        public string estado_web_preradicacion { get; set; }
        public string desc_estado_web_preradicacion { get; set; }
        public Nullable<long> FK_estado_servicio_validacion { get; set; }
        public string estado_servicio_validacion { get; set; }
        public string desc_estado_servicio_validacion { get; set; }
        public Nullable<long> FK_estado_radicacion { get; set; }
        public string estado_radicacion { get; set; }
        public string desc_estado_radicacion { get; set; }
    }
}

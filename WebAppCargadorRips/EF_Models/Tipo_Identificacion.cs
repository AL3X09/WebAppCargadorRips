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
    
    public partial class Tipo_Identificacion
    {
        public long tipo_identificacion_id { get; set; }
        public byte numero { get; set; }
        public string acronimo { get; set; }
        public string nombre { get; set; }
        public byte longitud_minima { get; set; }
        public byte longitud_maxima { get; set; }
        public short edad_minima_años { get; set; }
        public short edad_maxima_años { get; set; }
        public int edad_minima_dias { get; set; }
        public int edad_maxima_dias { get; set; }
        public bool idUsuarios { get; set; }
        public bool idPrestadores { get; set; }
        public bool idExtranjeros { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public long estado_rips_id { get; set; }
    
        public virtual Estado_RIPS Estado_RIPS { get; set; }
    }
}

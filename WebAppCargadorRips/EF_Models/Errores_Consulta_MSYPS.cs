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
    
    public partial class Errores_Consulta_MSYPS
    {
        public string numero_factura { get; set; }
        public string prestador { get; set; }
        public string tipo_identificacion { get; set; }
        public string numero_identificacion { get; set; }
        public Nullable<System.DateTime> fecha_consulta { get; set; }
        public string codigo_procedimiento { get; set; }
        public Nullable<byte> finalidad_consulta { get; set; }
        public Nullable<byte> causa_externa { get; set; }
        public string diagnostico_principal { get; set; }
        public string diagnostico_relacionado_1 { get; set; }
        public string diagnostico_relacionado_2 { get; set; }
        public string diagnostico_relacionado_3 { get; set; }
        public Nullable<byte> tipo_diagnostico { get; set; }
        public Nullable<decimal> valor_consulta { get; set; }
        public Nullable<decimal> valor_cuota { get; set; }
        public Nullable<decimal> valor_neto { get; set; }
        public string sexo { get; set; }
        public Nullable<byte> tipo_usuario { get; set; }
        public byte edad { get; set; }
        public Nullable<byte> med_edad { get; set; }
        public Nullable<byte> departamento { get; set; }
        public Nullable<short> municipio { get; set; }
        public string administradora { get; set; }
        public string radicado { get; set; }
        public Nullable<System.DateTime> fecha_carga { get; set; }
        public string error { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
    }
}

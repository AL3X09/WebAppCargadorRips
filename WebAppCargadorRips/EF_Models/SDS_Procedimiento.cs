
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
    
public partial class SDS_Procedimiento
{

    public long procedimiento_id { get; set; }

    public System.Guid rowguid { get; set; }

    public long factura_id { get; set; }

    public long prestador_id { get; set; }

    public long usuario_id { get; set; }

    public System.DateTime fecha_procedimiento { get; set; }

    public string numero_autorizacion { get; set; }

    public long codigo_procedimiento_id { get; set; }

    public long ambito_procedimiento_id { get; set; }

    public long finalidad_id { get; set; }

    public Nullable<long> personal_atiende_id { get; set; }

    public Nullable<long> diagnostico_principal_id { get; set; }

    public Nullable<long> diagnostico_relacionado_id { get; set; }

    public Nullable<long> diagnostico_complicacion_id { get; set; }

    public Nullable<long> acto_quirurgico_id { get; set; }

    public decimal valor_procedimiento { get; set; }

    public long administradora_id { get; set; }

    public long tipo_usuario_id { get; set; }

    public byte edad { get; set; }

    public Nullable<long> med_edad_id { get; set; }

    public Nullable<long> divipola_id { get; set; }

    public Nullable<long> zona_residencia_id { get; set; }

    public long radicacion_id { get; set; }

    public Nullable<long> tipo_usuario_nuevo_id { get; set; }

    public System.DateTime fecha_modificacion { get; set; }



    public virtual Radicacion Radicacion { get; set; }

    public virtual Acto_Quirurgico Acto_Quirurgico { get; set; }

    public virtual Ambito_Procedimiento Ambito_Procedimiento { get; set; }

    public virtual Finalidad_Procedimiento Finalidad_Procedimiento { get; set; }

    public virtual Medida_Edad Medida_Edad { get; set; }

    public virtual Personal_Atiende Personal_Atiende { get; set; }

    public virtual Tipo_Usuario Tipo_Usuario { get; set; }

    public virtual Tipo_Usuario Tipo_Usuario1 { get; set; }

    public virtual Zona_Residencia Zona_Residencia { get; set; }

    public virtual Administradora Administradora { get; set; }

    public virtual CIE10 CIE10 { get; set; }

    public virtual CIE10 CIE101 { get; set; }

    public virtual CIE10 CIE102 { get; set; }

    public virtual CUPS CUPS { get; set; }

    public virtual DIVIPOLA DIVIPOLA { get; set; }

    public virtual Prestador Prestador { get; set; }

    public virtual SDS_Factura SDS_Factura { get; set; }

    public virtual SDS_Usuario SDS_Usuario { get; set; }

}

}

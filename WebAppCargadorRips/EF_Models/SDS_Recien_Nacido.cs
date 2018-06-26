
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
    
public partial class SDS_Recien_Nacido
{

    public long recien_nacido_id { get; set; }

    public System.Guid rowguid { get; set; }

    public long factura_id { get; set; }

    public long prestador_id { get; set; }

    public long usuario_id { get; set; }

    public System.DateTime fecha_nacimiento { get; set; }

    public byte edad_gestacional { get; set; }

    public long control_prenatal_id { get; set; }

    public long sexo_recien_nacido_id { get; set; }

    public short peso { get; set; }

    public long diagnostico_recien_nacido_id { get; set; }

    public Nullable<long> diagnostico_muerte_id { get; set; }

    public Nullable<System.DateTime> fecha_muerte { get; set; }

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

    public virtual Control_Prenatal Control_Prenatal { get; set; }

    public virtual Medida_Edad Medida_Edad { get; set; }

    public virtual Sexo Sexo { get; set; }

    public virtual Tipo_Usuario Tipo_Usuario { get; set; }

    public virtual Tipo_Usuario Tipo_Usuario1 { get; set; }

    public virtual Zona_Residencia Zona_Residencia { get; set; }

    public virtual Administradora Administradora { get; set; }

    public virtual CIE10 CIE10 { get; set; }

    public virtual CIE10 CIE101 { get; set; }

    public virtual DIVIPOLA DIVIPOLA { get; set; }

    public virtual Prestador Prestador { get; set; }

    public virtual SDS_Factura SDS_Factura { get; set; }

    public virtual SDS_Usuario SDS_Usuario { get; set; }

}

}

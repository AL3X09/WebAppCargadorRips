
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
    
public partial class Tipo_Diagnostico
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Tipo_Diagnostico()
    {

        this.MSYPS_Consulta = new HashSet<MSYPS_Consulta>();

        this.SDS_Consulta = new HashSet<SDS_Consulta>();

    }


    public long tipo_diagnostico_id { get; set; }

    public byte numero { get; set; }

    public string nombre { get; set; }

    public System.DateTime fecha_modificacion { get; set; }

    public long estado_rips_id { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<MSYPS_Consulta> MSYPS_Consulta { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<SDS_Consulta> SDS_Consulta { get; set; }

    public virtual Estado_RIPS Estado_RIPS { get; set; }

}

}

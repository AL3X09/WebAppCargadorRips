
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
    
public partial class Web_Validacion
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Web_Validacion()
    {

        this.Web_Preradicacion = new HashSet<Web_Preradicacion>();

    }


    public long validacion_id { get; set; }

    public long FK_web_validacion_tipo_usuario { get; set; }

    public long FK_web_validacion_categoria { get; set; }

    public System.DateTime periodofechainicio { get; set; }

    public System.DateTime periodofechafin { get; set; }

    public long FK_web_validacion_web_usuario { get; set; }

    public long FK_web_validacion_estado_rips { get; set; }

    public System.DateTime fecha_modificacion { get; set; }



    public virtual Categoria Categoria { get; set; }

    public virtual Estado_RIPS Estado_RIPS { get; set; }

    public virtual Tipo_Usuario Tipo_Usuario { get; set; }

    public virtual Web_Usuario Web_Usuario { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Web_Preradicacion> Web_Preradicacion { get; set; }

}

}
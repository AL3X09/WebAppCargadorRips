
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
    
public partial class UPZ
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public UPZ()
    {

        this.Prestador = new HashSet<Prestador>();

    }


    public long upz_id { get; set; }

    public long localidad_id { get; set; }

    public short numero { get; set; }

    public string nombre { get; set; }

    public System.DateTime fecha_modificacion { get; set; }



    public virtual Localidad Localidad { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Prestador> Prestador { get; set; }

}

}
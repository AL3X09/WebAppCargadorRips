
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
    
public partial class Web_Administrador
{

    public long administrador_id { get; set; }

    public string usuario { get; set; }

    public byte[] contrasenia { get; set; }

    public string nombres { get; set; }

    public string apellidos { get; set; }

    public string descripcion { get; set; }

    public string correo { get; set; }

    public int extencion { get; set; }

    public string imagen { get; set; }

    public long FK_web_administrador_rol { get; set; }

    public long FK_web_administrador_estado_rips { get; set; }

    public System.DateTime fecha_modificacion { get; set; }



    public virtual Web_Rol Web_Rol { get; set; }

    public virtual Estado_RIPS Estado_RIPS { get; set; }

}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppCargadorRips.Models
{
    public class EnviarCorreoModel
    {
        public int codPlantilla { get; set; }
        public string usercorreo { get; set; }
        public string usernombre { get; set; }
        public string codigocarga { get; set; }
        
    }

    public class EnviarCorreoModelErrors
    {
        public int codPlantilla { get; set; }
        public string usercorreo { get; set; }
        public string usernombre { get; set; }
        public List<string> errores { get; set; }
    }

    //eliminar si no se usa
    public class EnviarCorreoRecuperacionModel
    {
        public int codPlantilla { get; set; }
        public string usercorreo { get; set; }
        public string id { get; set; }//este es codigoprestador cifrado
        public string token { get; set; } //esta es la nueva contraseña cifrada

    }

    
    public class EnviarCorreoLogsModel
    {
        //public int codPlantilla { get; set; }
        public string usercorreo { get; set; }
       
    }
}
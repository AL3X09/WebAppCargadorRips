using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppCargadorRips.Models
{
    public class MsgClass
    {
        public int codigo { get; set; }
        public string mensaje { get; set; }
        public string tipo { get; set; }

    }
    public class RootObject
    {
        public MsgClass[] msg { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips.EF_Models;

namespace WebAppCargadorRips.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Presentaciones")]
    public class PresentacionesAyudaController : ApiController
    {

        private RipsEntitieConnection bd = new RipsEntitieConnection();

        //constructor
        PresentacionesAyudaController()
        {

        }

        //RECORDAR QUE SE DEBE HABILITAR CORS + INFO IR AL LINK
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api

        ///<summary>
        /// Lista de las presentaciones de ayuda existentes en el aplicativo
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = bd.SP_GetAllPresentacionesAyuda();
            return result;
        }

    }
}

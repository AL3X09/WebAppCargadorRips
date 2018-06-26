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
    [RoutePrefix("api/Manuales")]
    public class ManualesController : ApiController
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();

        //constructor
        ManualesController()
        {

        }

        ///<summary>
        /// Lista de los links manuales de la base de datos
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = bd.SP_GetAllManuales();
            return result;
        }
    }
}

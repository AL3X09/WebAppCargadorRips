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
    [RoutePrefix("api/Normatividad")]
    public class NormatividadController : ApiController
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();

        //constructor
        NormatividadController()
        {

        }

        ///<summary>
        /// Lista de la y normatividad de la base de datos
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = bd.SP_GetAllNormatividad();
            return result;
        }
    }
}

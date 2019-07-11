using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAppCargadorRips.EF_Models;

namespace WebAppCargadorRips.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Redascrita")]
    public class AyudaRedAdscritaController : ApiController
    {
        private RipsEntitieConnection db = new RipsEntitieConnection();

        [Route("Listar")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: api/RedAdscrita
        public IEnumerable<Object> Get()
        {
            var result = db.Web_Documento.Where(r => r.FK_web_documento_estado_rips.Equals(1));
            return result;
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Web_DocumentoExists(long id)
        {
            return db.Web_Documento.Count(e => e.documento_id == id) > 0;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips.EF_Models;

namespace WebAppCargadorRips.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Categorias")]
    public class CategoriasController : ApiController
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();

        //Constructor
        CategoriasController()
        {

        }

        //RECORDAR QUE SE DEBE HABILITAR CORS + INFO IR AL LINK
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api

        /// <summary>
        /// Se listan todas las categorias existentes en la base de datos.
        /// </summary>
        /// <returns>Valores nulos</returns>
        // GET api/TipoUsuario/ListarTipos
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            return null;
        }

        /// <summary>
        /// Se listan todas las categorias por id, existentes en la base de datos.
        /// </summary>
        /// <returns>Todos los valores disponibles en el universo corresponidentes al id enviado</returns>
        // GET api/Categoria/Listar/1
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Object Get(string id)
        {
            //se elimino el procedimeinto almmacenado y se deja la consulta con lambada

            var result = bd.Categoria.SqlQuery("SELECT * FROM Parametros.Categoria WHERE " + id + " = 1 AND estado_rips_id = 1 ORDER BY Parametros.nombre ASC").Select("new (categoria_id, nombre)");
            return result;

        }
    }
}

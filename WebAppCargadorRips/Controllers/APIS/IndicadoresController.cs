using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips.EF_Models;

namespace WebAppCargadorRips.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Indicadores")]
    public class IndicadoresController : ApiController
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();
        private DateTime fechaActual = new DateTime();
        //constructor
        IndicadoresController()
        {

        }

        ///<summary>
        /// Lista cantidad de rips cargados en la plataforma WEB y sus estados, los ultimos 5 años
        ///</summary>
        [HttpGet]
        [Route("ListarCantidadaniosCargadosaWebValidacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetWebValidacion(int iduser)
        {
            var result = from WV in bd.Web_Validacion
                         join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion
                         join ER in bd.Estado_RIPS on WV.FK_web_validacion_estado_rips equals ER.estado_rips_id
                         where WV.FK_web_validacion_web_usuario == iduser 
                         && WV.FK_web_validacion_estado_rips != 2
                         && WPR.FK_preradicacion_estado_rips != 2
                         && WPR.FK_preradicacion_estado_rips != 6
                         orderby WV.fecha_modificacion.Year descending
                         group WV by WV.fecha_modificacion.Year into d
                         select new
                         {
                             Anio = d.Key,
                             Cantidad = d.Count()
                         };

            return result.Take(5);
        }

        ///<summary>
        /// Lista cantidad los estados de los rips enviados por la plataforma WEB y sus estados por años
        ///</summary>
        [HttpGet]
        [Route("ListarEstadosXAnios")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstadosXAnios(int iduser)
        {
            var result = (from ER in bd.Estado_RIPS
                          join WV in bd.Web_Validacion on ER.estado_rips_id equals WV.FK_web_validacion_estado_rips
                          join WU in bd.Web_Usuario on WV.FK_web_validacion_web_usuario equals WU.usuario_id
                          join P in bd.Prestador on WU.FK_usuario_prestador equals P.prestador_id
                          join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion into WPR1
                          from WPR in WPR1.DefaultIfEmpty()
                          join SV in bd.Servicio_Validacion on P.codigo equals SV.prestador into SVR
                          from SV in SVR.DefaultIfEmpty()
                          join R in bd.Radicacion on SV.servicio_validacion_id equals R.servicio_validacion_id into R1
                          from R in R1.DefaultIfEmpty()
                          where WV.FK_web_validacion_web_usuario == iduser
                          && WV.FK_web_validacion_estado_rips != 5
                          && WV.FK_web_validacion_estado_rips != 2
                          //&& WV.fecha_modificacion.Year == fechaActual.Year
                          group ER by new { WV.fecha_modificacion.Year, ER.nombre, ER.estado_rips_id } into d
                          orderby d.Key.Year descending
                          select new
                          {
                              Anio = d.Key.Year,
                              Fk_estado = d.Key.estado_rips_id,
                              Estado = d.Key.nombre,
                              Cantidad = d.Count(),
                          });

            return result;

        }

        ///<summary>
        /// Lista cantidad los estados de los rips enviados por la plataforma WEB y sus estados por años y meses
        ///</summary>
        [HttpGet]
        [Route("ListarEstadosXAniosXmes")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstadosXAniosXEstados(int iduser)
        {
            var result = (from ER in bd.Estado_RIPS
                          join WV in bd.Web_Validacion on ER.estado_rips_id equals WV.FK_web_validacion_estado_rips
                          join WU in bd.Web_Usuario on WV.FK_web_validacion_web_usuario equals WU.usuario_id
                          join P in bd.Prestador on WU.FK_usuario_prestador equals P.prestador_id
                          join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion into WPR1
                          from WPR in WPR1.DefaultIfEmpty()
                          join SV in bd.Servicio_Validacion on P.codigo equals SV.prestador into SVR
                          from SV in SVR.DefaultIfEmpty()
                          join R in bd.Radicacion on SV.servicio_validacion_id equals R.servicio_validacion_id into R1
                          from R in R1.DefaultIfEmpty()
                          where WV.FK_web_validacion_web_usuario == iduser
                          && WV.FK_web_validacion_estado_rips != 5
                          && WV.FK_web_validacion_estado_rips != 2
                          //&& WV.fecha_modificacion.Year == fechaActual.Year
                          group ER by new { WV.fecha_modificacion.Year, WV.fecha_modificacion.Month, ER.nombre, ER.estado_rips_id } into d
                          orderby d.Key.Year descending
                          select new
                          {
                              Anio = d.Key.Year,
                              Mes = d.Key.Month,
                              Estado = d.Key.nombre,
                              Cantidad = d.Count(),
                          });

            return result;

        }

        ///<summary>
        /// Lista cantidad de rips cargados en WEB Validacion y sus estados todo el tiempo
        ///</summary>
        [HttpGet]
        [Route("ListarCantidadaniosCargadosaWebPreradicacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetWebPreradicacion(int iduser)
        {
            var result = from WPR in bd.Web_Preradicacion
                         join WV in bd.Web_Validacion on WPR.FK_web_preradicacion_web_validacion equals WV.validacion_id
                         join ER in bd.Estado_RIPS on WPR.FK_preradicacion_estado_rips equals ER.estado_rips_id
                         where WV.FK_web_validacion_web_usuario == iduser && WPR.FK_preradicacion_estado_rips != 2
                         group WPR by WPR.fecha_modificacion.Year into d
                         select new
                         {
                             Anio = d.Key,
                             Cantidad = d.Count(),
                         };

            return result;
        }

        ///<summary>
        /// Lista cantidad de rips cargados en WEB Preradicacion y sus estados año actual
        ///</summary>
        [HttpGet]
        [Route("ListarXEstadosWebRadicacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetanioactualWebPreradicacion(int iduser)
        {
            var result = from WPR in bd.Web_Preradicacion
                         join WV in bd.Web_Validacion on WPR.FK_web_preradicacion_web_validacion equals WV.validacion_id
                         join ER in bd.Estado_RIPS on WV.FK_web_validacion_estado_rips equals ER.estado_rips_id
                         where WV.FK_web_validacion_web_usuario == iduser && WPR.FK_preradicacion_estado_rips != 2
                         orderby ER.nombre ascending
                         group WPR by ER.nombre into d
                         select new
                         {
                             Estado = d.Key,
                             Cantidad = d.Count(),
                         };

            return result;
        }
    }
}

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
        /// Lista cantidad de rips cargados en la plataforma WEB y sus estados, 5 años mas actuales en tabla web validacion
        ///</summary>
        [HttpGet]
        [Route("ListarCantidadaniosCargadosaWebValidacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetCantidadCargadorWeb(int iduser)
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
        /// Lista cantidad de rips validados por el servicio Integral y sus estados, 5 años mas actuales en tabla servicio validación
        ///</summary>
        [HttpGet]
        [Route("ListarCantidadaniosRadicados")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetCantidadServicioWeb(int iduser)
        {
            var result = from R in bd.Radicacion
                         join SV in bd.Servicio_Validacion on R.servicio_validacion_id equals SV.servicio_validacion_id
                         join P in bd.Prestador on SV.prestador equals P.codigo_habilitacion
                         join WU in bd.Web_Usuario on P.prestador_id equals WU.FK_usuario_prestador
                         where WU.usuario_id == 1
                         && R.radicacion_estado_id != 2
                         && R.radicacion_estado_id != 12
                         && R.radicacion_estado_id != 13
                         orderby R.fecha_modificacion.Year descending
                         group R by R.fecha_modificacion.Year into d
                         select new
                         {
                             Anio = d.Key,
                             Cantidad = d.Count()
                         };

            return result.Take(5);
        }

        ///<summary>
        /// Lista cantidad los estados de los rips enviados por la plataforma WEB, agrupados por años solo en web validación
        ///</summary>
        [HttpGet]
        [Route("ListarEstadosXAniosWebValidacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstadosXAniosWebValidacion(int iduser)
        {
            var result = (from ER in bd.Estado_RIPS
                          join WV in bd.Web_Validacion on ER.estado_rips_id equals WV.FK_web_validacion_estado_rips
                          join WU in bd.Web_Usuario on WV.FK_web_validacion_web_usuario equals WU.usuario_id
                          where WV.FK_web_validacion_web_usuario == 1
                          && WV.FK_web_validacion_estado_rips != 5
                          && WV.FK_web_validacion_estado_rips != 2
                          group ER by new { WV.fecha_modificacion.Year, ER.estado_rips_id, ER } into d
                          orderby d.Key.Year descending
                          select new
                          {
                              Anio = d.Key.Year,
                              Fk_estado = d.Key.estado_rips_id,
                              Estado = d.Key.ER.nombre,
                              Cantidad = d.Count(),
                          }).Take(5);

            return result;

        }

        ///<summary>
        /// Lista cantidad los estados de los rips enviados por la plataforma WEB, agrupados por años solo en web validación
        ///</summary>
        [HttpGet]
        [Route("ListarEstadosXAniosWebPreRadicacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstadosXAniosWebPreRadicacion(int iduser)
        {
            var result = (from ER in bd.Estado_RIPS
                          join WPR in bd.Web_Preradicacion on ER.estado_rips_id equals WPR.FK_preradicacion_estado_rips
                          join WV in bd.Web_Validacion on WPR.FK_web_preradicacion_web_validacion equals WV.validacion_id
                          join WU in bd.Web_Usuario on WV.FK_web_validacion_web_usuario equals WU.usuario_id
                          where WV.FK_web_validacion_web_usuario == 1
                          && WPR.FK_preradicacion_estado_rips != 6
                          && WPR.FK_preradicacion_estado_rips != 2
                          group ER by new { WV.fecha_modificacion.Year, ER.estado_rips_id, ER } into d
                          orderby d.Key.Year descending
                          select new
                          {
                              Anio = d.Key.Year,
                              Fk_estado = d.Key.estado_rips_id,
                              Estado = d.Key.ER.nombre,
                              Cantidad = d.Count(),
                          }).Take(5);

            return result;

        }

        ///<summary>
        /// Lista cantidad los estados de los rips enviados por la plataforma WEB, agrupados por años solo en servicio validación
        ///</summary>
        [HttpGet]
        [Route("ListarEstadosXAniosServicioValidacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstadosXAniosServicioValidacion(int iduser)
        {
            var result = (from ER in bd.Estado_RIPS
                          join SV in bd.Servicio_Validacion on ER.estado_rips_id equals SV.estado_rips_id
                          join P in bd.Prestador on SV.prestador equals P.codigo_habilitacion
                          join WU in bd.Web_Usuario on P.prestador_id equals WU.FK_usuario_prestador
                          where WU.usuario_id == 1                         
                          && SV.estado_rips_id != 2
                          group ER by new { SV.fecha_modificacion.Year, ER.estado_rips_id, ER } into d
                          orderby d.Key.Year descending
                          select new
                          {
                              Anio = d.Key.Year,
                              Fk_estado = d.Key.estado_rips_id,
                              Estado = d.Key.ER.nombre,
                              Cantidad = d.Count(),
                          }).Take(5);

            return result;

        }

        ///<summary>
        /// Lista cantidad los estados de los rips enviados por la plataforma WEB, agrupados por años solo en radicacion
        ///</summary>
        [HttpGet]
        [Route("ListarEstadosXAniosRadicacion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstadosXAniosRadicacion(int iduser)
        {
            var result = (from ER in bd.Estado_RIPS
                          join R in bd.Radicacion on ER.estado_rips_id equals R.radicacion_estado_id
                          join SV in bd.Servicio_Validacion on R.servicio_validacion_id equals SV.servicio_validacion_id
                          join P in bd.Prestador on SV.prestador equals P.codigo_habilitacion
                          join WU in bd.Web_Usuario on P.prestador_id equals WU.FK_usuario_prestador
                          where WU.usuario_id == 1
                          && R.radicacion_estado_id != 2
                          && R.radicacion_estado_id != 12
                          && R.radicacion_estado_id != 13
                          group ER by new { SV.fecha_modificacion.Year, ER.estado_rips_id, ER } into d
                          orderby d.Key.Year descending
                          select new
                          {
                              Anio = d.Key.Year,
                              Fk_estado = d.Key.estado_rips_id,
                              Estado = d.Key.ER.nombre,
                              Cantidad = d.Count(),
                          }).Take(5);

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
                          //join WV in bd.Web_Validacion on WU.usuario_id equals WV.FK_web_validacion_web_usuario 
                          //join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion into WPR1
                          join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion into WPR1
                          from WPR in WPR1.DefaultIfEmpty()
                          join SV in bd.Servicio_Validacion on P.codigo equals SV.prestador into SVR
                          from SV in SVR.DefaultIfEmpty()
                          join R in bd.Radicacion on SV.servicio_validacion_id equals R.servicio_validacion_id into R1
                          from R in R1.DefaultIfEmpty()
                          where WV.FK_web_validacion_web_usuario == iduser
                          && ER.estado_rips_id == WV.FK_web_validacion_estado_rips
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
/**
 *  var result = (from ER in bd.Estado_RIPS
                          join WV in bd.Web_Validacion on ER.estado_rips_id equals WV.FK_web_validacion_estado_rips
                          join WU in bd.Web_Usuario on WV.FK_web_validacion_web_usuario equals WU.usuario_id
                          join P in bd.Prestador on WU.FK_usuario_prestador equals P.prestador_id
                          //join WV in bd.Web_Validacion on WU.usuario_id equals WV.FK_web_validacion_web_usuario 
                          //join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion into WPR1
                          join WPR in bd.Web_Preradicacion on WV.validacion_id equals WPR.FK_web_preradicacion_web_validacion into WPR1
                          from WPR in WPR1.DefaultIfEmpty()
                          join SV in bd.Servicio_Validacion on P.codigo equals SV.prestador into SVR
                          from SV in SVR.DefaultIfEmpty()
                          join R in bd.Radicacion on SV.servicio_validacion_id equals R.servicio_validacion_id into R1
                          from R in R1.DefaultIfEmpty()
                          where WV.FK_web_validacion_web_usuario == iduser
                          && ER.estado_rips_id == WV.FK_web_validacion_estado_rips
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
*/
//CONTROLADOR ENCARGADO DE LOS ARCHIVOS Y RIPS
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Permissions;
using System.Security;
using System.Web;
using System.IO.Compression;
using WebAppCargadorRips.Models;
using Ionic.Zip;
using System.Web.Http.Cors;
using WebAppCargadorRips.EF_Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
//using WebAppCargadorRips.Interfaces;
//using WebAppCargadorRips.Repository;

namespace WebAppCargadorRips.Controllers.APIS
{

    [Authorize]
    [RoutePrefix("api/Rips")]
    public class ArchivosController : ApiController
    {

        //private IRadicadosRips radicadoripsRepository;
        private static DateTime fechaActual = DateTime.Today;
        private RipsEntitieConnection bd = new RipsEntitieConnection();
        private string path = HttpContext.Current.Server.MapPath("~/RIPSCargados/");
        //private string path = @"\\Desktop-002g4er\e2\pruebared";

        //Constructor        
        public ArchivosController()
        {
            //this.radicadoripsRepository = new RadicadoRipsRepository(new BD_CargadorRipsConnection());
        }


        ///<summary>
        /// Metodo asincrono carga un archivo con datos del respectivo formulario y genera el consecutivo de preradicación
        ///</summary>
        [Route("Upload")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        //[ValidateMimeMultipartContentFilter]
        public async Task<Object> UploadSingleFile()
        {

            //variables que se estan reciviendo del front
            // obtengo las variables enviadas por el formulario
            var tipoUsuario = HttpContext.Current.Request.Params["tipoUsuario"];
            var categoria = HttpContext.Current.Request.Params["categoria"];
            var IVE = HttpContext.Current.Request.Params["IVE"];
            var NOPOS = HttpContext.Current.Request.Params["NOPOS"];
            var fechaInicio = HttpContext.Current.Request.Params["fechaInicio"];
            var fechaFin = HttpContext.Current.Request.Params["fechaFin"];
            var idUsuario = HttpContext.Current.Request.Params["idUsuario"];
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            //valido la información recivida del formulario
            if (!String.IsNullOrEmpty(tipoUsuario))
            {
                tipoUsuario = tipoUsuario;
            }

            if (!String.IsNullOrEmpty(IVE))
            {
                tipoUsuario = "1";
                categoria = "6";
            }

            if (!String.IsNullOrEmpty(NOPOS))
            {
                tipoUsuario = "2";
                categoria = "1";
            }

            //Valido que el formulario sea enviado con el formato permitido.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                //Armo mensaje y envio al cliente
                MSG.Add(new { type = "error", value = "Formato de envio no permitido" });

                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType)
                    );
                //TODO envio error a la base de datos

            }

            //almaceno la información solo del formulario en la base de datos
            try
            {
                //inserto en la tabla web_validacion
                //3 es estado aprobado sin errores
                var result = bd.SP_Web_Insert_Datos_Rips_a_Validar(tipoUsuario, categoria, fechaInicio, fechaFin, idUsuario,"3").First();

                //si la respuesta del porcedimeinto de insercion a la tabla validacion, es satisfactoria realizo el almacenamiento de los archivos
                if (result.codigo == 201)
                {
                    
                    try {
                        //Inserto en la tabla web_preradicado
                        var preradicadoResult = bd.SP_Web_Insert_Rips_a_Preradicar(Convert.ToInt64(idUsuario), result.ultimoIdInsert).First();
                        //Si el SP de insert de preradicado retorno el una respuesta satisfactoria cargo el archivo
                        if (preradicadoResult.codigo==201)
                        {
                            //intemto crear y guardar los archivos en el forlder para insertalo
                            try
                            {

                                //creo el nombre del path
                                string pathresult = path + "/" + preradicadoResult.ultimoIdInsertPreradicado;
                                //consulto que exista el folder raiz
                                if (!Directory.Exists(pathresult))
                                {
                                    Directory.CreateDirectory(pathresult);
                                    var permisos = new FileIOPermission(FileIOPermissionAccess.AllAccess, pathresult);
                                    var permisosSET = new PermissionSet(PermissionState.None);
                                    permisosSET.AddPermission(permisos);
                                    if (permisosSET.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                                    {
                                    }

                                }

                                //variables que almacenan temporalmente los archivos para no perderlos
                                var streamProvider = new MultipartFormDataStreamProvider(path);
                                await Request.Content.ReadAsMultipartAsync(streamProvider);

                                using (ZipFile zip = new ZipFile())
                                {
                                    foreach (MultipartFileData archivo in streamProvider.FileData)
                                    {
                                        string fileName = "";
                                        if (string.IsNullOrEmpty(archivo.Headers.ContentDisposition.FileName))
                                        {
                                            fileName = Guid.NewGuid().ToString();
                                        }
                                        fileName = archivo.Headers.ContentDisposition.FileName;
                                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                                        {
                                            fileName = fileName.Trim('"');
                                        }
                                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                                        {
                                            fileName = Path.GetFileName(fileName);
                                        }
                                        if (archivo != null && fileName != "")
                                        {
                                            fileName = fileName.Substring(0, 2) + ".txt";
                                            File.Move(archivo.LocalFileName, Path.Combine(pathresult, fileName));
                                            //zip.AddFile(archivo.LocalFileName).FileName = fileName;
                                            //zip.AddFile(File.Delete(archivo.LocalFileName));


                                        }

                                    }
                                    //comprimo los archivos
                                    //https://stackoverflow.com/questions/24391794/c-sharp-move-files-to-zip-folder
                                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                                    zip.AddDirectory(pathresult);
                                    zip.Save(pathresult + ".zip");
                                    /**
                                     * libero de archivos temporales 
                                     * OJO CON ESTA LINEA: ESTA ELIMINA ARCHIVOS TEMPORALES PODRIA ELIMINAR DE OTROS USUARIOS SEGUN RECURRENCIA DE USUARIOS
                                     **/

                                    /*
                                     * TODO
                                     *Por seguridad consulto que los archivos existan en el path del servidor de archivos
                                     */
                                    if (!Directory.Exists(pathresult + ".zip"))
                                    {
                                        //Guardo en tabla pre radicado
                                    }
                                    /*
                                    * Fin Por seguridad consulto que los archivos existan en el path del servidor de archivos
                                    */


                                    //File.Delete(pathresult);

                                }
                                var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1009).First();

                                MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = preradicadoResult.codigo, consec = preradicadoResult.ultimoIdInsertPreradicado });

                            }
                            //error al cargar en el servidor del servicio integrado de WIN
                            catch (Exception e) // si hay un error al crear y guardar el fichero cambio el estado del registro en la tabla Auditoria.Web_Validacion
                            {
                                //Cambio el estado la tabla web_preradicado a disponible
                                var UpdatepreradicadoResult = bd.SP_Web_Update_Estado_Disponible_Preradicado(result.ultimoIdInsert, preradicadoResult.ultimoIdInsertPreradicado).First();
                                //Cambio el estado la tabla web_validacion estado error de carga 
                                var UpdatewebvalidacionError = bd.SP_Web_Update_Estado_Error_Carga_WebValidacion(result.ultimoIdInsert).First();

                                //envio log a archivo de logs 
                                LogsController log = new LogsController(e.ToString());
                                log.createFolder();
                                //TODO envio error a la base de datos
                                //Envio mensaje de error a la vista
                                MSG.Add(new { type = "error", value = "No se caragaron los archivos correctamente en el servidor." });
                                //MSG.Add(new { type = "error", value = path+"-------"+e.ToString() });

                            }

                        }
                        else
                        {

                        }
                    }
                    catch (Exception e) // si hay un error al crear y guardar el fichero cambio el estado del registro en la tabla Auditoria.Web_Validacion
                    {
                        LogsController log = new LogsController(e.ToString());
                        log.createFolder();
                        //Envio mensaje de error a la vista
                        MSG.Add(new { type = "error", value = "NO se caragaron los archivos correctamente en el servidor." });
                    }
                    
                }//fin if respuesta satisfactoria
                else
                {
                    //envio mensaje al usuario final
                    MSG.Add(new { type = result.tipo, value = result.mensaje });
                }

            }
            catch (Exception e)
            {
                LogsController log = new LogsController(e.ToString());
                log.createFolder();
                //TODO enviar a la base de datos 
                MSG.Add(new { type = "error", value = e.ToString() });
                //todo enviar error a la  base de datos
            }

            return Json(MSG);
        }

        ///<summary>
        /// Metodo asincrono carga solo la informacion del formulario en web validacion
        /// cuando la plataforma encuentra un error de estructura de la validacion del archivo
        ///</summary>
        [Route("GuardarValidacionConErrores")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        //[ValidateMimeMultipartContentFilter]
        public async Task<Object> SaveValidacionConErrores()
        {

            //variables que se estan reciviendo del front
            // obtengo las variables enviadas por el formulario
            var tipoUsuario = HttpContext.Current.Request.Params["tipoUsuario"];
            var categoria = HttpContext.Current.Request.Params["categoria"];
            var IVE = HttpContext.Current.Request.Params["IVE"];
            var NOPOS = HttpContext.Current.Request.Params["NOPOS"];
            var fechaInicio = HttpContext.Current.Request.Params["fechaInicio"];
            var fechaFin = HttpContext.Current.Request.Params["fechaFin"];
            var idUsuario = HttpContext.Current.Request.Params["idUsuario"];
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();


            if (!String.IsNullOrEmpty(IVE))
            {
                tipoUsuario = "1";
                categoria = "6";
            }

            if (!String.IsNullOrEmpty(NOPOS))
            {
                tipoUsuario = "2";
                categoria = "1";
            }

            //Valido que el formulario sea enviado con el formato permitido.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                //Armo mensaje y envio al cliente
                MSG.Add(new { type = "error", value = "Formato de envio no permitido" });

                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType)
                    );
                //TODO envio error a la base de datos

            }

            //almaceno la información en la base de datos
            try
            {
                //inserto en la tabla web_validacion
                //3 es estado aprobado con errores de estructura
                var result = bd.SP_Web_Insert_Datos_Rips_a_Validar(tipoUsuario, categoria, fechaInicio, fechaFin, idUsuario,"4").First();
                
                //si la respuesta del porcedimeinto de insercion a la tabla validacion, es satisfactoria realizo el almacenamiento de los archivos
                if (result.codigo == 201)
                {

                    MSG.Add(new { type = "error", value = "No se cargaran archivos con errores en el servidor, por favor ajustelos." });

                }//fin if respuesta satisfactoria
                else
                {
                    //envio mensaje al usuario final
                    MSG.Add(new { type = result.tipo, value = result.mensaje });
                }

            }
            catch (Exception e)
            {
                LogsController log = new LogsController(e.ToString());
                log.createFolder();
                //TODO enviar a la base de datos 
                MSG.Add(new { type = "error", value = e.ToString() });
                //todo enviar error a la  base de datos
            }

            return Json(MSG);
        }

        //api lista la cantidad de rips cargados
        [Route("GetCantidad")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetCantidad(int fktoken)
        {
            var result = bd.SP_CantidadRipsRecibidos(fktoken);
            return result;
        }

        //api lista el estado de los rips
        [Route("GetEstado")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetEstado(int fktoken)
        {
            var result = bd.SP_GetEstadoRipsRecibidos(fktoken);
            return result;
        }



        ///<summary>
        /// Metodo asincrono carga un archivo con datos de su respectivo formulario
        ///</summary>
        [Route("GetListadoRips")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> GetListadoRipstAsync(int fktoken)
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string sEcho = nvc["sEcho"].ToString();
            string sSearch = nvc["sSearch"].ToString();
            int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
            //this provides display length of table it can be 10,25, 50
            int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);
            //iSortCol gives your Column numebr of for which sorting is required
            int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
            //provides your sort order (asc/desc)
            string sortOrder = nvc["sSortDir_0"].ToString();
            var result= new List<VW_Listado_Estado_Rips>();//new List<VW_Listado_Estado_Rips>();
            //Search query when sSearch is not empty
            if (sSearch != "" && sSearch != null) //If there is search query
            {

                result = (from VLR in bd.VW_Listado_Estado_Rips
                          where (VLR.codigo.ToString().Contains(sSearch.ToString()) 
                          || VLR.tipo_usuario.ToString().ToLower().Contains(sSearch.ToString()) 
                          || VLR.categoria.ToString().ToLower().Contains(sSearch.ToString()) 
                          || VLR.periodo_fecha_inicio.Value.ToString().Contains(sSearch.ToString()) 
                          || VLR.periodo_fecha_fin.Value.ToString().Contains(sSearch.ToString()) 
                          || VLR.fecha_cargo.Value.ToString().Contains(sSearch.ToString())
                          || VLR.estado_web_validacion.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.estado_web_preradicacion.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.estado_servicio_validacion.ToString().ToLower().Contains(sSearch.ToString())
                          || VLR.estado_radicacion.ToString().ToLower().Contains(sSearch.ToString())
                          )
                          where VLR.FK_usuario == fktoken
                          orderby VLR.fecha_cargo descending
                          select VLR).ToList();
                // Call Funcion de ordenado  y proveer sorted Data, then Skip using iDisplayStart  
                result = SortFunction(iSortCol, sortOrder, result).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else //Si no hay valores a buscar
            {
                result = (from VLR in bd.VW_Listado_Estado_Rips
                         where VLR.FK_usuario == fktoken
                         orderby VLR.fecha_cargo descending
                         select VLR).ToList();
                // Call Funcion de ordenado  y proveer sorted Data, then Skip using iDisplayStart  
                result = SortFunction(iSortCol, sortOrder, result).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            //get total value count
            var Cantidad = bd.VW_Listado_Estado_Rips.Where(v=>v.FK_usuario == fktoken).Count();
        
             /*result = from VLR in bd.VW_Listado_Estado_Rips
                         where VLR.FK_usuario == fktoken
                         orderby VLR.fecha_cargo descending
                         select VLR;*/
            

            //se Creo un modelo para datatable paging and sending data & enter all the required values
            var VWListadoPaged = new SysDataTablePager<VW_Listado_Estado_Rips>(result, Cantidad, result.Count(), sEcho);

            return VWListadoPaged;
        }


        //Funcion de ordenado
        private List<VW_Listado_Estado_Rips> SortFunction(int iSortCol, string sortOrder, List<VW_Listado_Estado_Rips> list)
        {

            //Sorting for String columns
            if (iSortCol == 1 || iSortCol == 0)
            {
                Func<VW_Listado_Estado_Rips, long> orderingFunction = (c => iSortCol == 1 ? c.codigo : c.codigo); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }
            //Sorting for Int columns TODO
            else if (iSortCol == 2)
            {
                Func<VW_Listado_Estado_Rips, long> orderingFunction = (c => iSortCol == 2 ? c.validacion_id : c.codigo); // compara la columna a ordenar

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
                }
                else
                {
                    list = list.OrderBy(orderingFunction).ToList();

                }
            }

            return list;
        }

        ///<summary>
        ///TODO VALIDAR
        /// Metodo asincrono carga un archivo con datos de su respectivo formulario
        ///</summary>
        [Route("GetListadoRips2")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> GetListadoRipstAsync2(int token)
        {

            var result = from VLR in bd.VW_Listado_Estado_Rips
                         where VLR.FK_usuario == token
                         orderby VLR.fecha_cargo ascending
                         select VLR;//into d
                         //;

            return result;
        }


        /*
        [Route("PostListadoRips")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //public List<Rips_Recibidos> GetProducts()
        public DataTableResponse postd(DataTableRequest request)
        {
            // Query products
            int v = 6;
            //var listadorips = radicadoripsRepository.GetRadicadosRips(v);
            //var products = result.Where(p => p. == v);

            var listadorips = bd.SP_GetEstadoRipsRecibidos(v).ToList();

            // Searching Data
            IEnumerable<Object> filteredProducts;
            if (request.Search.Value != ""  && request.Search.Value != null)
            {
                var searchText = request.Search.Value.Trim();
                filteredProducts = null;
                //NO SIRVIO
                /*filteredProducts =from p in bd.Rips_Recibidos
                     join ev in bd.Param_Estado_Validacion on p.FK_Estado_Validacion_Rips_Recibidos equals ev.IdEstado_Validacion
                     where(p =>
                        p.consecutivoRips_Recibidos.ToString().Contains(searchText) ||
                        p.PeriodoFechaFinRips_Recibidos.ToString().Contains(searchText) ||
                        p.PeriodoFechaInicioRips_Recibidos.ToString().Contains(searchText))
                                  select new
                                  {
                                      id = p.IdRips_Recibidos,
                                      Consec = p.ConsecutivoRips_Recibidos,
                                      FK_Usuario = p.FK_Usuario_Rips_Recibidos,
                                      Students = ev.NombreEstado_Validacion

                                  };
            }
            else
            {
                filteredProducts = listadorips; //result.ToString();//bd.SP_GetEstadoRipsRecibidos(v);
            }


            return new DataTableResponse
            {
                draw = request.Draw,
                recordsTotal = listadorips.Count(),
                recordsFiltered = filteredProducts.Count(),
                data = filteredProducts.ToArray(),
                error = ""
            };

        }*/

        /*
         *  [Route("PostListadoRips")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //public List<Rips_Recibidos> GetProducts()
        public DataTableResponse postd(DataTableRequest request)
        {
            int v = 6;
            var result = bd.SP_GetEstadoRipsRecibidos(v).ToList();

           
            
            return new DataTableResponse
            {
                recordsTotal = result.Count(),
                recordsFiltered = 10,
                data = result.Take(10).ToArray(),
                error = ""
             };

        }
         */

        /*[Route("PostListadoRips")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<Rips_Recibidos> GetProducts()
        {
            //var response = JsonConvert.SerializeObject(radicadoripsRepository.GetRadicadosRips().ToList());
            return radicadoripsRepository.GetRadicadosRips().ToList();
        }*/


        /*
         *  //OJO

        public class CustomerData
        {
            public IList<CustomerSearchDetail> Data { get; set; }
        }
         *private const string CustomerDatas = @"
    {
    ""Data"": [
     {
       ""CompanyName"": ""Microsoft"",
       ""Address"": ""1 Microsoft Way, London"",
       ""Postcode"": ""N1 1NN"",
       ""Telephone"": ""020 7100 1000""  
     },
     {
       ""CompanyName"": ""Nokia"",
       ""Address"": ""2 Nokia Way, London"",
       ""Postcode"": ""N2 2NN"",
       ""Telephone"": ""020 7200 2000""
     },
     {
       ""CompanyName"": ""Apple"",
       ""Address"": ""3 Apple Way, London"",
       ""Postcode"": ""N3 3NN"",
       ""Telephone"": ""020 7300 3000""
     },
     {
       ""CompanyName"": ""Google"",
       ""Address"": ""4 Google Way, London"",
       ""Postcode"": ""N4 4NN"",
       ""Telephone"": ""020 7400 4000""
     },
     {
       ""CompanyName"": ""Samsung"",
       ""Address"": ""5 Samsung Way, London"",
       ""Postcode"": ""N5 5NN"",
       ""Telephone"": ""020 7500 5000""
     }
    ] 
    }";

         /*
         //api lista Todos los RIPS cargados de un prestador
         [Route("PostListadoRips")]
         [HttpPost]
         [EnableCors(origins: "*", headers: "*", methods: "*")]
         //public IHttpActionResult Get([FromUri] SearchRequest request)
         public IHttpActionResult Post(SearchRequest request)
         {
             var allCustomers = JsonConvert.DeserializeObject<CustomerData>(CustomerDatas);
             var response = WrapSearch(allCustomers.Data, request);
             return Ok(response);
         }

         private static CustomerSearchResponse WrapSearch(ICollection<CustomerSearchDetail> details, SearchRequest request)
         {
             var results = ApiHelperSearch.FilterCustomers(details, request.Search.Value).ToList();
             var response = new CustomerSearchResponse
             {
                 Data = PageResults(results, request),
                 draw = request.Draw,
                 recordsFiltered = results.Count,
                 recordsTotal = details.Count
             };
             return response;
         }

         public class CustomerSearch : SearchController
         {
             //api lista Todos los RIPS cargados de un prestador
             [Route("PostListadoRips")]
             [HttpPost]
             [EnableCors(origins: "*", headers: "*", methods: "*")]
             //public IHttpActionResult Get([FromUri] SearchRequest request)
             public IHttpActionResult Post(DataTableRequest request)
             {
                 var allCustomers = JsonConvert.DeserializeObject<CustomerData>(CustomerDatas);
                 var response = WrapSearch(allCustomers.Data, request);
                 return Ok(response);
             }

             private static CustomerSearchResponse WrapSearch(ICollection<CustomerSearchDetail> details, DataTableRequest request)
             {
                 var results = ApiHelperSearch.FilterCustomers(details, request.Search.Value).ToList();
                 var response = new CustomerSearchResponse
                 {
                     Data = PageResults(results, request),
                     draw = request.Draw,
                     recordsFiltered = results.Count,
                     recordsTotal = details.Count
                 };
                 return response;
             }

         }// fin clase CustomerSearch*/

    }

}

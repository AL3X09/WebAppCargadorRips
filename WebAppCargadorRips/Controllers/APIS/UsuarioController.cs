using WebAppCargadorRips.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips.EF_Models;

namespace WebAppCargadorRips.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();
        private string path = HttpContext.Current.Server.MapPath("~/Img/avatarsusers/"); //crea la carpeta apropiada
        private string pathimagen = "/Img/avatarsusers/"; // esta liena debe ser igual a la linea anterior ya que es la que tiene el nombre del donde se alamcenara la imagen
        
        //Constructor
        UsuariosController()
        {

        }

        //RECORDAR QUE SE DEBE HABILITAR CORS + INFO IR AL LINK
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api

        ///<summary>
        /// Lista de los usuarios existentes en el aplicativo
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = bd.SP_GetAllInfoUsers();
            return result;
        }

        ///<summary>
        /// Lista de los usuarios existentes en el aplicativo paginado
        ///</summary>
        [HttpGet]
        [Route("Listar")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get(int pagina)
        {
            var result = bd.SP_GetAllInfoUsers();
            return result;
        }

        /// <summary>
        /// Obtener informacion del usuario loguedo por el codigó de prestador
        /// </summary>
        [HttpGet]
        [Route("GetAllDatos")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get(string codigo)
        {
            var result = bd.SP_GetDatosUsuario(codigo);
            return result;
        }

        /// <summary>
        /// Actualizar la imagen de un usuario especifico
        /// </summary>
        [Route("PostUploadAvatar")]
        [HttpPost]
        [Authorize]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> UpdateAvatar()
        {
            //consulto que exista el folder raiz
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var permisos = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
                var permisosSET = new PermissionSet(PermissionState.None);
                permisosSET.AddPermission(permisos);
                if (permisosSET.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                {
                }

            }

            //variables que se estan reciviendo del front
            // obtengo las variables enviadas por el formulario
            var nombreimagen = HttpContext.Current.Request.Files["avatar"];
            var idUser = HttpContext.Current.Request.Params["idUsuario"];
            var codigousuario = HttpContext.Current.Request.Params["codigousuario"];
            //almaceno el path donde se guarda la imagen
            var contentType = "."+nombreimagen.ContentType.Substring(6);
            var pathguardo = pathimagen+codigousuario+contentType;
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            //valido la información recivida del formulario
            if (String.IsNullOrEmpty(nombreimagen.FileName))
            {
                MSG.Add(new { type = "error", value = "Debe cargar una imagen.", codigo = 0 });
            }
            //Valido que el formulario sea enviado con el formato permitido.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                //Armo mensaje y envio al cliente
                MSG.Add(new { type = "error", value = "Formato de envio no permitido", codigo = 0 });

                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType)
                    
                    );
                //TODO envio error a la base de datos

            }
            else
            {
                
                //almaceno la información en la base de datos
                try
                {
                    var result = bd.SP_UpdateAvatarUser(Int32.Parse(idUser), pathguardo).First();

                    //si la respuesta del porcedimeinto es satisfactoria realizo el almacenamiento de los archivos
                    if (result.codigo == 201)
                    {
                        //variables que almacenan temporalmente los archivos para no perderlos
                        var streamProvider = new MultipartFormDataStreamProvider(path);
                        await Request.Content.ReadAsMultipartAsync(streamProvider);

                        //carga de archivos a la carpeta
                        try
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
                                    fileName = codigousuario + contentType;
                                    if (File.Exists(Path.Combine(path, fileName)))
                                    {
                                        File.Replace(archivo.LocalFileName, Path.Combine(path, fileName), null);
                                    }
                                    else
                                    {
                                        File.Move(archivo.LocalFileName, Path.Combine(path, fileName));
                                    }
                                    
                                }

                            }

                            var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1011).First();

                            MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = result.codigo });

                        }
                        catch (Exception e)
                        {
                            //envio log a archivo de logs 
                            LogsController log = new LogsController(e.ToString());
                            log.createFolder();
                            //TODO envio error a la base de datos
                            MSG.Add(new { type = "error", value = "NO se caragaron los archivos correctamente" });
                        }
                    }//fin if respuesta satisfactoria
                    else
                    {
                        //envio mensaje al usuario final
                        MSG.Add(new { type = result.tipo, value = result.mensaje, codigo = result.codigo });
                    }

                }
                catch (Exception e)
                {
                    //envio log a archivo de logs 
                    LogsController log = new LogsController(e.ToString());
                    log.createFolder();
                    MSG.Add(new { type = "error", value = e.ToString() });
                    //todo enviar error a la  base de datos
                }//end catch

              }//end else

                return Json(MSG);
            }

        /// <summary>
        /// Actualiza los datos de un usuario especifico
        /// </summary>
        [HttpPut]
        [Route("PutUpdateDatosUser")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> UpdateDatosUsuario(ActualizarDatosViewModel datos)
        {
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            if (!ModelState.IsValid)
            {
                //var linq1 = bd.Mensajes.Where(s => s.Codigo_mensaje == 412).First();
                //MSG.Add(new { type = linq1.Tipo_mensaje, value = linq1.Cuerpo_mensaje, codigo = linq1.Codigo_mensaje });
                //return Json(MSG);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {

                try
                {
                    var result = bd.SP_UpdateDatosUser(datos.usuario_id, datos.codigo, datos.nombres, datos.apellidos, datos.telefono, datos.razon_social, datos.correo,datos.id_rol, datos.id_estado).First();
                    await bd.SaveChangesAsync();
                    
                    return Json(result);
                }
                catch (Exception e)
                {
                    //envio mensaje a usuarios
                    var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 412).First();
                    MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });
                    
                    //envio log a archivo de logs 
                    LogsController log = new LogsController(e.ToString());
                    log.createFolder();
                    //TODO enviar a la base de datos
                    return Json(MSG);
                }

            }

        }

        /// <summary>
        /// Actualiza la contraseña de un usuario especifico
        /// </summary>
        [HttpPut]
        [Route("PutUpdateContrasenia")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> UpdateContrasenia(CambiarContraseniaViewModel datos)
        {
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();

            if (!ModelState.IsValid)
            {
                //var linq1 = bd.Mensajes.Where(s => s.Codigo_mensaje == 412).First();
                //MSG.Add(new { type = linq1.Tipo_mensaje, value = linq1.Cuerpo_mensaje, codigo = linq1.Codigo_mensaje });
                //return Json(MSG);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {

                try
                {
                    var result = bd.SP_ChangeContraseniaUser(datos.idUsuario,datos.contrasenia).First();
                    await bd.SaveChangesAsync();
                    //MSG.Add(new { type = result.tipo, value = result.mensaje, codigo = result.codigo });
                    return Json(result);
                }
                catch (Exception e)
                {
                    //envio log a archivo de logs 
                    LogsController log = new LogsController(e.ToString());
                    log.createFolder();
                    //TODO ENVIAR A LA BASE DE DATOS
                    //ENVIO MENSAJE AL USUARIO
                    var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 412).First();

                    MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });
                    return Json(MSG);
                }
                
            }

        }

        ///<summary>
        /// Lista de los Contactos existentes en el universo.
        /// </summary>
        /// <returns>Todos los valores disponibles en el universo</returns>
        [HttpGet]
        [Route("ListarContactos")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> GetContactos()
        {
            var result = from e in bd.Web_Administrador
                         where e.FK_web_administrador_estado_rips == 1
                         select new
                         {
                             id = e.administrador_id,
                             nombre = e.nombres,
                             apellido = e.apellidos,
                             descripcion = e.descripcion,
                             correo = e.correo,
                             extencion = e.extencion,
                             imagen = e.imagen
                         };
            return result;
        }


    }
}

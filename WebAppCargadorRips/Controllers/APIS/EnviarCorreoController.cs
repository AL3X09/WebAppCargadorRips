using WebAppCargadorRips.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppCargadorRips.EF_Models;
using System.IO;


//PARA ESTA API ES RECOMENDABLE ARMAR LOS CORREOS POR EL LINK
//https://html-online.com/editor/
namespace WebAppCargadorRips.Controllers.APIS
{
    [Authorize]
    [RoutePrefix("api/Correo")]
    public class EnviarCorreoController : ApiController
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();
        private string SmtpCorreo { get; set; }//= "a1cifuentes@saludcapital.gov.co";
        private string PassCorreo { get; set; }//= "Colombia2018_*";
        private string FromCorreo { get; set; }//= "a1cifuentes@saludcapital.gov.co";
        private string Host { get; set; }//"smtp.office365.com";
        private int Puerto { get; set; }//""; 
        


        //Sobrecargo el constructor para acerlo publico a ciertas clases, tener cuidado esta sobrecarga es para casos especiales
        public EnviarCorreoController()
        {
            //agrego a la variable privada un texto
            //IdPlantillacorreo = idPlantillacorreo;

        }

        //consulto la tabla que manntiene la configuracion del smtp para envio de correos
        //IEnumerable<Object> SmtpCorreos()
        void SmtpCorreos()
        {
            var result = from e in bd.Correo
                         where e.FK_correo_estado_rips == 1
                         select e;
            foreach (var a in result)
            {
                SmtpCorreo = a.correo1;
                PassCorreo = a.contrasenia;
                FromCorreo = a.from;
                Host = a.host;
                Puerto = a.puerto;
            }
            /*select new
            {
                SmtpCorreo1 = e.CorreoSmtp_Correo,
                PassCorreo1 = e.ContraseniaSmtp_Correo,
                FromCorreo1 = e.FromSmtp_Correo,
            }).FirstOrDefault();*/
            //return result;
            //smtpCorreo = result.smtpCorreo;
        }

        ///<summary>
        /// Lista de las plantillas correos existentes en el aplicativo
        /// </summary>
        /// <returns>Todos los valores disponibles en el universo</returns>
        [HttpGet]
        [Route("ListarPlantillasCorreo")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Object> Get()
        {
            var result = bd.SP_GetAllPlantillasCorreo();
            return result;
        }

        /// <summary>
        /// Envia correo a un email especifico.
        /// </summary>
        /// <returns>Respuesta "SI" o "NO" fue satisfactorio el envio</returns>
        [Route("SendEmail")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> PostSendEmail(EnviarCorreoModel datos)
        {
            //invoco metodo que permite obtener los datos del smtp del correo
            SmtpCorreos();
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();
            
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = Host;
            client.Port = Puerto;//587;
            client.EnableSsl = true;
            // setup Smtp authentication
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpCorreo, PassCorreo);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            //can be obtained from your model
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(FromCorreo);
            msg.To.Add(new MailAddress(datos.usercorreo.ToLower()));
            //consulto el procedimiento para traer la información de la plantilla solicitada
            var result = bd.SP_GetPlantillaCorreo(datos.codPlantilla).First();
            //agrego el asunto
            msg.Subject = result.asunto;
            msg.IsBodyHtml = true;
            //se arma cuerpo del correo
            msg.Body = string.Format(result.cuerpo,datos.usernombre,datos.codigocarga);

            /*multiples correos
             * var mails = lista.Text.Split(';');
            foreach (string dir in mails)
                email.To.Add(dir);*/


            try
            {
                await client.SendMailAsync(msg);
                client.Dispose();
                //USO linq para consultar la tabla de mensajes y dejar el mensaje modificable para el usuario
                var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1009).First();
                MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });

            }
            catch (Exception e)
            {
                //USO linq para consultar la tabla de mensajes y dejar el mensaje modificable para el usuario
                //var linq1 = bd.Mensajes.Where(s => s.Codigo_mensaje == 412).First(); // SE ELIMINA NO VIABLE YA QUE SI EL ERROR ES DE BD EL APLICATIVO NO RESPONDERIA
                //MSG.Add(new { type = linq1.Tipo_mensaje, value = linq1.Cuerpo_mensaje, codigo = linq1.Codigo_mensaje });
                MSG.Add(new { type = "error", value = "El servidor no pudo responder con la solicitud, "+e, codigo = 404 });

                //envio a la tabla log
                //TODO
                //envio a la carpeta logs
                LogsController log = new LogsController(e+"//"+Host.ToString());
                log.createFolder();

            }
            return Json(MSG);
            
        }

        /// <summary>
        /// Envia correo de errores a un email especifico.
        /// </summary>
        /// <returns>Respuesta "SI" o "NO" fue satisfactorio el envio</returns>os.
        [Route("SendEmailErrors")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> PostSendEmailErrors(EnviarCorreoModelErrors datos)
        {
            //invoco metodo que permite obtener los datos del smtp del correo
            SmtpCorreos();
            //variable para la coleccion de los errores a lsitar
            List<string> collection = new List<string>();
            //creo una variable para manejar los mensajes
            var MSG = new List<object>();
            
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = Host;
            client.Port = Puerto;//587;
            // setup Smtp authentication
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpCorreo, PassCorreo);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            //can be obtained from your model
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(FromCorreo);
            msg.To.Add(new MailAddress(datos.usercorreo));
            //consulto el procedimiento para traer la información de la plantilla solicitada
            var result = bd.SP_GetPlantillaCorreo(datos.codPlantilla).First();
            msg.Subject = result.asunto;
            msg.IsBodyHtml = true;

            // Add image attachment from local disk
            //armo las filas 
            foreach (string valor in datos.errores)
            {
                //if(datos.errores)
                collection.Add("<tr><td>" + valor + "</td></tr>");
            }

            //uno los errores encontrados 
            string td = string.Join(" ", collection.ToArray());

            //linea para la imagen
            //LinkedResource windowsLogo = new LinkedResource(@"c:\windows_logo.png", MediaTypeNames.Image.Jpeg);
            //LinkedResource windowsLogo = new LinkedResource(@"~/img/email/footerCorreo.png", MediaTypeNames.Image.Jpeg);

            //armo el cuepo del correo
            msg.Body = string.Format(result.cuerpo, datos.usernombre,td);
            try
            {
                await client.SendMailAsync(msg);
                client.Dispose();
                //USO linq para consultar la tabla de mensajes y dejar el mensaje modificable para el usuario
                var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1008).First();
                MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });

            }
            catch (Exception e)
            {
                //TODO enviar errores a la tabla de errores
                //envio a la carpeta logs
                LogsController log = new LogsController(e.ToString());
                log.createFolder();
                //USO linq para consultar la tabla de mensajes y dejar el mensaje modificable para el usuario
                //var linq1 = bd.Mensajes.Where(s => s.Codigo_mensaje == 412).First();
                //MSG.Add(new { type = linq1.Tipo_mensaje, value = linq1.Cuerpo_mensaje, codigo = linq1.Codigo_mensaje });
                MSG.Add(new { type = "error", value = "El servidor no pudo responder con la solicitud, " + e, codigo = 404 });

            }
            return Json(MSG);
        }

        /// <summary>
        /// Envia correo de recuperación de contraseña a un email especifico.
        /// </summary>
        /// <returns>Respuesta "SI" o "NO" fue satisfactorio el envio</returns>
        [AllowAnonymous]
        [Route("SendEmailRecuperarContrasenia")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> PostSendEmailRecuperacionContrasenia(EnviarCorreoRecuperacionModel datos)
        
        //public async Task<IHttpActionResult> PostSendEmailRecuperacionContrasenia(EnviarCorreoRecuperacionModel datos)
        {
            //Valido que los valores no lleguen vacios
            if(datos != null) {
                
                //var mappedPath = HttpContext.Current.Request.Url.AbsoluteUri; //mapeo la ruta completa del servidor
                var mappedPath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/cargadorrips"; //mapeo la url del servidor

                //invoco metodo que permite obtener los datos del smtp del correo
                SmtpCorreos();
                //creo una variable para manejar los mensajes
                var MSG = new List<object>();

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = Host;
                client.Port = Puerto;//587;
                client.EnableSsl = true;
                // setup Smtp authentication
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpCorreo, PassCorreo);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                //can be obtained from your model
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(FromCorreo);
                msg.To.Add(new MailAddress(datos.usercorreo));
                //consulto el procedimiento para traer la información de la plantilla solicitada
                var result = bd.SP_GetPlantillaCorreo(datos.codPlantilla).First();
                //agrego el asunto
                msg.Subject = result.asunto;
                msg.IsBodyHtml = true;
                //se arma cuerpo del correo                
                msg.Body = String.Format(@result.cuerpo, mappedPath, datos.id,datos.token);
                try
                {
                    await client.SendMailAsync(msg);
                    client.Dispose();
                    //USO linq para consultar la tabla de mensajes y dejar el mensaje modificable para el usuario
                    //TODO el mensaje esta erroneo
                    var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1009).First();
                    MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });

                }
                catch (Exception e)
                {
                    //envio a la tabla log
                    //TODO
                    //envio a la carpeta logs
                    LogsController log = new LogsController(e + "//" + Host.ToString());
                    log.createFolder();
                    return BadRequest();
                }
                
                return Json(MSG);

            }//fin if Valida que los valores no lleguen vacios
            return BadRequest();
            
        }


        /// <summary>
        /// Envia correo de logs dentro del servidor de la aplicación a un email especifico.
        /// </summary>
        /// <returns>Respuesta "SI" o "NO" fue satisfactorio el envio</returns>
        [AllowAnonymous]
        [Route("SendEmailCorreoLogs")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Object> EnviarCorreoLogs(String usercorreo)
        {
            
                //Valido que los valores no lleguen vacios
                if (usercorreo == null)
                {
                    usercorreo = "a1cifuentes@saludcapital.gov.co";
                }
                else {
                
                var mappedPath = HttpContext.Current.Server.MapPath("~/Logs/log_2019.txt");

                //invoco metodo que permite obtener los datos del smtp del correo
                SmtpCorreos();
                //creo una variable para manejar los mensajes
                var MSG = new List<object>();

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = Host;
                client.Port = Puerto;//587;
                client.EnableSsl = true;
                FileStream fs = new FileStream(mappedPath, FileMode.Open, FileAccess.Read);
                Attachment a = new Attachment(fs, "log_2019.txt", MediaTypeNames.Application.Octet);
                //email.Attachments.Add(a);
                // setup Smtp authentication
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpCorreo, PassCorreo);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                //can be obtained from your model
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(FromCorreo);
                msg.To.Add(new MailAddress(usercorreo));
                //consulto el procedimiento para traer la información de la plantilla solicitada
                //var result = bd.SP_GetPlantillaCorreo(datos.codPlantilla).First();
                //agrego el asunto
                msg.Subject = "errores LOG";//result.asunto;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                //se arma cuerpo del correo                
                msg.Body = "PSI";
                msg.Attachments.Add(a);
                try
                {
                    await client.SendMailAsync(msg);
                    client.Dispose();
                    //USO linq para consultar la tabla de mensajes y dejar el mensaje modificable para el usuario
                    //TODO el mensaje esta erroneo
                    var linq1 = bd.Web_Mensaje.Where(s => s.codigo == 1009).First();
                    MSG.Add(new { type = linq1.tipo, value = linq1.cuerpo, codigo = linq1.codigo });

                }
                catch (Exception e)
                {
                    //envio a la carpeta logs
                    LogsController log = new LogsController(e + "//" + Host.ToString());
                    log.createFolder();
                    return BadRequest();
                }

                return Json(MSG);

            }//fin if Valida que los valores no lleguen vacios
            return BadRequest();

        }



    }
}

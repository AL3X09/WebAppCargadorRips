using System;
using System.Linq;
using System.Web.Mvc;
using WebAppCargadorRips.Models;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Web.Security;
using Newtonsoft.Json;
using WebAppCargadorRips.Controllers.APIS;
using WebAppCargadorRips.EF_Models;
using System.Configuration;
using System.IO;
using CaptchaMvc.HtmlHelpers;

namespace WebAppCargadorRips.Controllers
{
    public class CuentaController : Controller
    {
        private RipsEntitieConnection bd = new RipsEntitieConnection();
        // GET: Cuenta
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cuenta
        public ActionResult ViewPartialLogin()
        {
            //Limpio campos
            ModelState.Clear();
            return View("Index");
            
        }

        //Metodo que ejecuta el inicio de sesion
        // POST: /Account/ViewPartialLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ViewPartialLogin(LoginViewModel model)
        {

            //alido los modelos
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //alido el capcha
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ModelState.AddModelError(string.Empty, "Error: captcha no es válido.");
            }
            //si el capccha es alido
            else
            {

                try
                {

                  

                    //Ejecuto los valores
                    Object response = bd.SP_Ingreso_Usuario(model.Usuario, model.Password).ToArray();
                    //
                    await bd.SaveChangesAsync();

                    if (response.Equals(string.Empty))
                        response = HttpStatusCode.NotFound;
                    else
                    {

                        //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                        var json = JsonConvert.SerializeObject(response, Formatting.Indented);
                        //Serializo el contenido en un array para manejar las diferentes opciones
                        MsgClass[] respuesta = JsonConvert.DeserializeObject<MsgClass[]>(json);
                        //recorro el vector de la respuesta y valido   
                        foreach (var item in respuesta)
                        {

                            if (item.codigo != 200)
                            {
                                ModelState.AddModelError(string.Empty, item.mensaje);
                            }
                            else if (item.codigo == 200)
                            {
                                FormsAuthentication.SetAuthCookie(model.Usuario, false);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                //envio error a la api logs errores
                                //TODO
                                //Limpio campos
                                ModelState.Clear();
                                //envio un mensaje al usuario
                                ModelState.AddModelError(string.Empty, "La plataforma no esta respondiendo a su solicitud, por favor intente mas tarde");
                            }

                        }

                    }


                    /*
                    }//fin if captcha
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El captcha no se ingresó correctamente.");
                    }//fin else captcha
                    */

                }
                catch (Exception e)
                {
                    //envio error a la api logs errores
                    //TODO
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //Limpio campos
                    ModelState.Clear();
                    //envio error mensaje al usuario
                    //ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                    ModelState.AddModelError(string.Empty, e.ToString());
                }

            }

            //retorno la vista en caso de que no se efectue el regsitro
            return View("Index", model);

        }


        public ActionResult ViewPartialRegistro()
        {
            //Limpio campos
            ModelState.Clear();
            return View("Index");
        }

        //Metodo que ejecuta el registro
        // POST: /Account/ViewPartialRegistro
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ViewPartialRegistro(RegisterViewModel usuario)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //sección del recaptcha
                    var captcharesponse = Request["g-recaptcha-response"];
                    string secretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"]; //esta linea esta en el web config
                    var client = new WebClient();
                    var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, captcharesponse));
                    var obj = JObject.Parse(result);
                    var status = (bool)obj.SelectToken("success");
                    //valido que el status del recapcha sea verdadero
                    if (status == true)
                    {

                        //Ejecuto los valores en el SP
                        Object response = bd.SP_Registro_Usuario(usuario.CodPrestador, usuario.Email, usuario.Pasword, usuario.Nombres, usuario.Apellidos, usuario.RazonSocial, usuario.Telefono, "/Img/avatarsusers/avatar.png", 1, 1, 1, 2).ToArray();
                        //
                        await bd.SaveChangesAsync();

                        if (response.Equals(string.Empty))
                            response = HttpStatusCode.NotFound;
                        else
                        {
                            //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
                            //creo un json dinamico para enviarlo a la vista
                            dynamic dynJson = JsonConvert.DeserializeObject(json);
                            ViewBag.SomeData = dynJson;
                        }
                    }//fin if captcha
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El captcha no se ingresó correctamente.");
                    }//fin else captcha


                }
                catch (Exception e)
                {
                    //envio error a la api logs errores
                    //TODO
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //Limpio campos
                    ModelState.Clear();
                    //envio error mensaje al usuario
                    //ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                    ModelState.AddModelError(string.Empty, e.ToString());
                }

            }
           
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View("Index", usuario);
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

        //este metodo es usado cuando el usuario esta logeado y quiere cambiar su contraseña
        [Authorize]
        public ActionResult CambiarContrasenia()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        //Este metodo es usado cuando el usuario no recuerda y quiere recuperar la contraseña
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult RecuperarContrasenia()
        {
            //Limpio campos
            ModelState.Clear();
            return View();
        }


        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperarContrasenia(RecuperarContraseniaViewModel model)
        {
            if (ModelState.IsValid)
            {

                //var MSG = new List<EnviarCorreoRecuperacionModel>();
                var MSG = new EnviarCorreoRecuperacionModel();
                try
                {
                    //sección del recaptcha
                    var captcharesponse = Request["g-recaptcha-response"];
                    string secretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"]; //esta linea esta en el web config
                    var client = new WebClient();
                    var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, captcharesponse));
                    var obj = JObject.Parse(result);
                    var status = (bool)obj.SelectToken("success");
                    //valido que el status del recapcha sea verdadero
                    if (status == true)
                    {
                        //Ejecuto los valores en el SP
                        var response = bd.SP_GenerarCodigoRecuperacionContraseniaUser(model.Usuario, model.Email).First();//.ToArray();
                        //almaceno cambios asincronamente
                        await bd.SaveChangesAsync();

                        //se elimina xq pueden llgar multiples respuestas if (response.Equals(string.Empty))
                        //response = HttpStatusCode.NotFound;
                        //ModelState.AddModelError(string.Empty, "No se encontraron coincidencias, para restablecer la contraseña");
                        if (response.codigo == 200)
                        {
                            MSG.codPlantilla = 3;
                            MSG.usercorreo = response.correousuario;
                            MSG.id = response.codprestador;
                            MSG.token = response.token;

                            try
                            {
                                // invoco el constructor
                                EnviarCorreoController enviocorreo = new EnviarCorreoController();
                                //llamo el metodo que realiza la acción de envio de correos
                                var postdatos = await enviocorreo.PostSendEmailRecuperacionContrasenia(MSG);
                                //Console.WriteLine(postdatos.GetType());
                                //Console.WriteLine(postdatos.GetType().Name);
                                // valido la respuesta del metodo
                                if (postdatos.GetType().Name != null && postdatos.GetType().Name != "BadRequestResult")
                                {

                                    //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                                    var json = JsonConvert.SerializeObject(response, Formatting.Indented);
                                    //creo un json dinamico para enviarlo a la vista
                                    dynamic dynJson = JsonConvert.DeserializeObject(json);
                                    ViewBag.SomeData = dynJson;
                                    return View("Index");
                                }
                                else
                                {
                                    // si la respuesta del correo fue erronea envio respuesta a la vista   
                                    ModelState.AddModelError(string.Empty, "No se pudo efectuar el restablecimiento de contraseña.");
                                }
                            }
                            catch (Exception e)
                            {
                                // envio error a la api logs errores
                                //TODO
                                //envio a la carpeta logs
                                APIS.LogsController log = new APIS.LogsController(e.ToString());
                                log.createFolder();
                                //envio error mensaje al usuario
                                ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el restablecimiento de contraseña, por favor intente mas tarde");
                                throw e;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.mensaje);
                        }

                    }//fin if captcha
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El captcha no se ingresó correctamente.");
                    }//fin else captcha

                }
                catch (Exception e)
                {
                    // envio error a la api logs errores
                    //TODO
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                }
            }
            return View("RecuperarContrasenia", model);
        }

        // GET: /Account/ValidaPasswordExternal
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult> ValidaContraseniaExternal(validarContraseniaModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //Ejecuto los valores en el SP
                    var response = bd.SP_ValidarDatosRestablecimientoContrasenia(model.id, model.token).First();
                    //
                    await bd.SaveChangesAsync();

                    //valido que el procedimiento se ejecute de manera correcta
                    if (response.codigo != 200)
                    {
                        //Returno la vista principal no hay aviso alguno ya que peude que la pagina pueda estar bajo ataque
                        //cierro session
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                        return RedirectToAction("Index");

                    }
                    else if (response.codigo == 200)
                    {
                        //cierro session
                        FormsAuthentication.SetAuthCookie(response.idusuario.ToString(), false);
                        //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                        var json = JsonConvert.SerializeObject(response.idusuario, Formatting.Indented);
                        //creo un json dinamico para enviarlo a la vista
                        dynamic dynJson = JsonConvert.DeserializeObject(json);
                        ViewBag.TokenP = response.idusuario;
                        return View("NuevaContrasenia", string.Empty);
                    }
                    else
                    {
                        //envio error a la api logs errores
                        //TODO
                        //envio un mensaje al usuario
                        ModelState.AddModelError(string.Empty, "La plataforma no esta respondiendo a su solicitud, por favor intente mas tarde");
                        FormsAuthentication.SignOut();
                        Session.Abandon();
                    }

                }
                catch (Exception e)
                {
                    //cierro session
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    // envio error a la api logs errores
                    //TODO
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                }
            }
            return View("Index");
        }

        //Este metodo es usado cuando el usuario va a ingresar la nueva contraseña
        // GET: /Account/ForgotPassword
        [Authorize]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult NuevaContrasenia()
        {
            return View();
        }


        // Post: /Account/NewPassword
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken] quito el validador del token ya que la aplicación se estalla y no hay forma de solucionar el problema TODO
        //[OutputCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> NuevaContrasenia(CambiarContraseniaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MSG = new EnviarCorreoRecuperacionModel();
                try
                {
                    //sección del recaptcha
                    var captcharesponse = Request["g-recaptcha-response"];
                    string secretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"]; //esta linea esta en el web config
                    var client = new WebClient();
                    var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, captcharesponse));
                    var obj = JObject.Parse(result);
                    var status = (bool)obj.SelectToken("success");
                    //valido que el status del recapcha sea verdadero
                    if (status == true)
                    {
                        //Ejecuto los valores en el SP
                        //borrarSP_Updaterestacontra
                        var response = bd.SP_ChangeContraseniaUser(model.idUsuario, model.contrasenia).First();
                        //
                        await bd.SaveChangesAsync();

                        // el procedimiento envia un codigo de 201 como respuesta
                        if (response.codigo == 201)
                        {
                            //creo un array a partir del json devuelto por la api para tratarlo desde aca y poder enviar los diferentes errores
                            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
                            //creo un json dinamico para enviarlo a la vista
                            dynamic dynJson = JsonConvert.DeserializeObject(json);
                            ViewBag.SomeData = dynJson;
                            FormsAuthentication.SignOut();
                            Session.Abandon();
                            return View("Index");

                        }// fin if valida response
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.mensaje);
                            return RedirectToAction("Index");
                        }

                    }//fin if captcha
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El captcha no se ingresó correctamente.");
                    }//fin else captcha

                }
                catch (Exception e)
                {
                    // envio error a la api logs errores
                    //TODO
                    //envio a la carpeta logs
                    APIS.LogsController log = new APIS.LogsController(e.ToString());
                    log.createFolder();
                    //envio error mensaje al usuario
                    ModelState.AddModelError(string.Empty, "Estamos presentando dificultades en el momento por favor intente mas tarde");
                }
            }
            return View();
        }


    }
}


//Capcha oole

/*
              //MEJORAR PROCESO
              var status = false;
              //sección del recaptcha
              var captcharesponse = Request["g-recaptcha-response"];
              string secretKey = ConfigurationManager.AppSettings["recaptchaPrivateKey"]; //esta linea esta en el web config
              //Mas infromación  en https://www.c-sharpcorner.com/article/integration-of-google-recaptcha-in-websites/
              var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
              var requestUri = string.Format(apiUrl, secretKey, captcharesponse);
              var request = (HttpWebRequest)WebRequest.Create(requestUri);
              using (WebResponse response = request.GetResponse())
              {
                  using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                  {

                      JObject obj = JObject.Parse(stream.ReadToEnd());
                      //var status = (bool)obj.SelectToken("success");
                      var isSuccess = obj.Value<bool>("success");
                      status = (isSuccess) ? true : false;
                      stream.Close();
                  }
              }

                  //valido que el status del recapcha sea verdadero
                  if (status == true)
              {
              */

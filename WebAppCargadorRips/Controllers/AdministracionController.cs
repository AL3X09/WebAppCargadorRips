using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips.Controllers
{
    [Authorize]
    public class AdministracionController : Controller
    {

        // GET: Administracion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Administracion usuarios
        public ActionResult UsuariosView()
        {
            return View();
        }

        // GET: Administracion roles
        public ActionResult RolesView()
        {
            return View();
        }

        // GET: Administracion permiso no se llevara
        public ActionResult PermisosView()
        {
            return View();
        }

        // GET: Administracion plantillas correo
        public ActionResult PlantillasCorreoView()
        {
            return View();
        }

        // GET: Administracion preguntas
        public ActionResult PreguntasView()
        {
            return View();
        }

        // GET: Administracion preguntas
        public ActionResult PresentacionesView()
        {
            return View();
        }

        // GET: Administracion auditorias
        public ActionResult AuditoriasView()
        {
            return View();
        }

    }
}
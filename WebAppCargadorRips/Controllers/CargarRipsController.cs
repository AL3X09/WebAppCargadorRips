using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips.Controllers
{
    [Authorize]
    public class CargarRipsController : Controller
    {

        public CargarRipsController()
        {
        }
        // GET: CargarRips
        public ActionResult ViewCargaRips()
        {
            return View();
        }
    }
}
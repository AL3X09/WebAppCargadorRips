using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppCargadorRips.Controllers
{
    [Authorize]
    public class AyudaController : Controller
    {
        // GET: Ayuda
        public ActionResult Index()
        {
            return View();
        }
    }
}
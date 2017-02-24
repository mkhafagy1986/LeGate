using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelsService.Areas.MVCArea.Controllers
{
    public class HomeController : Controller
    {
        // GET: MVCArea/Home
        [Route("Hotels")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sun200api.Controllers
{
    public class DevicesController : Controller
    {
        // GET: Devices
        public ActionResult Index()
        {
            using (SunContext ctx = new SunContext())
            {
                return View(ctx.Devices.ToList());
            }
        }
    }
}
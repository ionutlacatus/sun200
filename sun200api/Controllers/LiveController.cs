using sun200api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace sun200api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LiveController : Controller
    {
        // GET: Live
        public ActionResult Index()
        {
            using (SunContext ctx = new SunContext())
            {
                SunSampleNew[] samples = ctx.Samples.ToArray();
                return View(samples);
            }
        }

        [HttpGet]
        public ActionResult Info(int deviceid)
        {
            ViewBag.DeviceId = deviceid;
            using (SunContext ctx = new SunContext())
            {
                if (deviceid == 0)
                    return (View(ctx.Samples.OrderByDescending(s => s.Id).ToArray()));
                return View(ctx.Samples.Where(s => s.deviceid == deviceid).OrderByDescending(s => s.Id).ToArray());
            }
        }
    }
}
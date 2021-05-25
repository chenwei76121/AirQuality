using AirQuality.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirQuality.Controllers
{
    [RoutePrefix("Air")]
    public class AirController : Controller
    {
        [HttpGet]
        [Route()]
        [Route("index")]
        // GET: Air
        public ActionResult Index()
        {
            var service = new AirService();
            var viewml = service.GetData();
            var result = viewml.records;
            return View(result);
        }

        [HttpPost]
        [Route()]
        [Route("index")]
        [ValidateAntiForgeryToken]
        public ActionResult Query(string county, string site)
        {            
                var queryresult = AirService.Query(county, site);

                return View("index", queryresult);
                      
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeixinTest.Debug.Controllers
{
    public class DebugController : Controller
    {
        public ActionResult Index()
        {
            var x = new WeixinTest.Service.WeatherReportService().QuerySimpleDecription("盛泽");
            return View();
        }
    }
}

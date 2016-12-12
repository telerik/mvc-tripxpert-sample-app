using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripXpert.DAL;

namespace TripXpert.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts()
        {
            List<Destination> data = TripXpertDAL.GetAllDestinations();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
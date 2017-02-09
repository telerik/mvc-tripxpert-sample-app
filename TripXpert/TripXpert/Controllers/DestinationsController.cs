using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripXpert.DAL;
using TripXpert.Models;

namespace TripXpert.Controllers
{
    public class DestinationsController : Controller
    {
        public ActionResult Destinations()
        {
            return View();
        }

        public ActionResult Destinations_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<DestinationViewModel> data = TripXpertDAL.GetAllDestinations().Select(s => new DestinationViewModel()
            {
                DestinationID = s.DestinationID,
                DefaultImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID, 'M'),
                LowestPrice = TripXpertDAL.GetLowestPriceForDestination(s.DestinationID, null, null),
                TestimonialID = s.TestimonialID,
                IsSpecial = s.IsSpecial,
                Title = s.Title,
                ShortDescription = s.ShortDescription.Split('|'),
                FullDescription = s.FullDescription,
                Duration = s.Duration,
                VideoURL = s.VideoURL,
            });
            
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
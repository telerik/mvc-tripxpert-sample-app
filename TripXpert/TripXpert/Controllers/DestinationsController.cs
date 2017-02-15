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

        public ActionResult DestinationDetails(int id)
        {
            DestinationViewModel destination = TripXpertDAL.GetAllDestinations().Where(x=>x.DestinationID == id).Select(s => new DestinationViewModel()
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
                TourInfos = TripXpertDAL.GetTourInfos(s.DestinationID).Select(t=> new Models.TourInfo()
                {
                    InfoID = t.InfoID,
                    DestinationID = t.DestinationID,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    PerPersonSharing = t.PerPersonSharing,
                    PerSingleOccupancy = t.PerSingleOccupancy,
                    PerChild = t.PerChild,
                    Duration = t.Duration,
                    TourDates = GetTourDates((DateTime)t.StartDate, (DateTime)t.EndDate)
                }),
                Testimonial = new Models.Testimonial(TripXpertDAL.GetTestimonial(s.TestimonialID)),
                Attractions = TripXpertDAL.GetAttactionsForDestionation(s.DestinationID).Select(a=> new Models.Attraction()
                {
                    AttractionID = a.AttractionID,
                    DestinationID = a.DestinationID,
                    Title = a.Title,
                    Location = a.Location,
                    Description = a.Description,
                    Image = new Models.Image( TripXpertDAL.GetAttractionImage(a.AttractionID) )
                }),
            }).FirstOrDefault();


            return View(destination);
        }

        private List<DateTime> GetTourDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            TimeSpan dayDiff = endDate.Subtract(startDate);

            for (int i = 0; i < dayDiff.Days; i++)
            {
                dates.Add(startDate.AddDays(i));
            }

            return dates;
        }

        [HttpPost]
        public ActionResult AttractionLocations(int id=1)
        {
            MapInfo MapInfo = new MapInfo()
            {
                ZoomSettings = TripXpertDAL.GetMapZoomSettings(id, 320, 350),
                MapData = TripXpertDAL.GetMapData(id)
            };

            return Json(MapInfo.MapData);
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

        public JsonResult GetDestinationImages(int destinationId)
        {
            return Json(TripXpertDAL.GetAllDestinationImages(destinationId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PriceInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = TripXpertDAL.GetAllDestinations().Select(s => new DestinationViewModel()
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
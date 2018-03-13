using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Net;
using Wedding_Website.Models;
using System.Data.Entity.Validation;

namespace Wedding_Website.Controllers
{
    public class HomeController : Controller
    {
        private Wedding_DataEntities db = new Wedding_DataEntities();

        /// <summary>
        /// Each ActionResult has a ViewBag.BodyID = "***"; in order to tag the <html> tag in the layout page.  
        /// This is done to apply a css property that helps prevent a bug where scrolling to the bottom of the screen displays a white bar.
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            TrafficLogger();
            ViewBag.BodyID = "index";
            return View();
        }

        public ActionResult HoneyFund()
        {
            TrafficLogger();
            ViewBag.BodyID = "honeyfund";
            return View();
        }

        public ActionResult OurStory()
        {
            TrafficLogger();
            ViewBag.BodyID = "ourstory";
            return View();
        }

        public ActionResult Photos()
        {
            TrafficLogger();
            ViewBag.BodyID = "photos";
            return View();
        }

        public ActionResult WhereWhen()
        {
            TrafficLogger();
            ViewBag.BodyID = "wherewhen";
            return View();
        }

        public ActionResult Error()
        {
            TrafficLogger();
            ViewBag.BodyID = "error";
            return View();
        }
        public ActionResult Photo(string id)
        {
            TrafficLogger();
            var dir = Server.MapPath("/Assets/Images/JJAndLiz");
            var path = Path.Combine(dir, id + ".jpg"); //validate the path for security or use other means to generate the path.
            return base.File(path, "image/jpeg");
        }
        
        /// <summary>
        /// The TrafficLogger() method is applied to each ActionResult above.  
        /// The method logs the controller, action, id, and requesting IP address of the action.
        /// </summary>

        public void TrafficLogger()
        {
            string ipaddress;
            string controller;
            string action;
            string ID;

            controller = ControllerContext.RouteData.Values["controller"].ToString();

            action = ControllerContext.RouteData.Values["action"].ToString();

            var routeValues = ControllerContext.RouteData.Values;
            if (routeValues.ContainsKey("id"))
            {
                ID = ControllerContext.RouteData.Values["id"].ToString();
            }
            else
            {
                ID = null;
            }

            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            Traffic traffic = new Traffic
            {
                DateTime = DateTime.Now,
                IPAddress = ipaddress,
                Controller = controller,
                Action = action,
                ID = ID
            };
            db.Traffic.Add(traffic);
            db.SaveChanges();
        }
        
    }
}
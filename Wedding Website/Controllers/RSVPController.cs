using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wedding_Website.Models;

namespace Wedding_Website.Controllers
{
    public class RSVPController : Controller
    {
        //This is used for mapping to RSVP model
        private Wedding_DataEntities1 db = new Wedding_DataEntities1();

        //This is used for mapping to Traffic model
        private Wedding_DataEntities db2 = new Wedding_DataEntities();

        public ActionResult RSVP()
        {
            TrafficLogger();
            ViewBag.BodyID = "rsvp";
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RSVP([Bind(Include = "RSVPId,FirstName,LasttName,Email,Comment,Response")] RSVP rSVP, AdditionalRSVP[] additionalRSVP)
        {
            TrafficLogger();
            if (ModelState.IsValid)
            {
                string ipaddress;
                ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                rSVP.IPAddress = ipaddress;
                rSVP.CreatedDate = DateTime.Now;
                db.RSVPs.Add(rSVP);
                db.SaveChanges();

                
                //The if statement below checks to see if there are any additional RSVPs added to the main RSVP.
                //If they're additional RSVPs, add each to the database. If not, move on.
                if (additionalRSVP != null)
                {
                    foreach (var item in additionalRSVP)
                    {
                        AdditionalRSVP rsvp = new AdditionalRSVP
                        {
                            RSVPId = rSVP.RSVPId,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            Response = item.Response,
                            CreatedDate = DateTime.Now
                        };
                        db.AdditionalRSVPs.Add(rsvp);
                        db.SaveChanges();
                    }
                }
                
                return RedirectToAction("Confirmation");
            }

            return RedirectToAction("../Home/Error");
        }

        public ActionResult Confirmation()
        {
            TrafficLogger();
            return View();
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
            db2.Traffic.Add(traffic);
            db2.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wedding_Website.Models;
using System.Net.Mail;

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

                ///////////////
                //Email guest RSVP confirmation
                ///////////////

                //if (rSVP.Email != null)
                //{
                //    const String FROM = "no-reply@jjandliz.com";
                //    const String FROMNAME = "JJ and Liz";

                //    String TO = rSVP.Email;

                //    const String SMTP_USERNAME = "********";

                //    const String SMTP_PASSWORD = "********";

                //    const String HOST = "********";

                //    const int PORT = 587;

                //    String SUBJECT = "JJ + Liz RSVP Confirmation";

                //    // The body of the email
                //    String BODY =
                //        "<h1>Thanks for RSVPing!</h1>" +
                //        "<p>RSVP Confirmation:" +
                //        "<br />Name: " + rSVP.FirstName + " " + rSVP.LasttName +
                //        "<br />Response: " + rSVP.Response +
                //        "<br />Received on: " + rSVP.CreatedDate +
                //        "<br />Comment: " + rSVP.Comment +
                //        "<p>";


                //    if (additionalRSVP != null)
                //    {

                //        BODY = BODY + "<p>Additonal Guests: ";

                //        foreach (var item in additionalRSVP)
                //        {
                //            BODY = BODY +
                //            "<br />Name: " + item.FirstName + " " + item.LastName +
                //            "<br />Email: " + item.Email +
                //            "<br />Response: " + item.Response +
                //            "<p>";

                //        }
                //    };

                //    // Create and build a new MailMessage object
                //    MailMessage message = new MailMessage();
                //    message.IsBodyHtml = true;
                //    message.From = new MailAddress(FROM, FROMNAME);
                //    message.To.Add(new MailAddress(TO));
                //    message.Subject = SUBJECT;
                //    message.Body = BODY;
                //    SmtpClient client =
                //        new SmtpClient(HOST, PORT);

                //    client.Credentials =
                //        new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                //    client.EnableSsl = true;

                //    // Send the email. 
                //    try
                //    {
                //        System.Diagnostics.Trace.WriteLine("Attempting to send email...");
                //        client.Send(message);
                //        System.Diagnostics.Trace.WriteLine("Email sent!");
                //    }
                //    catch (Exception ex)
                //    {
                //        System.Diagnostics.Trace.WriteLine("The email was not sent.");
                //        System.Diagnostics.Trace.WriteLine("Error message: " + ex.Message);
                //    }
                //}

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

                        ////////////
                        //Email additional guest RSVP confirmation
                        ////////////

                        //if (item.Email != null)
                        //{
                        //    String FROM = "no-reply@jjandliz.com";
                        //    String FROMNAME = "JJ and Liz";
                        //    string TO = item.Email;
                        //    string SUBJECT = rSVP.FirstName + " " + rSVP.LasttName + " has RSVP'd you for JJ and Liz's wedding!";

                        //    const String SMTP_USERNAME = "********";

                        //    const String SMTP_PASSWORD = "*****";

                        //    const String HOST = "*****";

                        //    const int PORT = 587;

                        //    // The body of the email
                        //    string BODY =
                        //        "<h1>You've been RSVPd for by " + rSVP.FirstName + " " + rSVP.LasttName + ".  Thanks, " + rSVP.FirstName + "!</h1>" +
                        //        "<p>Your RSVP Confirmation:" +
                        //        "<br />Name: " + item.FirstName + " " + item.LastName +
                        //        "<br />Response: " + item.Response +
                        //        "<p>" + rSVP.FirstName + "'s RSVP: " +
                        //        "<br />Name: " + rSVP.FirstName + " " + rSVP.LasttName +
                        //        "<br />Response: " + rSVP.Response;

                        //    // Create and build a new MailMessage object
                        //    MailMessage message = new MailMessage();
                        //    message.IsBodyHtml = true;
                        //    message.From = new MailAddress(FROM, FROMNAME);
                        //    message.To.Add(new MailAddress(TO));
                        //    message.Subject = SUBJECT;
                        //    message.Body = BODY;
                        //    SmtpClient client =
                        //        new SmtpClient(HOST, PORT);

                        //    client.Credentials =
                        //        new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                        //    client.EnableSsl = true;

                        //    // Send the email. 
                        //    try
                        //    {
                        //        System.Diagnostics.Trace.WriteLine("Attempting to send email...");
                        //        client.Send(message);
                        //        System.Diagnostics.Trace.WriteLine("Email sent!");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        System.Diagnostics.Trace.WriteLine("The email was not sent.");
                        //        System.Diagnostics.Trace.WriteLine("Error message: " + ex.Message);
                        //    }
                        //}
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

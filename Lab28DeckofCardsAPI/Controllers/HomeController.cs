using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab28DeckofCardsAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (ViewBag.Remain <= 2)
            {
                HttpWebRequest SH = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/c58hnlpfftg8/shuffle/");
                SH.UserAgent = ".NET Framework Test Client";

                HttpWebResponse sponse;

                sponse = (HttpWebResponse)SH.GetResponse();
            }

            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/c58hnlpfftg8/draw/?count=5");
            WR.UserAgent = ".NET Framework Test Client";

            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string CardInfo = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(CardInfo);
                ViewBag.Cards = JsonData["cards"];
                ViewBag.Remain = JsonData["remaining"];

            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (ViewBag.Remain <= 2)
            {
                HttpWebRequest SH = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/c58hnlpfftg8/shuffle/");
                SH.UserAgent = ".NET Framework Test Client";

                HttpWebResponse sponse;

                sponse = (HttpWebResponse)SH.GetResponse();
            }

            return View();
        }

        public ActionResult Shuffle()
        {
            HttpWebRequest SH = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/c58hnlpfftg8/shuffle/");
            SH.UserAgent = ".NET Framework Test Client";

            HttpWebResponse esponse;

            esponse = (HttpWebResponse)SH.GetResponse();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
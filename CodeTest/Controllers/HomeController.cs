﻿using System;
using System.Linq;
using System.Web.Mvc;
using Quote.Contracts;
using Quote.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;


namespace PruebaIngreso.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuoteEngine quote;

        public HomeController(IQuoteEngine quote)
        {
            this.quote = quote;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                TourCode = "E-U10-PRVPARKTRF",
                Language = Language.Spanish
            };

            var result = this.quote.Quote(request);
            var tour = result.Tours.FirstOrDefault();
            ViewBag.Message = "Test 1 Correcto";
            return View(tour);
        }

        public ActionResult Test2()
        {
            ViewBag.Message = "Test 2 Correcto";
            return View();
        }

        public ActionResult Test3()
        {

            var result = GetData("E-U10-UNILATIN");

            ViewBag.Margin = result;

            return View();
        }

        public ActionResult Test4()
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                Language = Language.Spanish
            };

            var result = this.quote.Quote(request);
            return View(result.TourQuotes);
        }

        private static string GetData(string id)
        {
            var url = "https://refactored-pancake.free.beeceptor.com/margin/" + id;

            using (var client = new HttpClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var responseString = "0.0";
                try
                {
                    responseString = client.GetStringAsync(url).Result;
                    
                }
                catch (Exception e)
                {
                    //deal with it 
                }

                return responseString;

            }
        }
    }
}
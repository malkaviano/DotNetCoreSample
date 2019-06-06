using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LanguageFeatures.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace LanguageFeatures.Controllers
{

    public class HomeController : Controller
    {
        private ILogger logger;

        public HomeController(ILogger<HomeController> log)
        {
            logger = log;
        }

        public ViewResult Index(string id)
        {
            logger.LogInformation($"HHHHHHHHHHHHHH {id}");

            List<string> results = new List<string>();

            foreach (Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No Name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";

                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }

            logger.LogInformation("POOOOOOOOOOOOOOOOOOOOOOORRRRRA");

            return View(results);
        }

        public IActionResult Problem()
        {
            var statusCodePagesFeature = HttpContext.Features.Get<IStatusCodePagesFeature>();

            //if (statusCodePagesFeature != null) statusCodePagesFeature.Enabled = false;

            return StatusCode(500);
        }

        public IActionResult Caralho(int id)
        {
            if (id == 500)
            {
                var viewName = id.ToString();

                return View(viewName);
            }

            return View("Error");
        }

        public IActionResult Exception() => throw new System.Exception();

        public IActionResult ErrorHandler() => View();
    }
}
namespace LanguageFeatures.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using LanguageFeatures.Admin.Models;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Area("Admin")]
    public class HomeController : Controller
    {
        private Person[] data = new Person[] {
            new Person { Name = "Alice", City = "London" },
            new Person { Name = "Bob", City = "Paris" },
            new Person { Name = "Joe", City = "New York" }
        };

        public ViewResult Index() => View(data);

        public IActionResult PostForm()
        {
            var p = new Person();

            p.SelectedValue = "python";

            var compiled = new SelectListGroup { Name = "Compiled" };
            var interpreted = new SelectListGroup { Name = "Interpreted" };

            p.Options = new List<SelectListItem>
            {
            new SelectListItem{Value= "csharp", Text="C#", Group = compiled},
            new SelectListItem{Value= "python", Text= "Python", Group = interpreted},
            new SelectListItem{Value= "cpp", Text="C++", Group = compiled},
            new SelectListItem{Value= "java", Text="Java", Group = compiled},
            new SelectListItem{Value= "js", Text="JavaScript"},
            new SelectListItem{Value= "ruby", Text="Ruby", Group = interpreted},
            };

            return View(p);
        }

        [HttpPost]
        public IActionResult PostForm(Person p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Exception() => throw new System.Exception();
    }
}
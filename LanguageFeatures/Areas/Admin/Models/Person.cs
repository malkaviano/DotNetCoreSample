using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanguageFeatures.Admin.Models
{
    public class Person
    {
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public IEnumerable<SelectListItem> Options { get; set; }
        public string SelectedValue { get; set; }
    }
}
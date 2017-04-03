using System.ComponentModel.DataAnnotations;

namespace MovieCollection.Web.Models
{
    public class Search
    {
        [Display(Name = "Search Title")]
        public string Title { get; set; }
    }
}
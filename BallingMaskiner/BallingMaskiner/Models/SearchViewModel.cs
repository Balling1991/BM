using System.Collections.Generic;
using System.Web.Mvc;

namespace BallingMaskiner.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            SearchResultViewModel = new SearchResultViewModel();
        }

        public List<SelectListItem> Categories { get; set; }
        public string Category { get; set; }

        public string SearchValue { get; set; }

        public bool SearchExecuted { get; set; }

        public SearchResultViewModel SearchResultViewModel { get; set; }
    }
}

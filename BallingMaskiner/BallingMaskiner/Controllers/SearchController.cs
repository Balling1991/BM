using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BallingMaskiner.Models;
using BallingMaskiner.Services;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        readonly SearchService _searchService = new SearchService();
        // GET: Search
        readonly List<string> _categories = new List<string>
            {
                "Maskiner",
                "Kontakter",
                "Manualer",
                "Prospekter",
                "Tilbud",
                "Reservedele",
                "Kunder",
                "Besøg"
            };

        public ActionResult Index()
        {
            SearchViewModel viewModel = new SearchViewModel
            {
                Categories = _categories.Select(x => new SelectListItem() {Value = x, Text = x}).ToList(),
                SearchExecuted = false
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SearchViewModel viewModel)
        {
            viewModel.SearchExecuted = true;
            SearchResultViewModel model = _searchService.Search(viewModel.SearchValue, viewModel.Category);
            viewModel.Categories = _categories.Select(x => new SelectListItem() {Value = x, Text = x}).ToList();
            viewModel.SearchResultViewModel = model;

            return View(viewModel);
        }

        public ActionResult SearchResult(string value, string category)
        {
            SearchResultViewModel viewModel = _searchService.Search(value, category);

            return View(viewModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private ICategoryRepository _categoryRepository;
        public NavController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _categoryRepository.GetAll().Select(c => c.CategoryName).OrderBy(c => c);
            return PartialView("FlexMenu",categories);
        }
    }
}
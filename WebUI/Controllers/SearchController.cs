using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SearchController : Controller
    {
        private IProductRepository _productRepository;
        public int PageSize = 10;
        public SearchController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ActionResult SearchProduct(string searchTerm = "", int page = 1) {
            var query = _productRepository.Products.Where(p => p.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1);
            ProductsListViewModel model = new ProductsListViewModel {
                Products = query
                            .OrderBy(p => p.ProductID)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = query.Count()
                }
            };
            return View(model);
        }
        public ActionResult Autocomplete(string term = "")
        {
            var query = _productRepository.Products.Where(p => p.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) != -1).Select(s => new {
                label = s.Name
            });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
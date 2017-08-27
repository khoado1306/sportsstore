using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUI.Models;
using Domain.Entities;
using System.Collections;
using System.Web.Helpers;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 6;
        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        // GET: Product
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                            .Where(p => category == null || p.Category.CategoryName == category)
                            .OrderBy(p => p.ProductID)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Where(c => c.Category.CategoryName.Equals(category)).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
        public ActionResult ProductDetail(int productId) {
            Product product = repository.GetById(productId);
            if (product != null)
            {
                return View(product);
            }
            else {
                return RedirectToAction("List");
            }
        }
        public FileContentResult GetImage(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        
    }
}
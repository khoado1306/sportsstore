using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles ="StoreAdmin")]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        private ICategoryRepository _categoryRepository;

        public AdminController(IProductRepository repo, ICategoryRepository categoryRepository)
        {
            repository = repo;
            _categoryRepository = categoryRepository;
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }
        public ActionResult Statistics() {
            return View();
        }
        public ActionResult Edit(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null) {
                ProductCreateEditModel model = new ProductCreateEditModel();
                model.Description = product.Description;
                model.Price = product.Price;
                model.ProductName = product.Name;
                model.ImageData = product.ImageData;
                model.ImageMimeType = product.ImageMimeType;
                model.ProductId = product.ProductID;
                model.SelectedCategoryId = product.CategoryId;
                model.Categories = _categoryRepository.GetAll().ToList();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ProductCreateEditModel model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.ImageMimeType = image.ContentType;
                    model.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.ImageData, 0, image.ContentLength);
                }
                Product product = new Product() {
                    ProductID = model.ProductId,
                    Name = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.SelectedCategoryId,
                    ImageData = model.ImageData,
                    ImageMimeType = model.ImageMimeType
                };
                repository.UpdateProduct(product);
                TempData["message"] = string.Format("{0} has been saved", model.ProductName);
                return RedirectToAction("Index");
            }
            else
            {
                //there is something wrong with the data values
                return View(model);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} has been deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
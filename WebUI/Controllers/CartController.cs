using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Domain.Concrete;

namespace WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IProductRepository _productRepository;
        private ICartRepository _cartRepository;
        private ICartLineRepository _cartLineRepository;
        private UserManager<Customer> _customerManager;
        private EFDbContext _context;

        public CartController(IProductRepository productRepository, ICartLineRepository cartLineRepository, ICartRepository cartRepository, EFDbContext context)
        {
            _productRepository = productRepository;
            _cartLineRepository = cartLineRepository;
            _cartRepository = cartRepository;
            _context = context;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
        {
            Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [AllowAnonymous]
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            ShippingDetails shippingDetails = new ShippingDetails();
            _customerManager = new UserManager<Customer>(new UserStore<Customer>(_context));
            Customer currentCustomer = _customerManager.FindByName(HttpContext.User.Identity.Name);
            shippingDetails.Name = currentCustomer.UserName;
            shippingDetails.Line1 = currentCustomer.Address;
            shippingDetails.PhoneNumber = currentCustomer.PhoneNumber;
            shippingDetails.EmailAddress = currentCustomer.Email;
            return View(shippingDetails);
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.LinesCollection.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                _customerManager = new UserManager<Customer>(new UserStore<Customer>(_context));
                Customer currentCustomer = _customerManager.FindByName(HttpContext.User.Identity.Name);
                //_orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.CustomerId = currentCustomer.Id;
                _cartRepository.AddCart(cart);
                foreach (var line in cart.LinesCollection)
                {
                    line.CartId = cart.CartId;
                    _cartLineRepository.CreateLine(line);
                }
                cart.LinesCollection.ToList().ForEach(c=>_context.Entry(c).State = System.Data.Entity.EntityState.Deleted);
                cart.Clear();
                return View("Completed");

            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
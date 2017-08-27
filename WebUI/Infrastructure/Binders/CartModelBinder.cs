using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Domain.Concrete;
using System.Web;

namespace WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        public CartModelBinder()
        {
            _context = new EFDbContext();
            _customerManager = new UserManager<Customer>(new UserStore<Customer>(_context));
           
        }
        private UserManager<Customer> _customerManager;
        private Customer CurrentCustomer { get; set; }
        private string sessionKey = string.Empty;
        private EFDbContext _context;

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CurrentCustomer = _customerManager.FindByName(controllerContext.HttpContext.User.Identity.Name);
            if (CurrentCustomer != null)
            {
                sessionKey = CurrentCustomer.Id;
            }
            else
            {
                sessionKey = "Cart";
            }
            // get the Cart from the session
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            // return the cart
            return cart;
        }
    }
}
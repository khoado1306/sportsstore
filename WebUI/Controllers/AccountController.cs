using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Owin;
using System.Security.Claims;
using System.Web.Security;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private EFDbContext _context;
        private UserManager<Customer> _customerManager;
        private RoleManager<IdentityRole> _roleManager;

        //private IAuthProvider authProvider;
        //public AccountController(IAuthProvider provider)
        //{
        //    authProvider = provider;
        //}
        public AccountController(EFDbContext context)
        {
            _context = context;
            UserStore<Customer> customerStore = new UserStore<Customer>(_context);
            _customerManager = new UserManager<Customer>(customerStore);

            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(_context);
            _roleManager = new RoleManager<IdentityRole>(roleStore);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.UserName = model.UserName;
                customer.FullName = model.FullName;
                customer.Address = model.Address;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                IdentityResult result = _customerManager.Create(customer, model.Password);
                if (result.Succeeded)
                {
                    _customerManager.AddToRole(customer.Id, "StoreUser");
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Error while creating the user!");
                }
            }
            return View(model);
        }

        public ViewResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _customerManager.Find(model.Username, model.Password);
                if (customer != null) {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _customerManager.CreateIdentity(customer, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties properties = new AuthenticationProperties();
                    properties.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(properties, identity);
                    var check = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else {
                        return RedirectToAction("List", "Product");
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult LogOut() {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var currentCustomer = _customerManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
            if (currentCustomer != null) {
                if (Session[currentCustomer.Id] != null) {
                    Session.Remove(currentCustomer.Id);
                }
            }
            authenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}
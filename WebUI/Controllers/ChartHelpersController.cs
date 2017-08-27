using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ChartHelpersController : Controller
    {
        private IProductRepository _productRepository;
        private ICartLineRepository _cartLineRepository;
        private ICartRepository _cartRepository;
        private EFDbContext _context;
        private UserManager<Customer> _customerManager;

        public ChartHelpersController(EFDbContext context, IProductRepository productRepository, ICartLineRepository cartLineRepository, ICartRepository cartRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _cartLineRepository = cartLineRepository;
            _cartRepository = cartRepository;
            UserStore<Customer> customerStore = new UserStore<Customer>(_context);
            _customerManager = new UserManager<Customer>(customerStore);
        }
        public ActionResult CheapestProducts()
        {
            var cheapestProducts = _productRepository.Products.OrderBy(p => p.Price).Take(5).Select(p => new
            {
                ProductName = p.Name,
                Price = p.Price
            }).ToList();
            List<string> xValue = new List<string>();
            cheapestProducts.ForEach(p => xValue.Add(p.ProductName));
            List<string> yValue = new List<string>();
            cheapestProducts.ForEach(p => yValue.Add(p.Price.ToString("c")));

            new Chart(width: 800, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Cheapest Products")
                .SetXAxis("Product Name")
                .SetYAxis("Price")
                .AddSeries("Products", chartType: "Column", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;
        }
        public ActionResult BestProducts()
        {
            var bestSoldProducts = _cartLineRepository.GetAllCartLines().GroupBy(c => c.ProductId).Take(5).Select(s => new
            {
                Name = _productRepository.GetById(s.Key).Name,
                Total = s.Sum(c => c.Quantity)
            }).OrderByDescending(p => p.Total).ToList();
            List<string> xValue = new List<string>();
            bestSoldProducts.ForEach(p => xValue.Add(p.Name));
            List<string> yValue = new List<string>();
            bestSoldProducts.ForEach(p => yValue.Add(p.Total.ToString()));

            new Chart(width: 800, height: 400, theme: ChartTheme.Vanilla3D)
                .AddTitle("Top 5 Best Products")
                .SetXAxis("Product Name")
                .SetYAxis("Total(s)")
                .AddSeries("Products", chartType: "Column", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }
        public ActionResult SalesByMonth()
        {
            var salesByMonth = _cartRepository.GetAll().Where(c => c.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(c => c.CreatedDate.Month).GroupBy(c => c.CreatedDate.Month).Select(s => new
                {
                    Month = s.Key,
                    Total = s.Sum(c => c.TotalPrice)
                }).ToList();
            List<string> xValue = new List<string>();
            salesByMonth.ForEach(p => xValue.Add(p.Month.ToString()));
            List<string> yValue = new List<string>();
            salesByMonth.ForEach(p => yValue.Add(p.Total.ToString()));

            new Chart(width: 800, height: 400, theme: ChartTheme.Vanilla)
                .AddTitle("Sales by Month in this Year")
                .SetXAxis("Months")
                .SetYAxis("Total Profit")
                .AddSeries("Sales By Month", chartType: "Column", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }
        public ActionResult SalesByYear()
        {
            var salesByYear = _cartRepository.GetAll().OrderBy(c => c.CreatedDate.Year).GroupBy(c => c.CreatedDate.Year)
                .Select(s => new
                {
                    Year = s.Key,
                    Total = s.Sum(c => c.TotalPrice)
                }).ToList();

            List<string> xValue = new List<string>();
            salesByYear.ForEach(p => xValue.Add(p.Year.ToString()));
            List<string> yValue = new List<string>();
            salesByYear.ForEach(p => yValue.Add(p.Total.ToString()));

            new Chart(width: 800, height: 400, theme: ChartTheme.Green)
                .AddTitle("Sales by Year")
                .SetXAxis("Year")
                .SetYAxis("Total Profit")
                .AddSeries("Sales By Year", chartType: "Pie", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }
        public ActionResult BestCustomer()
        {
            var bestCustomers = _cartRepository.GetAll().GroupBy(c => c.CustomerId).Select(c => new
            {
                Name = _customerManager.FindById(c.Key).UserName,
                TotalMoneySpent = c.Sum(m => m.TotalPrice)
            }).OrderByDescending(c => c.TotalMoneySpent).ToList();

            List<string> xValue = new List<string>();
            bestCustomers.ForEach(p => xValue.Add(p.Name.ToString()));
            List<string> yValue = new List<string>();
            bestCustomers.ForEach(p => yValue.Add(p.TotalMoneySpent.ToString()));

            new Chart(width: 800, height: 400, theme: ChartTheme.Yellow)
                .AddTitle("Best Customers")
                .SetXAxis("Username")
                .SetYAxis("Total Money Spent")
                .AddSeries("Best Customers", chartType: "column", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }
    }
}
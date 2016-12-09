using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Contracts.Repositories;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.Services;

namespace eCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //Inject repository into the controller rather than instantiate within actionresult
        public IRepositoryBase<Customer> customers;
        public IRepositoryBase<Product> products;
        public IRepositoryBase<Basket> baskets;
        private BasketService basketService;

        //use constructor that takes in interface
        public HomeController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, 
            IRepositoryBase<Basket> baskets )
        {
            this.customers = customers; //customers passed in = customers up the top
            this.products = products;
            this.baskets = baskets;
            basketService = new BasketService(this.baskets);
        }

        public ActionResult Index()
        {
            //CustomerRepository customers = new CustomerRepository(new DataContext());
            //IRepositoryBase<Customer> customers = new CustomerRepository(new DataContext());
            //IRepositoryBase<Product> products = new ProductRepository(new DataContext());
            var productList = products.GetAll();
            return View(productList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            var product = products.GetById(id);
            return View(product);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BasketSummary()
        {
            var model = basketService.GetBasket(this.HttpContext);
            return View(model.BasketItems);
        }

        public ActionResult AddToBasket(int id)
        {
            basketService.AddToBasket(this.HttpContext, id, 1);

            return RedirectToAction("BasketSummary");
        }
    }
}
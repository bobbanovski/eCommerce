using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Contracts.Repositories;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;

namespace eCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //Inject repository into the controller rather than instantiate within actionresult
        public IRepositoryBase<Customer> customers;
        public IRepositoryBase<Product> products;

        //use constructor that takes in interface
        public HomeController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products)
        {
            this.customers = customers; //customers passed in = customers up the top
            this.products = products;
        }

        public ActionResult Index()
        {
            //CustomerRepository customers = new CustomerRepository(new DataContext());
            //IRepositoryBase<Customer> customers = new CustomerRepository(new DataContext());
            //IRepositoryBase<Product> products = new ProductRepository(new DataContext());

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
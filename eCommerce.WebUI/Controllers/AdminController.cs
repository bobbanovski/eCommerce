using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using eCommerce.WebUI.Models.ViewModels;

namespace eCommerce.WebUI.Controllers
{
    public class AdminController : Controller
    {
        //Inject repository into the controller rather than instantiate within actionresult
        public IRepositoryBase<Customer> customers;
        public IRepositoryBase<Product> products;
        public IRepositoryBase<Voucher> vouchers;
        public IRepositoryBase<VoucherType> voucherTypes;

        //use constructor that takes in interface
        public AdminController(IRepositoryBase<Customer> customers, 
            IRepositoryBase<Product> products,
            IRepositoryBase<Voucher> vouchers,
            IRepositoryBase<VoucherType> voucherTypes )
        {
            this.customers = customers; //customers passed in = customers up the top
            this.products = products;
            this.vouchers = vouchers;
            this.voucherTypes = voucherTypes;
        }
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var model = products.GetAll();

            return View(model);
        }

        public ActionResult CreateProduct()
        {
            var model = new Product();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            products.Insert(product);
            products.Commit();
            return RedirectToAction("ProductList");
        }

        public ActionResult EditProduct(int id)
        {
            var product = products.GetById(id);

            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            products.Update(product);
            products.Commit();
            return RedirectToAction("ProductList");
        }

        public ActionResult VoucherTypes()
        {
            var model = voucherTypes.GetAll();
            return View(model);
        }

        public ActionResult CreateVoucherType()
        {
            var newVoucherType = new VoucherType();
            return View(newVoucherType);
        }

        [HttpPost]
        public ActionResult CreateVoucherType(VoucherType voucherType)
        {
            voucherTypes.Insert(voucherType);
            voucherTypes.Commit();
            return RedirectToAction("VoucherTypes");
        }

        public ActionResult EditVoucherType(int id)
        {
            var voucherType = voucherTypes.GetById(id);
            return View(voucherType);
        }

        [HttpPost]
        public ActionResult EditVoucherType(VoucherType voucherType)
        {
            voucherTypes.Update(voucherType);
            voucherTypes.Commit();
            return RedirectToAction("VoucherTypes");
        }
        
        public ActionResult DeleteVoucherType(int id)
        {
            var voucherType = voucherTypes.GetById(id);
            voucherTypes.Delete(voucherType);
            voucherTypes.Commit();
            return RedirectToAction("VoucherTypes");
        }

        public ActionResult Vouchers()
        {
            var model = vouchers.GetAll();
            return View(model);
        }

        public ActionResult CreateVoucher()
        {
            //var model = new CreateVoucherView();
            //model.Products = products.GetAll().ToList();
            //model.VoucherTypes = voucherTypes.GetAll().ToList();
            ViewBag.Products = products.GetAll();
            ViewBag.VoucherTypes = voucherTypes.GetAll();
            var model = new Voucher();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateVoucher(Voucher voucher)
        {
            vouchers.Insert(voucher);
            vouchers.Commit();
            return RedirectToAction("Vouchers");
        }

        public ActionResult EditVoucher(int id)
        {
            ViewBag.Products = products.GetAll();
            ViewBag.VoucherTypes = voucherTypes.GetAll();
            Voucher voucher = vouchers.GetById(id);
            return View(voucher);
        }

        [HttpPost]
        public ActionResult EditVoucher(Voucher voucher)
        {
            vouchers.Update(voucher);
            vouchers.Commit();
            return RedirectToAction("Vouchers");
        }

        public ActionResult DeleteVoucher(int id)
        {
            Voucher voucher = vouchers.GetById(id);
            vouchers.Delete(voucher);
            vouchers.Commit();
            return RedirectToAction("Vouchers");
        }

        [HttpPost]
        public ActionResult DeleteVoucher(Voucher voucher)
        {
            vouchers.Delete(voucher);
            vouchers.Commit();
            return RedirectToAction("Vouchers");
        }
    }
}
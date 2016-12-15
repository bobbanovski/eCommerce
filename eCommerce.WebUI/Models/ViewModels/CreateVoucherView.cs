using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Model;

namespace eCommerce.WebUI.Models.ViewModels
{
    public class CreateVoucherView
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<VoucherType> VoucherTypes { get; set; }

        public Voucher Voucher { get; set; }
    }
}
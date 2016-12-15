using eCommerce.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Contracts.Modules;

namespace eCommerce.Modules.Vouchers.PercentOff
{
    class eVoucher : IeVoucher
    {
        public void ProcessVoucher(IVoucher voucher, IBasket basket, IBasketVoucher basketVoucher)
        {
            if (voucher.MinimumSpend < basket.BasketTotal())
            {
                basketVoucher.Value = (voucher.Value * (basket.BasketTotal() / 100) * -1);
                basketVoucher.VoucherCode = voucher.VoucherCode;
                basketVoucher.VoucherDescription = voucher.VoucherDescription;
                basketVoucher.VoucherId = voucher.VoucherId;

                basket.AddBasketVoucher(basketVoucher);
            }
        }
    }
}

using eCommerce.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Modules.Vouchers.MoneyOff
{
    class eVoucher
    {
        public void ProcessVoucher(IVoucher voucher, IBasket basket, IBasketVoucher basketVoucher)
        {
            if(voucher.MinimumSpend < basket.BasketTotal()) // Then assign voucher to basket
            {
                basketVoucher.Value = voucher.Value * -1;
                basketVoucher.VoucherCode = voucher.VoucherCode;
                basketVoucher.VoucherDescription = voucher.VoucherDescription;
                basketVoucher.VoucherId = voucher.VoucherId;

                basket.AddBasketVoucher(basketVoucher);
            }
        }
    }
}

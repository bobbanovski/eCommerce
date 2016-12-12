using System;
using System.Collections.Generic;

namespace eCommerce.Contracts.Model
{
    public interface IBasket
    {
        Guid BasketId { get; set; }
        DateTime date { get; set; }
        ICollection<IBasketItem> IBasketItems { get; }
        ICollection<IBasketVoucher> IBasketVouchers { get; }
        decimal BasketTotal();
        int BasketItemCount();
        void AddBasketItem(IBasketItem item);
        void AddBasketVoucher(IBasketVoucher voucher);
    }
}
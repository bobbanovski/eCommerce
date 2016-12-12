using System;

namespace eCommerce.Contracts.Model
{
    public interface IBasketItem
    {
        int BasketItemId { get; set; }
        Guid BasketId { get; set; }
        int ProductId { get; set; }
        int Quantity { get; set; }
        IProduct IProduct { get; set; }
    }
}
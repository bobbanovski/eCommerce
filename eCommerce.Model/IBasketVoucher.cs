using System;

namespace eCommerce.Model
{
    public interface IBasketVoucher
    {
        int BasketVoucherId { get; set; }
        int VoucherId { get; set; }
        Guid BasketId { get; set; }
        string VoucherCode { get; set; }
        string VoucherType { get; set; }
        decimal Value { get; set; }
        string VoucherDescription { get; set; }
        int CorrespondingProductId { get; set; }
    }
}
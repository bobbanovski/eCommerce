using eCommerce.Contracts.Model;

namespace eCommerce.Contracts.Modules
{
    public interface IeVoucher
    {
        void ProcessVoucher(IVoucher voucher, IBasket basket, IBasketVoucher basketVoucher);
    }
}
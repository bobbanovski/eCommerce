using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Contracts.Model;

namespace eCommerce.Model
{
    public class Basket : IBasket
    {
        public Guid BasketId { get; set; }
        public DateTime date { get; set; }

        private List<BasketItem> _basketItems;
        private List<BasketVoucher> _basketVouchers;

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
            this.BasketVouchers = new List<BasketVoucher>();
        }

        public decimal BasketTotal()
        {
            decimal? Total = (from item in BasketItems
                select (int?) item.Quantity*item.Product.Price).Sum();
            return Total ?? Decimal.Zero;
        }

        public int BasketItemCount()
        {
            return _basketItems.Count();
        }

        public void AddBasketItem(IBasketItem item)
        {
            _basketItems.Add((BasketItem)item);
        }

        public void AddBasketVoucher(IBasketVoucher voucher)
        {
            _basketVouchers.Add((BasketVoucher)voucher);
        }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<IBasketItem> IBasketItems { get { return _basketItems.ConvertAll(i => (IBasketItem)i); } }

        public virtual ICollection<BasketVoucher> BasketVouchers { get; set; }
        public virtual ICollection<IBasketVoucher> IBasketVouchers { get { return _basketVouchers.ConvertAll(i => (IBasketVoucher)i); } }
    }
}

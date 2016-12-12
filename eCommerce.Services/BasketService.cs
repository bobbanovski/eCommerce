using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Contracts.Model;

namespace eCommerce.Services
{
    public class BasketService
    {
        private IRepositoryBase<Basket> baskets;
        private IRepositoryBase<Voucher> vouchers;
        private IRepositoryBase<VoucherType> voucherTypes;
        private IRepositoryBase<BasketVoucher> basketVouchers;

        public const string BasketSessionName = "eCommerceBasket"; //Arbitrary name

        public BasketService(IRepositoryBase<Basket> baskets, IRepositoryBase<Voucher> vouchers, 
            IRepositoryBase<VoucherType> voucherTypes, IRepositoryBase<BasketVoucher> basketVouchers ) // Constructor
        {
            this.baskets = baskets;
            this.vouchers = vouchers;
            this.voucherTypes = voucherTypes;
            this.basketVouchers = basketVouchers;
        }

        public void AddVoucher(string VoucherCode, HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext);
            Voucher voucher = vouchers.GetAll().FirstOrDefault(v => v.VoucherCode == VoucherCode);

            if (voucher != null)
            {
                VoucherType voucherType = voucherTypes.GetById(voucher.VoucherTypeId);
                if (voucherType != null)
                {
                    BasketVoucher basketVoucher = new BasketVoucher();
                    if (voucherType.Type == "Money Off")
                    {
                        MoneyOff(voucher, basket, basketVoucher);
                    }
                    if (voucherType.Type == "Percent Off")
                    {
                        PercentOff(voucher, basket, basketVoucher);
                    }
                    baskets.Commit();
                }
            }
        }

        public void MoneyOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        {
            decimal basketTotal = basket.BasketTotal();
            if (voucher.MinimumSpend < basketTotal) // Then assign voucher to basket
            {
                basketVoucher.Value = voucher.Value*-1;
                basketVoucher.VoucherCode = voucher.VoucherCode;
                basketVoucher.VoucherDescription = voucher.VoucherDescription;
                basketVoucher.VoucherId = voucher.VoucherId;

                basket.AddBasketVoucher(basketVoucher);
            }
        }

        public void PercentOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        {
            if (voucher.MinimumSpend > basket.BasketTotal())
            {
                basketVoucher.Value = (voucher.Value*(basket.BasketTotal()/100)*-1);
                basketVoucher.VoucherCode = voucher.VoucherCode;
                basketVoucher.VoucherDescription = voucher.VoucherDescription;
                basketVoucher.VoucherId = voucher.VoucherId;
                
                basket.AddBasketVoucher(basketVoucher);
            }
        }

        private Basket createNewBasket(HttpContextBase httpContext)
        {
            //create a new basket.

            //first create a new cookie.
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            //now create a new basket and set the creation date.
            Basket basket = new Basket();
            basket.date = DateTime.Now;
            basket.BasketId = Guid.NewGuid();

            //add and persist in the dabase.
            baskets.Insert(basket);
            baskets.Commit();

            //add the basket id to a cookie
            cookie.Value = basket.BasketId.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public bool AddToBasket(HttpContextBase httpContext, int productId, int quantity)
        {
            bool success = true;

            Basket basket = GetBasket(httpContext);

            if (basket == null)
                basket = createNewBasket(httpContext);

            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            { //Create new Basket
                item = new BasketItem()
                {
                    BasketId = basket.BasketId,
                    ProductId = productId,
                    Quantity = quantity
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + quantity;
            }
            baskets.Commit();

            return success;
        }

        public Basket GetBasket(HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket;

            Guid basketId;

            if (cookie != null)
            {

                if (Guid.TryParse(cookie.Value, out basketId))
                {
                    basket = baskets.GetById(basketId);
                }
                else //Basket has expired
                {
                    basket = createNewBasket(httpContext);
                }
            }
            else
            {
                basket = createNewBasket(httpContext);
            }

            return basket;
        }
    }
}

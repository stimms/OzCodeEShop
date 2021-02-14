using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Pages.Basket
{
    public class BasketViewModel
    {
        public decimal TaxRate { get; set; } = .05M;
        public decimal ShippingRate { get; set; }
        public int Id { get; set; }
        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
        public string BuyerId { get; set; }

        public decimal Total()
        {
            var subtotal = Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
            var tax = subtotal * TaxRate;
            var shipping = Math.Round(Items.Sum(x => x.ShippingPrice * x.Quantity), 2);
            return subtotal + tax + shipping;
        }

        public decimal Tax()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice + x.ShippingPrice * x.Quantity) * TaxRate, 2) ;
        }

        public decimal Shipping()
        {
            return Math.Round(Items.Sum(x => x.ShippingPrice * x.Quantity) * TaxRate, 2);
        }

        public decimal SubTotal()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice + x.ShippingPrice * x.Quantity), 2);
        }
    }
}

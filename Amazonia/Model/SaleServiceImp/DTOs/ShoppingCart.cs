using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    [Serializable()]
    public class ShoppingCart
    {
        public double totalPrice { get; set; }
        public List<ShoppingCartItem> items { get; set; }

        public ShoppingCart()
        {
            items = new List<ShoppingCartItem>();
            totalPrice = 0;
        }

        public ShoppingCart(double totalPrice, List<ShoppingCartItem> items)
        {
            this.totalPrice = totalPrice;
            this.items = items;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            ShoppingCart target = obj as ShoppingCart;
            return target != null
                   && totalPrice == target.totalPrice
                   && items.Equals(target.items);
        }

        public override int GetHashCode()
        {
            var hashCode = 1968053158;
            hashCode = hashCode * -1521134295 + totalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ShoppingCartItem>>.Default.GetHashCode(items);
            return hashCode;
        }
    }
}
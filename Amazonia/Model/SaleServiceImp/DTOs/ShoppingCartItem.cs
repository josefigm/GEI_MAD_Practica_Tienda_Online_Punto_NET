using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    [Serializable()]
    public class ShoppingCartItem
    {
        public ProductDTO product { get; set; }
        public long units { get; set; }
        public double price { get; set; }
        public bool gift { get; set; }

        public ShoppingCartItem(long units, bool gift, ProductDTO product)
        {
            this.units = units;
            this.gift = gift;
            this.product = product;
            this.price = 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            ShoppingCartItem target = obj as ShoppingCartItem;

            return true
                && product.Equals(target.product);
                ;
        }

        public override int GetHashCode()
        {
            var hashCode = -1869399599;
            hashCode = hashCode * -1521134295 + EqualityComparer<ProductDTO>.Default.GetHashCode(product);
            hashCode = hashCode * -1521134295 + units.GetHashCode();
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + gift.GetHashCode();
            return hashCode;
        }

    }
}
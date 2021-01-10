using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    [Serializable()]
    public class SaleLineDTO
    {
        public long units { get; set; }
        public double price { get; set; }
        public bool gift { get; set; }
        public long productId { get; set; }
        public string productName { get; set; }

        public SaleLineDTO(long units, double price, bool gift, long productId, string productName)
        {
            this.units = units;
            this.price = price;
            this.gift = gift;
            this.productId = productId;
            this.productName = productName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            SaleLineDTO target = obj as SaleLineDTO;

            return true
                && (this.units == target.units)
                && (this.price == target.price)
                && (this.gift == target.gift)
                && (this.productId == target.productId)
                && (this.productName == target.productName)
                ;
        }

        public override int GetHashCode()
        {
            var hashCode = -1689074619;
            hashCode = hashCode * -1521134295 + units.GetHashCode();
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + gift.GetHashCode();
            hashCode = hashCode * -1521134295 + productId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(productName);
            return hashCode;
        }
    }
}
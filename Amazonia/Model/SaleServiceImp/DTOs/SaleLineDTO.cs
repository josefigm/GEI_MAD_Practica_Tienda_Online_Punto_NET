using System;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    [Serializable()]
    public class SaleLineDTO
    {
        public long units { get; set; }
        public double price { get; set; }
        public bool gift { get; set; }
        public long productId { get; set; }

        public SaleLineDTO(long units, double price, bool gift, long productId)
        {
            this.units = units;
            this.price = price;
            this.gift = gift;
            this.productId = productId;
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
                ;
        }

        public override int GetHashCode()
        {
            var hashCode = -1781825586;
            hashCode = hashCode * -1521134295 + units.GetHashCode();
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + gift.GetHashCode();
            hashCode = hashCode * -1521134295 + productId.GetHashCode();
            return hashCode;
        }
    }
}
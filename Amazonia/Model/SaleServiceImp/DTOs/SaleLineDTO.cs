using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
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
    }
}

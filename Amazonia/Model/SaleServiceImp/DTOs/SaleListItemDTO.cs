using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    [Serializable()]
    public class SaleListItemDTO
    {
        public long id { get; set; }
        public System.DateTime date { get; set; }
        public string descName { get; set; }
        public double totalPrice { get; set; }

        public SaleListItemDTO(long id, DateTime date, string descName, double totalPrice)
        {
            this.id = id;
            this.date = date;
            this.descName = descName;
            this.totalPrice = totalPrice;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            SaleListItemDTO target = obj as SaleListItemDTO;
            return target != null
                   && id == target.id
                   && date == target.date
                   && descName == target.descName
                   && totalPrice == target.totalPrice;
        }

        public override int GetHashCode()
        {
            var hashCode = -1699464058;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(descName);
            hashCode = hashCode * -1521134295 + totalPrice.GetHashCode();
            return hashCode;
        }
    }
}
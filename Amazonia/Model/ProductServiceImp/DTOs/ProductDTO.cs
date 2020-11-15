using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{

    [Serializable()]
    public class ProductDTO
    {
        #region Properties region

        public Int64 id { get; private set; }
        public string productTitle { get; private set; }
        public Category category { get; private set; }
        public DateTime entryDate { get; private set; }
        public double price { get; private set; }

        #endregion

        public ProductDTO(Int64 id, string productTitle, Category category, DateTime entryDate, double price)
        {
            this.id = id;
            this.productTitle = productTitle;
            this.category = category;
            this.entryDate = entryDate;
            this.price = price;
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as ProductDTO;
            return dTO != null &&
                   id == dTO.id &&
                   productTitle == dTO.productTitle &&
                   EqualityComparer<Category>.Default.Equals(category, dTO.category) &&
                   entryDate == dTO.entryDate &&
                   price == dTO.price;
        }

        public override int GetHashCode()
        {
            var hashCode = 62468132;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(productTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<Category>.Default.GetHashCode(category);
            hashCode = hashCode * -1521134295 + entryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            return hashCode;
        }
    }
}
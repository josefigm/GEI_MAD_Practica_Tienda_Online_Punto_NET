using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{

    [Serializable()]
    public class ProductDTO
    {

        public Int64 id { get; private set; }
        public string productTitle { get; private set; }
        public double price { get; private set; }
        public DateTime entryDate { get; private set; }
        public Category category { get; private set; }


        public ProductDTO(long id, string productTitle, double price, DateTime entryDate, Category category)
        {
            this.id = id;
            this.productTitle = productTitle;
            this.price = price;
            this.entryDate = entryDate;
            this.category = category;
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as ProductDTO;
            return dTO != null &&
                   id == dTO.id &&
                   productTitle == dTO.productTitle &&
                   price == dTO.price &&
                   entryDate == dTO.entryDate &&
                   EqualityComparer<Category>.Default.Equals(category, dTO.category);
        }

        public override int GetHashCode()
        {
            var hashCode = 1037498420;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(productTitle);
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + entryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Category>.Default.GetHashCode(category);
            return hashCode;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    [Serializable()]
    public class CompleteProductDTO
    {

        public Int64 id { get; private set; }
        public string name { get; private set; }
        public double price { get; private set; }
        public DateTime entryDate { get; private set; }
        public String categoryName { get; private set; }
        public long categoryId { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public long stock { get; set; }

        public CompleteProductDTO(long id, string name, double price, DateTime entryDate, string categoryName, long categoryId, string image, string description, long stock)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.entryDate = entryDate;
            this.categoryName = categoryName;
            this.categoryId = categoryId;
            this.image = image;
            this.description = description;
            this.stock = stock;
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as CompleteProductDTO;
            return dTO != null &&
                   id == dTO.id &&
                   name == dTO.name &&
                   price == dTO.price &&
                   entryDate == dTO.entryDate &&
                   categoryName == dTO.categoryName &&
                   image == dTO.image &&
                   description == dTO.description;
        }

        public override int GetHashCode()
        {
            var hashCode = -889147444;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + entryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(categoryName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            return hashCode;
        }
    }
}

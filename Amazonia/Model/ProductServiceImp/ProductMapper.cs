using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public class ProductMapper
    {
        public static ProductDTO ProductToProductDTO(Product product)
        {
            ProductDTO result = new ProductDTO(product.id, product.name, product.Category,product.entryDate, product.price);
            return result;
        }

    }
}

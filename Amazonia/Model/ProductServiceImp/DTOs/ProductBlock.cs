using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs
{
    public class ProductBlock
    {
        public List<ProductDTO> products { get; private set; }
        public bool ExistMoreProducts { get; private set; }

        public ProductBlock(List<ProductDTO> products, bool existMoreProducts)
        {
            this.products = products;
            ExistMoreProducts = existMoreProducts;
        }
    }
}

using System;

namespace Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.Exceptions
{
    [Serializable]
    public class ProductAlreadyOnShoppingCartException : Exception
    {

        public ProductAlreadyOnShoppingCartException(long productId)
           : base("Product aready on shopping cart exception => id = " + productId)
        {
            this.productId = productId;
        }

        public long productId { get; private set; }

    }
}

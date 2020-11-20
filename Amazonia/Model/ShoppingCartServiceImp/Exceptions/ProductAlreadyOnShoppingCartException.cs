using System;

namespace Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.Exceptions
{
    /// <summary>Product is on shopping cart now, so it can't be added</summary>
    [Serializable]
    public class ProductAlreadyOnShoppingCartException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="ProductAlreadyOnShoppingCartException" /> class.</summary>
        /// <param name="productId">The product identifier.</param>
        public ProductAlreadyOnShoppingCartException(long productId)
           : base("Product aready on shopping cart exception => id = " + productId)
        {
            this.productId = productId;
        }

        /// <summary>Gets the product identifier.</summary>
        /// <value>The product identifier.</value>
        public long productId { get; private set; }
    }
}
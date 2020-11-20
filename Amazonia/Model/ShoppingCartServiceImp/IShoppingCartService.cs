using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp
{
    public interface IShoppingCartService
    {
        IProductDao ProductDao { set; }

        /// <summary>Adds to shopping cart.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="units">The units.</param>
        /// <param name="gift">if set to <c>true</c> [gift].</param>
        /// <returns>The sopping cart with new item added </returns>
        /// <exception cref="ProductAlreadyOnShoppingCartException">Product already on shopping cart</exception>
        [Transactional]
        ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, long productId, long units, bool gift);

        /// <summary>Deletes from shopping cart.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The shopping cart without the item of product</returns>
        [Transactional]
        ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId);

        /// <summary>Modifies the shopping cart item.</summary>
        /// <param name="shoppingCart">The shopping cart.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="units">The units.</param>
        /// <param name="gift">if set to <c>true</c> [gift].</param>
        /// <returns>The shopping cart with the item of product modified</returns>
        [Transactional]
        ShoppingCart ModifyShoppingCartItem(ShoppingCart shoppingCart, long productId, long units, bool gift);
    }
}
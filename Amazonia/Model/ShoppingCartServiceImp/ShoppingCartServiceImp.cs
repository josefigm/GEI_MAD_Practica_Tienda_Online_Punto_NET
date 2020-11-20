using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp
{
    public class ShoppingCartServiceImp : IShoppingCartService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Transactional]
        public ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, ShoppingCartItem item)
        {
            if (!shoppingCart.items.Contains(item))
            {
                if (ProductDao.Exists(item.productId))
                {
                    Product product = ProductDao.Find(item.productId);
                    double price = (item.units * product.price);
                    item.price = price;
                    shoppingCart.items.Add(item);

                    shoppingCart.totalPrice += price;
                }
            }
            else
            {
                throw new ProductAlreadyOnShoppingCartException(item.productId);
            }

            return shoppingCart;
        }
    }
}

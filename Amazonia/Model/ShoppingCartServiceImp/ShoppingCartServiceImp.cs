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
        public ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, long productId, long units, bool gift)
        {
            if (!shoppingCart.items.Exists(x => x.productId == productId))
            {
                if (ProductDao.Exists(productId))
                {
                    Product product = ProductDao.Find(productId);

                    ShoppingCartItem item = new ShoppingCartItem(units, gift, productId, product.name);
                    item.price = (item.units * product.price);
                    shoppingCart.items.Add(item);

                    shoppingCart.totalPrice += item.price;
                }
            }
            else
            {
                throw new ProductAlreadyOnShoppingCartException(productId);
            }

            return shoppingCart;
        }

        [Transactional]
        public ShoppingCart DeleteFromShoppingCart(ShoppingCart shoppingCart, long productId)
        {
            ShoppingCartItem item = shoppingCart.items.Find(x => x.productId == productId);
            if (shoppingCart.items.Remove(item))
            {
                shoppingCart.totalPrice -= item.price;
                return shoppingCart;
            }
            else
            {
                return shoppingCart;
            }
        }

        [Transactional]
        public ShoppingCart ModifyShoppingCartItem(ShoppingCart shoppingCart, long productId, long units, bool gift)
        {
            foreach (ShoppingCartItem item in shoppingCart.items)
            {
                if (item.productId == productId)
                {
                    Product product = ProductDao.Find(productId);
                    item.units = units;
                    item.gift = gift;
                    double oldPrice = item.price;
                    item.price = (units * product.price);

                    shoppingCart.totalPrice = (shoppingCart.totalPrice - oldPrice + item.price);
                    break;
                }
            }

            return shoppingCart;
        }
    }
}
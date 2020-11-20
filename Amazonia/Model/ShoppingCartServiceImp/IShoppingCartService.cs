using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Amazonia.Model.ShoppingCartServiceImp
{
    public interface IShoppingCartService
    {
        IProductDao ProductDao { set; }

        [Transactional]
        ShoppingCart AddToShoppingCart(ShoppingCart shoppingCart, ShoppingCartItem item);
    }
}

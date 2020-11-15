using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public interface IProductService
    {
        [Transactional]
        List<Category> FindCategories();

        [Transactional]
        List<Product> RetrieveProductsWithLabel(string lavelValue);
    }
}

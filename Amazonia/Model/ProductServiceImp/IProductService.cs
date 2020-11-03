using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public interface IProductService
    {
        ICategoryDao CategoryDao { set; }


        [Transactional]
        List<Category> FindCategories();
    }
}

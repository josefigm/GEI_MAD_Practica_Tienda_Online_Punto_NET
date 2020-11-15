using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public interface IProductService
    {
        ICategoryDao CategoryDao { set; }
        ICommentDao CommentDao { set; }
        IProductDao ProductDao { set; }
        ILabelDao LabelDao { set; }


        [Transactional]
        List<Category> FindCategories();

        [Transactional]
        List<Product> RetrieveProductsWithLabel(string lavelValue);
    }
}

using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;
using System;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public interface IProductService
    {
        ICategoryDao CategoryDao { set; }
        ICommentDao CommentDao { set; }
        IProductDao ProductDao { set; }
        ILabelDao LabelDao { set; }
        ICommentService CommentService { set; }

        /// <summary>
        /// Finds the categories.
        /// </summary>
        /// <returns></returns>
        [Transactional]
        List<Category> FindCategories();

        /// <summary>
        /// Retrieves the products with label.
        /// </summary>
        /// <param name="lavelValue">The lavel value.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        [Transactional]
        List<Product> RetrieveProductsWithLabel(string lavelValue);
    }
}

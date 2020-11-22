using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public interface IProductService
    {
        ICategoryDao CategoryDao { set; }
        ICommentDao CommentDao { set; }
        IProductDao ProductDao { set; }
        ILabelDao LabelDao { set; }
        ICommentService CommentService { set; }

        IProductDao ProductDaoEntityFramework { set; }

        MemoryCache Cache { get; }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        /// <param name="entryDate">The entry date.</param>
        /// <param name="stock">The stock.</param>
        /// <param name="image">The image.</param>
        /// <param name="description">The description.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <exception cref="ArgumentException"/>
        /// <returns></returns>
        [Transactional]
        Product CreateProduct(string name, double price, DateTime entryDate, long stock, string image, string description, long categoryId);

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <returns></returns>
        [Transactional]
        Product CreateProduct(Product product);

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        /// <returns></returns>
        [Transactional]
        Product UpdateProduct(Product product);

        /// <summary>
        /// Finds the categories.
        /// </summary>
        /// <returns></returns>
        [Transactional]
        List<Category> FindCategories();

        /// <summary>
        /// Finds the product by word and category.
        /// </summary>
        /// <param name="keyWord">The key word.</param>
        /// <param name="category">The category.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        [Transactional]
        List<ProductDTO> FindProductByWordAndCategory(string keyWord, Category category);

        /// <summary>
        /// Finds the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Transactional]
        Product FindProductById(long id);

        /// Retrieves the products with label.
        /// </summary>
        /// <param name="lavelValue">The lavel value.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        [Transactional]
        List<Product> RetrieveProductsWithLabel(string labelValue);

    }
}

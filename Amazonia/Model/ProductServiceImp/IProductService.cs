using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public interface IProductService
    {
        ICategoryDao CategoryDao { set; }

        IProductDao ProductDaoEntityFramework { set; }

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
        Product FindProductById(Int64 id);
    }
}

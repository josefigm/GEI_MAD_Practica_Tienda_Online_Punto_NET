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

        [Transactional]
        Product CreateProduct(string name, double price, DateTime entryDate, long stock, string image, string description, long categoryId);

        [Transactional]
        Product CreateProduct(Product product);

        [Transactional]
        Product UpdateProduct(Product product);

        [Transactional]
        List<Category> FindCategories();

        [Transactional]
        List<ProductDTO> FindProductByWordAndCategory(string keyWord, Category category);

        [Transactional]
        Product FindProductById(Int64 id);

    }
}

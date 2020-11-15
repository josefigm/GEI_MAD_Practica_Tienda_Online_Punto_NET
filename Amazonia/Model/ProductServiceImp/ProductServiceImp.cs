using System.Collections.Generic;
using System.Data.Entity;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public class ProductServiceImp : IProductService
    {
        [Inject]
        IProductDao productDao;

        public ProductServiceImp() { }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Transactional]
        public List<Category> FindCategories()
        {
            return CategoryDao.GetAllElements();
        }

        [Transactional]
        public Product FindProductById(long id)
        {
            return productDao.Find(id);
        }

        [Transactional]
        public List<ProductDTO> FindProductByWordAndCategory(string keyWord, Category category)
        {
            List<ProductDTO> productListOutput = new List<ProductDTO>();
            string cleanKeyWord = keyWord.Trim();

            if (category != null)
            {
                productListOutput = productDao.FindByKeyWordAndCategory(cleanKeyWord, category.id);
            }
            else
            {
                productListOutput = productDao.FindByKeyWord(cleanKeyWord);
            }
            return productListOutput;
        }
    }
}

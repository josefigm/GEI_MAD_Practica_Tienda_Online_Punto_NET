using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public class ProductServiceImp : IProductService
    {
        public ProductServiceImp() { }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }


        public List<Category> FindCategories()
        {
            return CategoryDao.GetAllElements();
        }
    }
}

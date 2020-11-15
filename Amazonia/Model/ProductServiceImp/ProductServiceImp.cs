using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public class ProductServiceImp : IProductService
    {
        public ProductServiceImp() { }

        [Inject]
        private ICategoryDao categoryDao;
    
        [Inject]
        private ICommentDao commentDao;

        [Inject]
        private IProductDao productDao;

        [Inject]
        private ILabelDao labelDao;

        public List<Category> FindCategories()
        {
            return categoryDao.GetAllElements();
        }

        public List<Product> RetrieveProductsWithLabel(string labelValue)
        {
            List<Product> allProducts = productDao.GetAllElements();
            List<Product> productsWithLabel = new List<Product>();

            foreach (Product product in allProducts)
            {
                bool labelFound = false;
                List<Comment> comments = (List<Comment>) product.Comments;
                for (int i = 0; i < comments.Capacity && labelFound == false; i++)
                {
                    List<Label> labels = labelDao.FindLabelsOfComment(comments[i]);
                    for (int j = 0; j < labels.Capacity && labelFound == false; j++)
                    {
                        if (labels[j].value == labelValue)
                        {
                            labelFound = true;
                            productsWithLabel.Add(product);
                        }
                    }
                }
            }

            return productsWithLabel;
        }
    }
}

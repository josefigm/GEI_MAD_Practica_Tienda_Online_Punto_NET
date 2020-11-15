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
        public ICategoryDao CategoryDao { private get; set; }
    
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ILabelDao LabelDao { private get; set; }

        public List<Category> FindCategories()
        {
            return CategoryDao.GetAllElements();
        }

        public List<Product> RetrieveProductsWithLabel(string labelValue)
        {
            List<Product> allProducts = ProductDao.GetAllElements();
            List<Product> productsWithLabel = new List<Product>();

            foreach (Product product in allProducts)
            {
                bool labelFound = false;
                List<Comment> comments = (List<Comment>) product.Comments;
                for (int i = 0; i < comments.Capacity && labelFound == false; i++)
                {
                    List<Label> labels = LabelDao.FindLabelsOfComment(comments[i]);
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

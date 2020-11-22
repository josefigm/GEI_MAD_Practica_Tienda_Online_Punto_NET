using System;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Ninject;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public class ProductServiceImp : IProductService
    {

        public ProductServiceImp() { }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public IProductDao ProductDaoEntityFramework { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ILabelDao LabelDao { private get; set; }

        [Inject]
        public ICommentService CommentService { private get; set; }

        private MemoryCache _Cache = new MemoryCache("cache");
        private List<string> cacheEntries = new List<string>();

        public MemoryCache Cache { get => _Cache; }

        [Transactional]
        public Product CreateProduct(string name, double price, DateTime entryDate, long stock, string image, string description, long categoryId)
        {
            if (name == null || price >= 0 || entryDate == null || stock >= 0)
            {
                throw new ArgumentException("Se han pasado parámetros no válidos");
            }

            Product productToInsert = new Product();
            productToInsert.name = name;
            productToInsert.price = price;
            productToInsert.entryDate = entryDate;
            productToInsert.stock = stock;
            productToInsert.image = image;
            productToInsert.description = description;
            productToInsert.categoryId = categoryId;

            ProductDaoEntityFramework.Create(productToInsert);

            return productToInsert;
        }


        [Transactional]
        public Product CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Se han pasado parámetros nulos");
            }

            if (product.price < 0 || product.stock < 0)
            {
                throw new ArgumentException("Se han pasado parámetros no válidos");
            }

            ProductDaoEntityFramework.Create(product);

            return product;
        }

        [Transactional]
        public Product UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Se han pasado parámetros nulos");
            }

            if (ProductDaoEntityFramework.Find(product.id) == null)
            {
                throw new Exception("Se intenta actualizar un producto que no existe");
            }
            ProductDaoEntityFramework.Update(product);

            return product;
        }


        [Transactional]
        public List<Category> FindCategories()
        {
            return CategoryDao.GetAllElements();
        }


        [Transactional]
        public Product FindProductById(long id)
        {
            return ProductDaoEntityFramework.Find(id);
        }

        private void AddToCache(String entrie, List<ProductDTO> result)
        {
            CacheItem item = new CacheItem(entrie, result);
            CacheItemPolicy itemPolicy = new CacheItemPolicy();

            if (_Cache.GetCount() < 5)
            {
                cacheEntries.Add(entrie);
                _Cache.Add(item, itemPolicy);
            }
            else
            {
                String firstItem = cacheEntries[0];
                cacheEntries.Remove(firstItem);
                cacheEntries.Add(entrie);

                _Cache.Remove(firstItem);
                _Cache.Add(item, itemPolicy);
            }
        }

        private string FormatKeyWordAndCategoryNames(string keyWord, Category category)
        {
            if (category != null)
            {
                return keyWord + "From" + category.name;
            }
            else
            {
                return keyWord;    
            }
        }

        [Transactional]
        public List<ProductDTO> FindProductByWordAndCategory(string keyWord, Category category)
        {
            // Category sí puede ser null (Es otro CU)

            if (keyWord == null)
            {
                throw new ArgumentNullException("Se han pasado parámetros nulos");
            }

            List<ProductDTO> productListOutput = new List<ProductDTO>();
            string cleanKeyWord = keyWord.Trim();
            string formattedKeywordAndCategory = FormatKeyWordAndCategoryNames(cleanKeyWord, category);

            if (_Cache.Contains(formattedKeywordAndCategory))
            {
                List<ProductDTO> cacheResult = (List<ProductDTO>)_Cache.GetCacheItem(formattedKeywordAndCategory).Value;
                return cacheResult;
            }

            if (category != null)
            {
                productListOutput = ProductDaoEntityFramework.FindByKeyWordAndCategory(cleanKeyWord, category.id);
            }
            else
            {
                productListOutput = ProductDaoEntityFramework.FindByKeyWord(cleanKeyWord);
            }

            AddToCache(formattedKeywordAndCategory, productListOutput);

            return productListOutput;
        }

        public List<Product> RetrieveProductsWithLabel(string labelValue)
        {
            if (labelValue == null)
            {
                throw new ArgumentNullException("Valor de etiqueta nulo");
            }

            List<Product> allProducts = ProductDao.GetAllElements();
            List<Product> productsWithLabel = new List<Product>();

            foreach (Product product in allProducts)
            {
                bool labelFound = false;
                List<Comment> comments = CommentService.FindCommentsOfProduct(product.id);
                for (int i = 0; i < comments.Count && labelFound == false; i++)
                {
                    List<Label> labels = LabelDao.FindLabelsOfComment(comments[i]);
                    for (int j = 0; j < labels.Count && labelFound == false; j++)
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

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
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.Cache;

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

        private CacheContainer cache = CacheContainer.GetCacheContainer();

        [Transactional]
        public Product UpdateProduct(long productId, string name, double price, long stock, string description)
        {
            if (name == null || name.Length == 0 || price <= 0 || stock < 0)
            {
                throw new ArgumentException("Se han pasado parámetros no válidos");
            }

            Product productToProcess = ProductDaoEntityFramework.Find(productId);

            if (productToProcess == null)
            {
                throw new Exception("Se intenta actualizar un producto que no existe");
            }

            productToProcess.name = name;
            productToProcess.price = price;
            productToProcess.stock = stock;
            productToProcess.description = description;

            ProductDaoEntityFramework.Update(productToProcess);

            return productToProcess;
        }


        [Transactional]
        public List<Category> FindCategories()
        {
            return CategoryDao.GetAllElements();
        }


        [Transactional]
        public CompleteProductDTO FindProductById(long id)
        {
            return ProductDaoEntityFramework.FindCompleteProductDTO(id);
        }

        [Transactional]
        public ProductBlock FindProductByWord(string keyWord, int startIndex, int count)
        {
            if (keyWord == null || keyWord.Length == 0)
            {
                throw new ArgumentException("Se han pasado parámetros invalidos");
            }

            List<ProductDTO> productListOutput = new List<ProductDTO>();
            string cleanKeyWord = keyWord.Trim();

            
            string formattedSearch = CacheUtils.FormatSearch(cleanKeyWord, -1, startIndex, count);

            if (cache.IsInCache(formattedSearch))
            {
                ProductBlock cacheResult = cache.GetEntrie(formattedSearch);
                return cacheResult;
            }

            productListOutput = ProductDaoEntityFramework.FindByKeyWord(cleanKeyWord, startIndex, count + 1);

            bool existMoreProducts = (productListOutput.Count == count + 1);

            if (existMoreProducts)
            {
                productListOutput.RemoveAt(count);
            }

            ProductBlock result = new ProductBlock(productListOutput, existMoreProducts);

            cache.AddToCache(formattedSearch, result);

            return result;
        }

        [Transactional]
        public ProductBlock FindProductByWordAndCategory(string keyWord, long categoryId, int startIndex, int count)
        {
            // Category sí puede ser null (Es otro CU)

            if (keyWord == null || keyWord.Length == 0)
            {
                throw new ArgumentNullException("Se han pasado parámetros invalidos");
            }

            Category category = CategoryDao.Find(categoryId);
            if (category == null)
            {
                throw new InstanceNotFoundException(categoryId, "No existe esa categoria");
            }

            List<ProductDTO> productListOutput = new List<ProductDTO>();
            string cleanKeyWord = keyWord.Trim();

            string formattedSearch = CacheUtils.FormatSearch(cleanKeyWord, category.id, startIndex, count);

            if (cache.IsInCache(formattedSearch))
            {
                ProductBlock cacheResult = cache.GetEntrie(formattedSearch);
                return cacheResult;
            }

            productListOutput = ProductDaoEntityFramework.FindByKeyWordAndCategory(cleanKeyWord, categoryId, startIndex, count +1);

            bool existMoreProducts = (productListOutput.Count == count + 1);

            if (existMoreProducts)
            {
                productListOutput.RemoveAt(count);
            }

            ProductBlock result = new ProductBlock(productListOutput, existMoreProducts);

            cache.AddToCache(formattedSearch, result);

            return result;
        }

        public ProductBlock RetrieveProductsWithLabel(int startIndex, int count, long labelId)
        {
            Label label = LabelDao.Find(labelId);

            List<Comment> comentariosConLabel = CommentDao.FindCommentsByLabel(label);

            List<ProductDTO> productDTOs = ProductDao.FindProductsByComments(startIndex, count + 1, comentariosConLabel);

            bool existMoreProducts = (productDTOs.Count == count + 1);

            if (existMoreProducts)
            {
                productDTOs.RemoveAt(count);
            }

            ProductBlock result = new ProductBlock(productDTOs, existMoreProducts);

            return result;
        }
    }
}

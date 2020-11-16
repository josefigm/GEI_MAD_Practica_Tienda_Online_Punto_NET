using System;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CategoryDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp
{
    public class ProductServiceImp : IProductService
    {
        
        public ProductServiceImp() { }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        [Inject]
        public IProductDao ProductDaoEntityFramework { private get; set; }

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

            if (category != null)
            {
                productListOutput = ProductDaoEntityFramework.FindByKeyWordAndCategory(cleanKeyWord, category.id);
            }
            else
            {
                productListOutput = ProductDaoEntityFramework.FindByKeyWord(cleanKeyWord);
            }
            return productListOutput;
        }
    }
}

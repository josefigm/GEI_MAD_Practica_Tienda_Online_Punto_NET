using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        public List<ProductDTO> FindByKeyWordAndCategory(string keyWord, int categoryId)
        {

            DbSet<Product> productList = Context.Set<Product>();

            List<Product> productListToTransform =
                (from p in productList
                    where p.name.ToLower().contains(keyWord.ToLower()) &&
                    p.categoryId == categoryId
                    select p);

            List<ProductDTO> productListOutput = new List<ProductDTO>();

            foreach (Product product in productListToTransform)
            {
                ProductDTO productDTO = new ProductDTO(product.id, product.name, product.Category, product.entryDate, product.price);
                productListOutput.Add(productDTO);
            }

            return productListOutput;
        }

        public List<ProductDTO> FindByKeyWord(string keyWord)
        {
            DbSet<Product> productList = Context.Set<Product>();

            List<Product> productListToTransform =
                (from p in productList
                 where p.name.ToLower().contains(keyWord.ToLower())
                 select p);

            List<ProductDTO> productListOutput = new List<ProductDTO>();

            foreach (Product product in productListToTransform)
            {
                ProductDTO productDTO = ProductMapper.ProductToProductDTO(product);
                productListOutput.Add(productDTO);
            }
            return productListOutput;
        }
    }
}


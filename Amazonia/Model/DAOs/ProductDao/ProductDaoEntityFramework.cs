using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        public List<ProductDTO> FindByKeyWordAndCategory(string keyWord, long categoryId, int startIndex, int count)
        {

            DbSet<Product> productList = Context.Set<Product>();

            List<Product> productListToTransform =
                    (from p in productList
                     where (p.name.ToLower().Contains(keyWord.ToLower())) && (p.categoryId == categoryId)
                     orderby p.entryDate
                     select p).Skip(startIndex).Take(count).ToList<Product>();

            List<ProductDTO> productListOutput = new List<ProductDTO>();

            foreach (Product product in productListToTransform)
            {
                ProductDTO productDTO = ProductMapper.ProductToProductDto(product);
                productListOutput.Add(productDTO);
            }

            return productListOutput;
        }

        public List<ProductDTO> FindByKeyWord(string keyWord, int startIndex, int count)
        {
            DbSet<Product> productList = Context.Set<Product>();

            List<Product> productListToTransform =
                (from p in productList
                 where p.name.ToLower().Contains(keyWord.ToLower())
                 orderby p.entryDate
                 select p).Skip(startIndex).Take(count).ToList<Product>();

            List<ProductDTO> productListOutput = new List<ProductDTO>();

            foreach (Product product in productListToTransform)
            {
                ProductDTO productDTO = ProductMapper.ProductToProductDto(product);
                productListOutput.Add(productDTO);
            }
            return productListOutput;
        }

        public CompleteProductDTO FindCompleteProductDTO(long productId)
        {
            Product product = Find(productId);

            return ProductMapper.ProductToCompleteProductDto(product);
        }
    }
}


using System;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao
{
    public interface IProductDao : IGenericDao<Product, Int64>
    {
        List<ProductDTO> FindByKeyWordAndCategory(string keyWord, long categoryId, int startIndex, int count);

        List<ProductDTO> FindByKeyWord(string keyWord, int startIndex, int count);
    }
}

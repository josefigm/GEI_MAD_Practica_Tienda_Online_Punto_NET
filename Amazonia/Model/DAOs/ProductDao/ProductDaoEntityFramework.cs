using Es.Udc.DotNet.ModelUtil.Dao;
using System;


namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
    }
}

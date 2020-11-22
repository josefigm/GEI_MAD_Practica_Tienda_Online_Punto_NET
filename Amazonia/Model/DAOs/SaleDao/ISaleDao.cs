using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao
{
    public interface ISaleDao : IGenericDao<Sale, Int64>
    {
        List<Sale> FindByClientLogin(string clientLogin, int startIndex, int count);
    }
}

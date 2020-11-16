using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao
{
    public class SaleDaoEntityFramework :
        GenericDaoEntityFramework<Sale, Int64>, ISaleDao
    {
    }
}

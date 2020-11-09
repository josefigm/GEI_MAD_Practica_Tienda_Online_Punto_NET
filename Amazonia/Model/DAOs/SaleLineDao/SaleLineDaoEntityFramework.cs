using Es.Udc.DotNet.ModelUtil.Dao;
using System;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.SaleLineDao
{
    public class SaleLineDaoEntityFramework :
        GenericDaoEntityFramework<SaleLine, Int64>, ISaleLineDao
    {
    }
}

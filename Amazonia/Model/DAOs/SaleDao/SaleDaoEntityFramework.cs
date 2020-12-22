using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.SaleDao
{
    public class SaleDaoEntityFramework :
        GenericDaoEntityFramework<Sale, Int64>, ISaleDao
    {
        public List<Sale> FindByClientId(long clientId, int startIndex, int count)
        {
            DbSet<Sale> sales = Context.Set<Sale>();

            var result =
                    (from s in sales
                     where s.clientId == clientId
                     orderby s.id
                     select s).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
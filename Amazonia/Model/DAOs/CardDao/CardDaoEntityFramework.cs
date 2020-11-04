using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao
{
    public class CardDaoEntityFramework :
       GenericDaoEntityFramework<Card, Int64>, ICardDao
    {
    }
}

using Es.Udc.DotNet.ModelUtil.Dao;
using System;


namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao
{
    public class CardDaoEntityFramework :
       GenericDaoEntityFramework<Card, Int64>, ICardDao
    {
    }
}

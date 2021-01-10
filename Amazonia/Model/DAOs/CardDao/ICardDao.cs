using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao
{
    public interface ICardDao : IGenericDao<Card, Int64>
    {
        Card FindByNumber(string number);

        Card FindDefaultCard(long clientId);
    }
}

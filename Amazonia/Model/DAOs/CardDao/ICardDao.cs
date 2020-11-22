using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao
{
    public interface ICardDao : IGenericDao<Card, String>
    {
        List<Card> FindCardsOfClient(Client client);

    }
}

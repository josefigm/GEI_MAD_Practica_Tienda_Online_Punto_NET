using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;


namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao
{
    public class CardDaoEntityFramework :
       GenericDaoEntityFramework<Card, String>, ICardDao
    {

        public List<Card> FindCardsOfClient(Client client)
        {

            DbSet<Card> cardList = Context.Set<Card>();

            List<Card> result =
                (from l in cardList
                 where l.Clients1.Select(c => c.login).Contains(client.login)
                 select l).ToList<Card>();

            return result;

        }

    }
}

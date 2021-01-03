using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao
{
    public class CardDaoEntityFramework :
       GenericDaoEntityFramework<Card, Int64>, ICardDao
    {
        public Card FindByNumber(string number)
        {

            Card card = null;

            DbSet<Card> cards = Context.Set<Card>();

            var result =
                (from c in cards
                 where c.number == number
                 select c);

            card = result.FirstOrDefault();

            if (card == null)
                throw new InstanceNotFoundException(number,
                    typeof(Card).FullName);

            return card;
        }

        public Card FindDefaultCard(long clientId)
        {

            Card card = null;

            DbSet<Card> cards = Context.Set<Card>();

            var result =
                (from c in cards
                 where (c.clientId == clientId && c.defaultCard == true)
                 select c);

            card = result.FirstOrDefault();

            return card;
        }
    }
}

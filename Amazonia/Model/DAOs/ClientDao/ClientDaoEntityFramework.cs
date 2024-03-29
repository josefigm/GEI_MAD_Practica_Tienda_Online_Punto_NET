﻿using System;
using System.Linq;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao
{
    public class ClientDaoEntityFramework :
        GenericDaoEntityFramework<Client, Int64>, IClientDao
    {

        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public ClientDaoEntityFramework()
        {
        }

        #endregion Public Constructors


        /// <summary>
        /// Finds a Client by his login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Client FindByLogin(string login)
        {

            Client clientProfile = null;

            DbSet<Client> clientProfiles = Context.Set<Client>();

            var result =
                (from c in clientProfiles
                 where c.login == login
                 select c);

            clientProfile = result.FirstOrDefault();


            if (clientProfile == null)
                throw new InstanceNotFoundException(login,
                    typeof(Client).FullName);

            return clientProfile;
        }


        public List<Card> FindCardsOfClient(Client client)
        {

            List<Card> cards = new List<Card>();
            foreach (Card card in client.Cards)
            {
                cards.Add(card);
            }

            return cards;
        }
    }
}
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao
{
    public interface IClientDao : IGenericDao<Client, Int64>
    {

        /// <summary>
        /// Finds a Client by login
        /// </summary>
        /// <param name="login">loginName</param>
        /// <returns>The Client</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Client FindByLogin(String login);

        /// <summary>
        /// Finds all card of a client
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>List of Cards</returns>
        List<Card> FindCardsOfClient(Client client);

    }
}

using System;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao
{
    public interface IClientDao : IGenericDao<Client, String>
    {

        /// <summary>
        /// Finds a Client by login
        /// </summary>
        /// <param name="login">loginName</param>
        /// <returns>The Client</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Client FindByLogin(String login);

    }
}

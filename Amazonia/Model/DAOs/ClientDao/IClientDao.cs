using System;
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

    }
}

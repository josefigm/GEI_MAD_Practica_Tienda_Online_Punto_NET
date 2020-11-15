using System;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{
    public interface IClientService
    {

        [Inject]
        IClientDao ClientDao { set; }


        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="login"> Login único e identificativo. </param>
        /// <param name="clearPassword"> Contraseña en claro. </param>
        /// <param name="clientDetails"> Detalles de cliente. </param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        void RegisterClient(String login, String clearPassword, 
            ClientDetails clientDetails);


        /// <summary>
        /// Actualiza los datos de un cliente ya existente.
        /// </summary>
        /// <param name="login"> The user profile id. </param>
        /// <param name="clientDetails"> The user profile details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserProfileDetails(String login,
            ClientDetails clientDetails);


    }
}

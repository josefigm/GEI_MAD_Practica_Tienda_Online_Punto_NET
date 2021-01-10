using System;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{
    public interface IClientService
    {

        [Inject]
        IClientDao ClientDao { set; }

        [Inject]
        ICardDao CardDao { set; }

        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="login"> Login único e identificativo. </param>
        /// <param name="clearPassword"> Contraseña en claro. </param>
        /// <param name="clientDetails"> Detalles de cliente. </param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        Client RegisterClient(String login, String clearPassword,
            ClientDTO clientDetails);


        /// <summary>
        /// Actualiza los datos de un cliente ya existente.
        /// </summary>
        /// <param name="id"> The user profile id. </param>
        /// <param name="clientDetails"> The client profile details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserProfileDetails(long id, ClientDTO clientDetails);

        /// <summary>
        /// Inicia sesión de un login determinado.
        /// </summary>
        /// <param name="login"> Name of the login. </param>
        /// <param name="password"> The password. </param>
        /// <param name="passwordIsEncrypted"> if set to <c> true </c> [password is encrypted]. </param>
        /// <returns> LoginResult </returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        LoginDTO Login(String login, String password,
            Boolean passwordIsEncrypted);

        /// <summary>
        /// Define por defecto una tarjeta para un usuario.
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void SetDefaultCard(String numberCard);

        /// <summary>
        /// Lista tarjetas de un cliente por su login.
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<CardDTO> ListCardsByClientId(long clientId);

        /// <summary>
        /// Recuperar tarjeta por defecto
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Card GetDefaultCard(long clientId);

        /// <summary>
        /// Recuperar ClientDTO
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ClientDTO GetClientDTO(long id);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="clientId"> The client id. </param>
        /// <param name="oldClearPassword"> The old clear password. </param>
        /// <param name="newClearPassword"> The new clear password. </param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        void ChangePassword(long clientId, String oldClearPassword,
            String newClearPassword);

    }
}

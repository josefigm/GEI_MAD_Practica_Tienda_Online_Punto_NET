using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Util;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{
    public class ClientServiceImp : IClientService
    {
        [Inject]
        public IClientDao ClientDao { private get; set; }

        [Inject]
        public ICardDao CardDao { private get; set; }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public Client RegisterClient(string login, string clearPassword,
            ClientDTO clientDetails)
        {
            try
            {
                ClientDao.FindByLogin(login);

                throw new DuplicateInstanceException(login,
                    typeof(Client).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                Client clientProfile = new Client();

                clientProfile.login = login;
                clientProfile.password = encryptedPassword;
                clientProfile.firstName = clientDetails.FirstName;
                clientProfile.lastName = clientDetails.LastName;
                clientProfile.address = clientDetails.Address;
                clientProfile.email = clientDetails.Email;
                clientProfile.role = clientDetails.Role;
                clientProfile.language = clientDetails.Language;

                ClientDao.Create(clientProfile);

                return clientProfile;

            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(string login, ClientDTO clientDetails)
        {
            Client client = ClientDao.FindByLogin(login);

            client.firstName = clientDetails.FirstName;
            client.lastName = clientDetails.LastName;
            client.address = clientDetails.Address;
            client.email = clientDetails.Email;
            client.role = clientDetails.Role;
            client.language = clientDetails.Language;

            ClientDao.Update(client);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginDTO Login(string login, string password, bool passwordIsEncrypted)
        {

            Client client =
                ClientDao.FindByLogin(login);

            String storedPassword = client.password;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(login);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(login);
                }
            }

            return new LoginDTO(client.id, client.login, client.firstName,
            client.password, client.role, client.address, client.language, false);
        }

        public void Logout(LoginDTO loginDetails)
        {
            if (!loginDetails.Exit)
            {
                LoginDTO.ExitLoginDetails(loginDetails);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void SetDefaultCard(string numberCard)
        {
            Card card = CardDao.FindByNumber(numberCard);
            card.defaultCard = true;

            CardDao.Update(card);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<Card> ListCardsByClientLogin(String login)
        {
            return ClientDao.FindCardsOfClient(ClientDao.FindByLogin(login));
        }

        public Card GetDefaultCard(string login)
        {

            // Recuperamos cliente por el login
            Client client = ClientDao.FindByLogin(login);

            // Recuperamos tarjeta del cliente con el booleano defaultCard a true
            List<Card> cardList = ClientDao.FindCardsOfClient(client);

            foreach(Card card in cardList)
            {
                if(card.defaultCard)
                {
                    return card;
                }
            }

            return null;
        }
    }
}

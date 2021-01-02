using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Util;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Exceptions;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp.DTOs;

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
                clientProfile.country = clientDetails.Country;

                ClientDao.Create(clientProfile);

                return clientProfile;

            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(long id, ClientDTO clientDetails)
        {
            Client client = ClientDao.Find(id);

            client.firstName = clientDetails.FirstName;
            client.lastName = clientDetails.LastName;
            client.address = clientDetails.Address;
            client.email = clientDetails.Email;
            client.role = clientDetails.Role;
            client.language = clientDetails.Language;
            client.country = clientDetails.Country;

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
            client.password, client.role, client.address, client.language, client.country);
        }


        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void SetDefaultCard(string numberCard)
        {

            // Recuperamos tarjeta
            Card card = CardDao.FindByNumber(numberCard);

            // Nos aseguramos de que no haya ninguna tarjeta por 
            // defecto del usuario
            Client client = card.Client;
            List<Card> clientCards = ClientDao.FindCardsOfClient(ClientDao.Find(client.id));
            foreach (Card cardItem in clientCards)
            {
                cardItem.defaultCard = false;
            }

            // Ponemos el boolean de tarjeta por defecto a true
            //  a la tarjeta que se quiere poner por defecto
            //Card card = CardDao.FindByNumber(numberCard);
            card.defaultCard = true;

            CardDao.Update(card);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<CardDTO> ListCardsByClientId(long clientId)
        {
            List<Card>  listCards = ClientDao.FindCardsOfClient(ClientDao.Find(clientId));
            List<CardDTO> listCardDTO = new List<CardDTO>();

            foreach (Card cardItem in listCards)
            {
                listCardDTO.Add(CardMapper.CardToCardDTO(cardItem));
            }

            return listCardDTO;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Card GetDefaultCard(long clientId)
        {

            // Recuperamos cliente por el id
            Client client = ClientDao.Find(clientId);
            if (client == null)
            {

            }

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

        public ClientDTO GetClientDTO(long id)
        {

            Client client = ClientDao.Find(id);

            return ClientMapper.ClientToClientDTO(client);

        }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public void ChangePassword(long clientId, string oldClearPassword, string newClearPassword)
        {

            Client userProfile = ClientDao.Find(clientId);
            String enStoredPassword = userProfile.password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 enStoredPassword))
            {
                throw new IncorrectPasswordException(userProfile.login);
            }

            userProfile.password = PasswordEncrypter.Crypt(newClearPassword);

            ClientDao.Update(userProfile);

        }
    }
}

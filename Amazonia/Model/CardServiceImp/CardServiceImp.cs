using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;


namespace Es.Udc.DotNet.Amazonia.Model.CardServiceImp
{
    public class CardServiceImp : ICardService
    {

        [Inject]
        public IClientDao ClientDao { private get; set; }

        [Inject]
        public ICardDao CardDao { private get; set; }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Card CreateCardToClient(CardDTO cardForm, long clientId)
        {
            // Recuperamos el cliente del login pasado
            Client relatedClient = ClientDao.Find(clientId);

            if (relatedClient == null)
            {
                throw new InstanceNotFoundException("No existe un cliente con login: " + clientId);
            }

            // Creamos tarjeta
            Card card = new Card();
            card.number = cardForm.Number;
            card.cvv = cardForm.CVV;
            card.expireDate = cardForm.ExpireDate;
            card.type = cardForm.Type;
            card.clientId = clientId;
            card.defaultCard = false;
                // Si es la unica, la ponemos por defecto
                List<Card> listCards = ClientDao.FindCardsOfClient(relatedClient);
                if (listCards.Count == 0)
                {
                    card.defaultCard = true;
                }
            CardDao.Create(card);

            // Modificamos cliente
            relatedClient.Cards.Add(card);

            ClientDao.Update(relatedClient);

            return card;
        }

        public void UpdateCardDetails(CardDTO cardDTO)
        {

            Card card = CardDao.FindByNumber(cardDTO.Number);

            card.cvv = cardDTO.CVV;
            card.expireDate = cardDTO.ExpireDate;
            card.type = cardDTO.Type;

            CardDao.Update(card);

        }
    }
}

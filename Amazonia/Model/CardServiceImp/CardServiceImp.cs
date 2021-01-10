using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.CardServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
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
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public Card CreateCardToClient(CardDTO cardForm, long clientId)
        {
            // Recuperamos el cliente del login pasado
            Client relatedClient = ClientDao.Find(clientId);

            if (relatedClient == null)
            {
                throw new InstanceNotFoundException("No existe un cliente con login: ", relatedClient.firstName);
            }

            try {

                // Comprobamos que el número de tarjeta no esté repetido
                Card cardRep = CardDao.FindByNumber(cardForm.Number);

                // Si no lanza excepcíón de que no la ha encontrado...
                throw new ModelUtil.Exceptions.DuplicateInstanceException(
                   "Ya existe tarjeta con ese número: ", cardForm.Number);


            } catch (InstanceNotFoundException)
            {
                // Al lanzar InstanceNotFoundException sabemos que no existe
                //   ninguna tarjeta por ese nombre

                // Creamos tarjeta
                Card card = new Card();
                card.number = cardForm.Number;
                card.cvv = cardForm.CVV;
                card.expireDate = cardForm.ExpireDate;

                if (cardForm.Type == "Credit Card" )
                {
                    card.type = false;
                }

                if (cardForm.Type == "Debit Card")
                {
                    card.type = true;
                }

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
        }

        /// <exception cref="InstanceNotFoundException"/>
        public CardDTO GetCardDTO(long cardId)
        {
            Card card = CardDao.Find(cardId);

            return CardMapper.CardToCardDTO(card);
        }

        public void UpdateCardDetails(CardDTO cardDTO)
        {

            Card card = CardDao.FindByNumber(cardDTO.Number);

            card.cvv = cardDTO.CVV;
            card.expireDate = cardDTO.ExpireDate;

            if (cardDTO.Type == "Credit Card")
            {
                card.type = false;
            }

            if (cardDTO.Type == "Debit Card")
            {
                card.type = true;
            }

            CardDao.Update(card);

        }
    }
}

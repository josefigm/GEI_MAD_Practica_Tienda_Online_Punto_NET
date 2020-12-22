using System;
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
        public Card CreateCardToClient(CardDTO cardForm, string login)
        {
            // Recuperamos el cliente del login pasado
            Client relatedClient = ClientDao.FindByLogin(login);

            if (relatedClient == null)
            {
                throw new InstanceNotFoundException("No existe un cliente con login: " + login);
            }

            // Creamos tarjeta
            Card card = new Card();
            card.number = cardForm.Number;
            card.cvv = cardForm.CVV;
            card.expireDate = cardForm.ExpireDate;
            card.type = cardForm.Type;
            card.clientId = relatedClient.id;
            card.defaultCard = cardForm.DefaultCard;
            CardDao.Create(card);

            // Modificamos cliente
            relatedClient.Cards.Add(card);

            //card.Client = relatedClient;

            //CardDao.Update(card);
            ClientDao.Update(relatedClient);

            return card;
        }
    }
}

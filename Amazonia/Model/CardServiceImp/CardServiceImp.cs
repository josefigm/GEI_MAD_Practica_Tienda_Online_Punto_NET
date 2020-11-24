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
        public Card CreateCardToClient(Card card, string login)
        {

            // Comprobamos que tanto tarjeta como usuario del login pasado
            //    no sean nulos

            if (card == null)
            {
                throw new ArgumentNullException("Card Nulo");
            }

            //++++++++ Cambiei pa que non petara, revisar
            if (ClientDao.FindByLogin(login) == null)
            {
                throw new InstanceNotFoundException("No existe un cliente con login: " + login);
            }

            // Cliente a añadir tarjeta
            Client relatedClient = ClientDao.FindByLogin(login);

            if (!CardDao.Exists(card.id))
            {
                CardDao.Create(card);
            }
            //+++++++++++++++++++++++++++++++++++++++++
            //card.Clients1.Add(relatedClient);
            relatedClient.Cards.Add(card);

            ClientDao.Update(relatedClient);
            CardDao.Update(card);

            return card;
        }


    }
}

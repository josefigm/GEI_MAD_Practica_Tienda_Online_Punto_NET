using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CardDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.CardServiceImp
{
    public interface ICardService
    {

        [Inject]
        ICardDao CardDao { set; }

        /// <summary>
        /// Crear tarjeta y asignar a un cliente.
        /// </summary>
        /// <param name="cardNumber">The cardNumber.</param>
        /// <param name="login">The comment identifier.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns></returns>
        [Transactional]
        Card CreateCardToClient(CardDTO cardForm, string login);
    }
}

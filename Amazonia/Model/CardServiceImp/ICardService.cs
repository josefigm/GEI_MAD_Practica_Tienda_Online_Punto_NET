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
        /// <param name="cardDTO">The card information.</param>
        /// <param name="id">The client identifier.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        /// <returns></returns>
        [Transactional]
        Card CreateCardToClient(CardDTO cardForm, long id);

        /// <summary>
        /// Actualiza los datos de una tarjeta ya existente.
        /// </summary>
        /// <param name="id"> The card id. </param>
        /// <param name="cardDTO"> The user profile details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateCardDetails(CardDTO cardDTO);

        /// <summary>
        /// Recuperar CardDTO
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        CardDTO GetCardDTO(long cardId);




    }
}

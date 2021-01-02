using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.CardServiceImp.DTOs
{
    class CardMapper
    {

        public static CardDTO CardToCardDTO(Card input)
        {
            return new CardDTO(input.number, input.cvv, input.expireDate, input.type, input.defaultCard);
        }


    }
}

using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.CardServiceImp
{

    /// <summary>
    /// VO Class which contains the card details
    /// </summary>
    [Serializable()]
    public class CardDTO
    {
        #region Properties Region

        public String Number { get; private set; }

        public String CVV { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public Boolean Type { get; private set; }

        public Boolean DefaultCard { get; private set; }

        #endregion

        /// <summary>
        /// (DEFAULT CARD) Initializes whit language a new instance of the <see cref="CardForm"/>
        /// class.
        /// </summary>
        /// <param name="number">El número de la tarjeta.</param>
        /// <param name="cVV">El cvv de la tarjeta.</param>
        /// <param name="expireDate">La fecha en la que expirará la tarjeta.</param>
        /// <param name="type">Tipo de tarjeta.</param>
        /// <param name="defaultCard">Tarjeta por defecto.</param>
        /// 
        public CardDTO(string number, string cVV, DateTime expireDate, bool type, bool defaultCard)
        {
            this.Number = number;
            this.CVV = cVV;
            this.ExpireDate = expireDate;
            this.Type = type;
            this.DefaultCard = defaultCard;
        }

        /// <summary>
        /// (NO DEFAULT CARD) Initializes whit language a new instance of the <see cref="CardForm"/>
        /// class.
        /// </summary>
        /// <param name="number">El número de la tarjeta.</param>
        /// <param name="cVV">El cvv de la tarjeta.</param>
        /// <param name="expireDate">La fecha en la que expirará la tarjeta.</param>
        /// <param name="type">Tipo de tarjeta.</param>
        /// 
        public CardDTO(string number, string cVV, DateTime expireDate, bool type)
        {
            this.Number = number;
            this.CVV = cVV;
            this.ExpireDate = expireDate;
            this.Type = type;
            this.DefaultCard = false;
        }

        public override bool Equals(object obj)
        {
            var form = obj as CardDTO;
            return form != null &&
                   Number == form.Number &&
                   CVV == form.CVV &&
                   ExpireDate == form.ExpireDate &&
                   Type == form.Type &&
                   DefaultCard == form.DefaultCard;
        }

        public override int GetHashCode()
        {
            var hashCode = 2095145509;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CVV);
            hashCode = hashCode * -1521134295 + ExpireDate.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + DefaultCard.GetHashCode();
            return hashCode;
        }
    }
}

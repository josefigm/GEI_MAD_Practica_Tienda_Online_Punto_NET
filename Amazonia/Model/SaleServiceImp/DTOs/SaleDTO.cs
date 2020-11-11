using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs
{
    public class SaleDTO
    {
        public long id { get; set; }
        public System.DateTime date { get; set; }
        public string address { get; set; }
        public double totalPrice { get; set; }
        public string cardNumber { get; set; }
        public string clientLogin { get; set; }
        public List<SaleLineDTO> saleLines  { get; set; }

        public SaleDTO() { }

        public SaleDTO(long id, DateTime date, string address, double totalPrice, 
            string cardNumber, string clientLogin, List<SaleLineDTO> saleLines)
        {
            this.id = id;
            this.date = date;
            this.address = address;
            this.totalPrice = totalPrice;
            this.cardNumber = cardNumber;
            this.clientLogin = clientLogin;
            this.saleLines = saleLines;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            SaleDTO target = obj as SaleDTO;
            return target != null
                   && id == target.id
                   && date == target.date
                   && address == target.address
                   && totalPrice == target.totalPrice
                   &&  cardNumber == target.cardNumber
                   && clientLogin == target.clientLogin 
                   && saleLines.Equals(target.saleLines);
        }
    }
}

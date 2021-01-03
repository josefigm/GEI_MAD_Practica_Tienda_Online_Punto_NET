using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using Es.Udc.DotNet.Amazonia.Model.SaleServiceImp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Es.Udc.DotNet.Amazonia.Web.HTTP.Session
{
    public class UserSession
    {
        private long userProfileId;
        private String firstName;
        private byte role;
        private String address;
        private CardDTO defaultCard;

        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public byte Role
        {
            get { return role; }
            set { role = value; }
        }

        public String Address
        {
            get { return address; }
            set { address = value; }
        }

        public CardDTO DefaultCard
        {
            get { return defaultCard; }
            set { defaultCard = value; }
        }
    }
}
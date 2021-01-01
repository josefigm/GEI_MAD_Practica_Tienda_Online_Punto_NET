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
    }
}
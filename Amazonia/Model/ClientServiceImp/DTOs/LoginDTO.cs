using Es.Udc.DotNet.Amazonia.Model.CardServiceImp;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{

    /// <summary>
    /// A Custom VO which keeps the details for a login action.
    /// </summary>
    [Serializable()]
    public class LoginDTO
    {

        #region Properties Region        
        /// <summary>
        /// Gets the user profile identifier.
        /// </summary>
        /// <value>
        /// The user profile identifier.
        /// </value>
        public long UserProfileId { get; private set; }

        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <value>The login.</value>
        public string Login { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The <c>firstName</c></value>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the encrypted password.
        /// </summary>
        /// <value>The <c>encryptedPassword.</c></value>
        public string EncryptedPassword { get; private set; }


        /// <summary>
        /// Gets the role code.
        /// </summary>
        /// <value>The role.</value>
        public byte Role { get; private set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the Language code.
        /// </summary>
        /// <value>The user profile id.</value>
        public string Language { get; private set; }

        /// <summary>
        /// Gets the client country
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the default client card
        /// </summary>
        /// <value>The card.</value>
        public CardDTO DefaultCard { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginDetails"/> class.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="login">The user login.</param>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="role">The role of user.</param>
        /// <param name="address">User postal address.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="defaultCard">User default card.</param>
        public LoginDTO(long userProfileId, string login, string firstName,
            string encryptedPassword, byte role, string address, string language,
            string country, CardDTO defaultCard)
        {
            UserProfileId = userProfileId;
            Login = login;
            FirstName = firstName;
            EncryptedPassword = encryptedPassword;
            Role = role;
            Address = address;
            Language = language;
            Country = country;
            this.DefaultCard = defaultCard;
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as LoginDTO;
            return dTO != null &&
                   UserProfileId == dTO.UserProfileId &&
                   Login == dTO.Login &&
                   FirstName == dTO.FirstName &&
                   EncryptedPassword == dTO.EncryptedPassword &&
                   Role == dTO.Role &&
                   Address == dTO.Address &&
                   Language == dTO.Language &&
                   Country == dTO.Country;
        }

        public override int GetHashCode()
        {
            var hashCode = -1359125452;
            hashCode = hashCode * -1521134295 + UserProfileId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Login);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EncryptedPassword);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            return hashCode;
        }
    }
}

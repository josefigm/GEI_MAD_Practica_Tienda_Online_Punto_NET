using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{

    /// <summary>
    /// A Custom VO which keeps the details for a login action.
    /// </summary>
    [Serializable()]
    public class LoginDetails
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginDetails"/> class.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="firstName">Users's first name.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="language">The language.</param>
        /// <param name="country">The country.</param>
        /// <param name="exit">The state of Login Details.</param>
        public LoginDetails(String login, String firstName,
            String encryptedPassword, byte role, String address, byte language, Boolean exit)
        {
            this.Login = login;
            this.FirstName = firstName;
            this.EncryptedPassword = encryptedPassword;
            this.Role = role;
            this.Address = address;
            this.Language = language;
            this.Exit = exit;
        }


        #region Properties Region

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
        public byte Language { get; private set; }

        /// <summary>
        /// Especify if user is exit.
        /// </summary>
        /// <value>The user profile id.</value>
        public bool Exit { get; private set; }

        #endregion Properties Region

        public static void ExitLoginDetails(LoginDetails loginDetails)
        {
            loginDetails.Exit = true;
        }

        public override bool Equals(object obj)
        {
            var details = obj as LoginDetails;
            return details != null &&
                   Login == details.Login &&
                   FirstName == details.FirstName &&
                   EncryptedPassword == details.EncryptedPassword &&
                   Role == details.Role &&
                   Address == details.Address &&
                   Language == details.Language &&
                   Exit == details.Exit;
        }

        public override int GetHashCode()
        {
            var hashCode = -1742444342;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Login);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EncryptedPassword);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + Language.GetHashCode();
            hashCode = hashCode * -1521134295 + Exit.GetHashCode();
            return hashCode;
        }




        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strLoginResult;

            strLoginResult =
                "[ login = " + Login + " | " +
                "firstName = " + FirstName + " | " +
                "encryptedPassword = " + EncryptedPassword + " | " +
                "role = " + Role + " | " +
                "address = " + Address + " | " +
                "language = " + Language + " | " +
                "exit = " + Exit + " ]";

            return strLoginResult;
        }
    }
}

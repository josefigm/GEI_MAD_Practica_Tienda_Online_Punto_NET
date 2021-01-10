using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{


    /// <summary>
    /// VO Class which contains the client details
    /// </summary>
    [Serializable()]
    public class ClientDTO
    {

        #region Properties Region

        public String FirstName { get; private set; }

        public String LastName { get; private set; }

        public String Address { get; private set; }

        public String Email { get; private set; }

        public byte Role { get; private set; }

        public String Language { get; private set; }

        public String Country { get; private set; }



        #endregion

        /// <summary>
        /// Initializes whit language a new instance of the <see cref="ClientDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">El Nombre del cliente.</param>
        /// <param name="lastName">El Apellido del cliente.</param>
        /// <param name="address">La dirección del cliente.</param>
        /// <param name="email">El email del cliente.</param>
        /// <param name="role">El rol del cliente.</param>
        /// <param name="language"> El lenguaje del cliente.</param>
        /// <param name="country"> El país del cliente.</param>
        /// 
        public ClientDTO(String firstName, String lastName, String address, String email, byte role, string language, string country)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Email = email;
            this.Role = role;
            this.Language = language;
            this.Country = country;
        }

        /// <summary>
        /// Initializes whit language a new instance of the <see cref="ClientDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">El Nombre del cliente.</param>
        /// <param name="lastName">El Apellido del cliente.</param>
        /// <param name="address">La dirección del cliente.</param>
        /// <param name="email">El email del cliente.</param>
        /// <param name="language"> El lenguaje del cliente.</param>
        /// <param name="country"> El país del cliente.</param>
        /// 
        public ClientDTO(String firstName, String lastName, String address, String email, string language, string country)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Email = email;
            this.Role = 0;
            this.Language = language;
            this.Country = country;
        }



        /// <summary>
        /// Initializes whitout language a new instance of the <see cref="ClientDetails"/>
        /// class.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="address">The user's address.</param>
        /// <param name="email">The user's email.</param>
        /// <param name="role">The user's role.</param>
        /// 
        public ClientDTO(String firstName, String lastName, String address, String email, byte role)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Email = email;
            this.Role = role;
            this.Language = "en";
            this.Country = "en";
        }

        public override bool Equals(object obj)
        {
            var dTO = obj as ClientDTO;
            return dTO != null &&
                   FirstName == dTO.FirstName &&
                   LastName == dTO.LastName &&
                   Address == dTO.Address &&
                   Email == dTO.Email &&
                   Role == dTO.Role &&
                   Language == dTO.Language &&
                   Country == dTO.Country;
        }

        public override int GetHashCode()
        {
            var hashCode = -1217607253;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            return hashCode;
        }

    }
}

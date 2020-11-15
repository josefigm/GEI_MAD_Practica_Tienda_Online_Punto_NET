using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.Util;

using Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp
{
    public class ClientServiceImp : IClientService
    {
        [Inject]
        public IClientDao ClientDao { private get; set; }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public void RegisterClient(string login, string clearPassword,
            ClientDetails clientDetails)
        {
            try
            {
                ClientDao.FindByLogin(login);

                throw new DuplicateInstanceException(login,
                    typeof(Client).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                Client clientProfile = new Client();

                clientProfile.login = login;
                clientProfile.password = encryptedPassword;
                clientProfile.firstName = clientDetails.FirstName;
                clientProfile.lastName = clientDetails.LastName;
                clientProfile.address = clientDetails.Address;
                clientProfile.email = clientDetails.Email;
                clientProfile.role = clientDetails.Role;
                clientProfile.language = clientDetails.Language;


                ClientDao.Create(clientProfile);

            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(string login, ClientDetails clientDetails)
        {

            Client client = ClientDao.FindByLogin(login);


            client.firstName = clientDetails.FirstName;
            client.lastName = clientDetails.LastName;
            client.address = clientDetails.Address;
            client.email = clientDetails.Email;
            client.role = clientDetails.Role;

            // Se cambia sólo si el valor de clientDetails enviado no es el valor por defecto
            if (!(clientDetails.Language == 0))
            {
                client.language = clientDetails.Language;
            }

            ClientDao.Update(client);
        }
    }
}

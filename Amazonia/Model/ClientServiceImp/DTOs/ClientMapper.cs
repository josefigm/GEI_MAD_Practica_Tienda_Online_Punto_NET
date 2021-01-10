using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.ClientServiceImp.DTOs
{
    class ClientMapper
    {

        public static ClientDTO ClientToClientDTO(Client input)
        {
            return new ClientDTO(input.firstName, input.lastName, input.address, input.email, input.role, input.language, input.country);
        }

    }
}

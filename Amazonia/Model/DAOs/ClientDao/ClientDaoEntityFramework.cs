using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.ClientDao
{
    public class ClientDaoEntityFramework :
        GenericDaoEntityFramework<Client, String>, IClientDao
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions
{
    [Serializable]
    public class AlreadyCommentedThisProduct : Exception
    {
         public AlreadyCommentedThisProduct() 
            : base("You can not comment a product you have already commented.")
         {
         }   
    }
}


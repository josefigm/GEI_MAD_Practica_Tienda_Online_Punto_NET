using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions
{
    [Serializable]
    public class NotAllowedToDeleteComment : Exception
    {
        public NotAllowedToDeleteComment()
            : base("You can not delete a comment of another user")
        {
        }
    }
}
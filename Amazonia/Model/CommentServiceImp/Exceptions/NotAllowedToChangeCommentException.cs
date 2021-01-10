using System;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions
{
    [Serializable]
    public class NotAllowedToChangeCommentException : Exception
    {
        
        public NotAllowedToChangeCommentException()
            : base("You can not change a comment of another user")
        {
        }
    }
}

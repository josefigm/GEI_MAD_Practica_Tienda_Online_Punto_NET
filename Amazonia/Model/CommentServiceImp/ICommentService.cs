using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }
        Comment AddComment(string title, string value, long productId);
        
        // Optional method
        List<Comment> FindCommentsOfProduct(long productId);
    }
}

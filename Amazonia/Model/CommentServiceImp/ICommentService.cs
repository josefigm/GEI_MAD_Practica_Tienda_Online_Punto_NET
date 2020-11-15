using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public interface ICommentService
    {
        Comment AddComment(string title, string value, long productId);
        
        // Optional method
        List<Comment> FindCommentsOfProduct(long productId);
    }
}

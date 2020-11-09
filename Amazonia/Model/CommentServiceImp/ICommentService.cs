using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public interface ICommentService
    {
        void AddComment(string title, string value, long productId);
        List<Comment> FindCommentsOfProduct(long productId);
    }
}

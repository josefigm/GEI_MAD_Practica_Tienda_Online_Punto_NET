using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Ninject;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public interface ICommentService
    {
        void addComment(string title, string value, long productId);
    }
}

using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Ninject;
using System.Collections.Generic;


namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public class CommentServiceImp : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        public Comment AddComment(string title, string value, long productId)
        {
            Comment newComment = new Comment();
            newComment.title = title;
            newComment.value = value;
            newComment.productId = productId;
            CommentDao.Create(newComment);

            return newComment;
        }

        // Optional method
        public List<Comment> FindCommentsOfProduct(long productId)
        {
            List<Comment> result = new List<Comment>();

            result = CommentDao.FindCommentsOfProduct(productId);
            return result;
        }
    }
}

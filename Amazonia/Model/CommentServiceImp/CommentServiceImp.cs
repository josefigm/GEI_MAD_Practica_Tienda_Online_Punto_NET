using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    class CommentServiceImp : ICommentService
    {
        [Inject]
        public ICommentDao commentDao;

        public void AddComment(string title, string value, long productId)
        {
            if (title == null || value == null || productId == null)
            {
                return;
            }

            Comment newComment = new Comment();
            newComment.title = title;
            newComment.value = value;
            newComment.productId = productId;

            commentDao.Create(newComment);
        }

        public List<Comment> FindCommentsOfProduct(long productId)
        {
            List<Comment> result = new List<Comment>;

            if (productId == null)
            {
                return result;
            }

            result = commentDao.FindCommentsOfProduct(productId);
            return result;
        }
    }
}

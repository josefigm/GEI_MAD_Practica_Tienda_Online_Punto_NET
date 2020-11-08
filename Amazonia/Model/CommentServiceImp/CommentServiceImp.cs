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

        public void addComment(string title, string value, long productId)
        {
            Comment newComment = new Comment();
            newComment.title = title;
            newComment.value = value;
            newComment.productId = productId;

            commentDao.Create(newComment);
        }
    }
}

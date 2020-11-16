using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Ninject;
using System;
using System.Management.Instrumentation;
using System.Collections.Generic;


namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public class CommentServiceImp : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get;  set; }

        public Comment AddComment(string title, string value, long productId)
        {
            if(title == null || value == null)
            {
                throw new ArgumentNullException("Se ha pasado argumentos nulos");
            }

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
            if (ProductDao.Find(productId) == null)
            {
                throw new InstanceNotFoundException("No existe un producto con id: " + productId);
            }
            List<Comment> result = new List<Comment>();

            result = CommentDao.FindCommentsOfProduct(productId);
            return result;
        }
    }
}

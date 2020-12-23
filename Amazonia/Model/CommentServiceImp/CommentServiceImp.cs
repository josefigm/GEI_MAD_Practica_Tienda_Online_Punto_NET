using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Ninject;
using System;
using System.Management.Instrumentation;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public class CommentServiceImp : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get;  set; }

        [Inject]
        public ILabelDao LabelDao { private get; set; }

        public Comment AddComment(string title, string value, long productId, long clientId)
        {
            List<Comment> commentsOfThisUserAndProduct = new List<Comment>();

            if(title == null || value == null)
            {
                throw new ArgumentNullException("Se ha pasado argumentos nulos");
            }

            commentsOfThisUserAndProduct = CommentDao.FindCommentsOfProductAndClient(productId, clientId);

            // An user should not be able to comment more than one time the same product
            if (commentsOfThisUserAndProduct.Count != 0)
            {
                throw new AlreadyCommentedThisProduct();
            }


            // Buscar comentarios de ese producto y ese cliente

            Comment newComment = new Comment();
            newComment.title = title;
            newComment.value = value;
            newComment.productId = productId;
            newComment.clientId = clientId;
            CommentDao.Create(newComment);

            return newComment;
        }






        public List<Comment> FindCommentsByLabel(long labelId)
        {

            // etiqueta : label de la que vamos a buscar sus comentarios
            Label etiqueta = LabelDao.Find(labelId);

            if (etiqueta == null)
            {
                throw new InstanceNotFoundException("No existe " +
                    "una etiqueta con id: " + labelId);
            }

            List<Comment> result = new List<Comment>();
            result = CommentDao.FindCommentsByLabel(etiqueta);
            return result;

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

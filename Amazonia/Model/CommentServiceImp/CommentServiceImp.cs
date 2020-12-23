using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Ninject;
using System;
using System.Management.Instrumentation;
using System.Collections.Generic;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;

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

        private List<LabelDTO> toLabelDTOList(ICollection<Label> labels)
        {
            List<LabelDTO> labelDTOs = new List<LabelDTO>();

            foreach (Label label in labels)
            {
                labelDTOs.Add(new LabelDTO(label.id, label.value));
            }

            return labelDTOs;
        }

        // Optional method
        public List<CommentDTO> FindCommentsOfProduct(long productId)
        {
            if (ProductDao.Find(productId) == null)
            {
                throw new InstanceNotFoundException("No existe un producto con id: " + productId);
            }
            List<Comment> result = new List<Comment>();
            List<CommentDTO> comments = new List<CommentDTO>();
            Comment comment;

            result = CommentDao.FindCommentsOfProduct(productId);

            for (int i = 0; i < result.Count; i++)
            {
                comment = result[i];

                comments.Add(
                    new CommentDTO(comment.id, comment.title, comment.value, comment.date, comment.productId,
                    comment.clientId, comment.Client.login, toLabelDTOList(comment.Labels))
                    );
            }

            return comments;
        }
    }
}

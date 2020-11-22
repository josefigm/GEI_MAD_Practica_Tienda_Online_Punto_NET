using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Ninject;
using System.Collections.Generic;
using System;
using System.Management.Instrumentation;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public class LabelServiceImp : ILabelService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

        public Label CreateLabel(string value, long commentId)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Valor de etiqueta nulo");
            }
            if (CommentDao.Find(commentId) == null)
            {
                throw new InstanceNotFoundException("No existe un comentario con id: " + commentId);
            }
            Label newLabel = new Label();
            newLabel.value = value;

            Comment relatedComment = CommentDao.Find(commentId);
            newLabel.Comments.Add(relatedComment);

            LabelDao.Create(newLabel);

            return newLabel;
        }

        public void DeleteLabel(long labelId)
        {
            LabelDao.Remove(labelId);
        }

        public List<Label> FindAllLabels()
        {
            return LabelDao.GetAllElements();
        }

        public List<Label> FindLabelsByComment(long commentId)
        {
            if (CommentDao.Find(commentId) == null)
            {
                throw new InstanceNotFoundException("No existe un comentario con id: " + commentId);
            }

            Comment relatedComment = CommentDao.Find(commentId);
            List<Label> result = new List<Label>();
            result = LabelDao.FindLabelsOfComment(relatedComment);
            return result;
        }
    }
}

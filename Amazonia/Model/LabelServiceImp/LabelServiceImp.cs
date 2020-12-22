using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Ninject;
using System.Collections.Generic;
using System;
using System.Management.Instrumentation;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;

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
            if (value == null || value.Length == 0)
            {
                throw new ArgumentException("Valor de etiqueta no valido");
            }
            Comment relatedComment = CommentDao.Find(commentId);
            if (relatedComment == null)
            {
                throw new InstanceNotFoundException("No existe un comentario con id: " + commentId);
            }

            List<Label> existentLabels = LabelDao.GetAllElements();

            foreach (Label label in existentLabels)
            {
                if (label.value.Equals(value))
                {
                    throw new ArgumentException("Se intenta añadir un label que ya existe");
                }
            }

            Label newLabel = new Label();
            newLabel.value = value;
            newLabel.Comments.Add(relatedComment);

            LabelDao.Create(newLabel);

            return newLabel;
        }

        public void AssignLabelsToComment(long commentId, List<long> labelIds)
        {
            if (labelIds == null || labelIds.Count == 0)
            {
                throw new ArgumentException("Lista de etiquetas de entrada nula");
            }
            Comment relatedComment = CommentDao.Find(commentId);
            if (relatedComment == null)
            {
                throw new InstanceNotFoundException("No existe un comentario con id: " + commentId);
            }

            foreach(long labelId in labelIds)
            {
                Label labelToProcess = LabelDao.Find(labelId);
                if (labelToProcess == null)
                {
                    throw new InstanceNotFoundException("No existe una etiqueta con id: " + labelId);
                }

                labelToProcess.Comments.Add(relatedComment);
                LabelDao.Update(labelToProcess);
            }
        }

        public void UpdateLabel(long labelId, string newValue)
        {
            Label labelToProcess = LabelDao.Find(labelId);
            if (labelToProcess == null)
            {
                throw new InstanceNotFoundException("No existe una etiqueta con id: " + labelId);
            }
            labelToProcess.value = newValue;
            LabelDao.Update(labelToProcess);
        }

        public void DeleteLabel(long labelId)
        {
            LabelDao.Remove(labelId);
        }


        public void DeleteLabelsFromComment(long commentId, List<long> labelIds)
        {
            if (labelIds == null || labelIds.Count == 0)
            {
                throw new ArgumentException("Lista de etiquetas de entrada nula");
            }
            Comment relatedComment = CommentDao.Find(commentId);
            if (relatedComment == null)
            {
                throw new InstanceNotFoundException("No existe un comentario con id: " + commentId);
            }

            foreach (long labelId in labelIds)
            {
                Label labelToProcess = LabelDao.Find(labelId);
                if (labelToProcess == null)
                {
                    throw new InstanceNotFoundException("No existe una etiqueta con id: " + labelId);
                }

                labelToProcess.Comments.Remove(relatedComment);
                LabelDao.Update(labelToProcess);
            }
        }

        public List<Label> FindAllLabels()
        {
            return LabelDao.GetAllElements();
        }

        public List<Label> FindLabelsByComment(long commentId)
        {
            List<Label> result = new List<Label>();
            result = LabelDao.FindLabelsOfComment(commentId);
            return result;
        }

        public List<int> GetNumberOfComments(List<long> labels)
        {
            List<int> commentsForLabels = new List<int>();
            Label label;

            for (int i = 0; i < labels.Count; i++)
            {
                label = LabelDao.Find(labels[i]);

                commentsForLabels.Add(label.Comments.Count);
            }

            return commentsForLabels;
        }

        public List<LabelDTO> FindMostUsedLabels(int limit)
        {
            List<LabelDTO> result = LabelDao.FindMostUsedLabels();
            
            // We return only the first limit labels
            return result.GetRange(0, limit);
        }
    }
}

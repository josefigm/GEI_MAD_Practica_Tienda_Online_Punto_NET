using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using Ninject;
using System.Collections.Generic;
using System;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using System.Management.Instrumentation;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public class LabelServiceImp : ILabelService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }

        public LabelDTO CreateLabel(string value)
        {
            if (value == null || value.Length == 0)
            {
                throw new ArgumentException("Valor de etiqueta no valido");
            }

            List<Label> existentLabels = LabelDao.GetAllElements();

            foreach (Label label in existentLabels)
            {
                if (label.value.Equals(value))
                {
                    throw new ModelUtil.Exceptions.DuplicateInstanceException("Se intenta añadir un label que ya existe", "Label");
                }
            }

            Label newLabel = new Label();
            newLabel.value = value;

            LabelDao.Create(newLabel);

            return LabelMapper.toLabelDTO(newLabel);
        }

        public void AssignLabelsToComment(long commentId, List<long> labelIds)
        {
            if (!CommentDao.Exists(commentId))
            {
                throw new InstanceNotFoundException("No existe un comentario con id: " + commentId);
            }

            List<Label> newLabels = new List<Label>();
            foreach (long labelId in labelIds)
            {
                Label labelToProcess = LabelDao.Find(labelId);
                if (labelToProcess == null)
                {
                    throw new InstanceNotFoundException("No existe una etiqueta con id: " + labelId);
                }

                newLabels.Add(labelToProcess);
            }

            CommentDao.ReplaceLabels(commentId, newLabels);
        }

        public List<LabelDTO> FindAllLabels()
        {
            List<Label> labels = LabelDao.GetAllElements();
            labels.Reverse();
            return LabelMapper.toLabelDTOList(labels);
        }

        public List<LabelDTO> FindLabelsByComment(long commentId)
        {
            List<Label> result = new List<Label>();
            result = LabelDao.FindLabelsOfComment(commentId);
            return LabelMapper.toLabelDTOList(result);
        }

        public List<int> GetNumberOfComments(List<long> labels)
        {
            List<int> commentsForLabels = new List<int>();

            for (int i = 0; i < labels.Count; i++)
            {
                commentsForLabels.Add(LabelDao.GetNumberOfCommentsForLabel(labels[i]));
            }

            return commentsForLabels;
        }

        public List<LabelDTO> FindMostUsedLabels(int limit)
        {
            List<LabelDTO> result = LabelDao.FindMostUsedLabels(limit);

            return result;
        }
    }
}

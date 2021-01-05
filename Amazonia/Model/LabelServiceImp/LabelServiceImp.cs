﻿using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
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

        public void CreateLabel(string value)
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
        }

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
            List<LabelDTO> result = LabelDao.FindMostUsedLabels();
            
            // We return only the first limit labels
            return result.GetRange(0, limit);
        }
    }
}

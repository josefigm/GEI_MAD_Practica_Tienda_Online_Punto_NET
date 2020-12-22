using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using System;
using System.Management.Instrumentation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public interface ILabelService
    {
        ICommentDao CommentDao { set; }
        ILabelDao LabelDao { set; }

        /// <summary>
        /// Creates the label and associates the label to a comment since a label with no
        /// comment has no reason to exist.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns></returns>
        [Transactional]
        Label CreateLabel(string value, long commentId);

        /// <summary>
        /// Assigns the labels to an specific comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="labelIds">The label ids.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ArgumentException"/>
        [Transactional]
        void AssignLabelsToComment(long commentId, List<long> labelIds);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateLabel(long labelId, string newValue);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        [Transactional]
        void DeleteLabel(long labelId);

        /// <summary>
        /// Deletes the labels from a certain comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="labelIds">The label ids.</param>
        /// <exception cref="InstanceNotFoundException"/>
        void DeleteLabelsFromComment(long commentId, List<long> labelIds);

        /// <summary>
        /// Finds all labels.
        /// </summary>
        /// <returns></returns>
        [Transactional]
        List<Label> FindAllLabels();

        /// <summary>
        /// Finds the labels by comment.
        /// </summary>
        /// <param name="commendId">The commend identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns></returns>
        [Transactional]
        List<Label> FindLabelsByComment(long commendId);

        [Transactional]
        List<int> GetNumberOfComments(List<long> labels);

        /// <summary>
        /// Finds the most used labels.
        /// </summary>
        /// <param name="limit"> Specifies the number of labels to retrieve.</param>
        /// <returns> The list of the most used labels (containing limit labels)</returns>
        [Transactional]
        List<LabelDTO> FindMostUsedLabels(int limit);
    }
}

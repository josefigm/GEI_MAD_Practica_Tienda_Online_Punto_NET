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
        /// Creates the label
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="DuplicateInstanceException"/>
        /// <exception cref="ArgumentException"/>
        /// <returns></returns>
        [Transactional]
        LabelDTO CreateLabel(string value);

        /// <summary>
        /// Assigns the labels to an specific comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="labelIds">The label ids.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void AssignLabelsToComment(long commentId, List<long> labelIds);

        /// <summary>
        /// Finds all labels.
        /// </summary>
        /// <returns></returns>
        [Transactional]
        List<LabelDTO> FindAllLabels();

        /// <summary>
        /// Finds the labels by comment.
        /// </summary>
        /// <param name="commendId">The commend identifier.</param>
        /// <returns></returns>
        [Transactional]
        List<LabelDTO> FindLabelsByComment(long commentId);

        /// <summary>Gets the number of comments.</summary>
        /// <param name="labels">The labels.</param>
        /// <returns>A list with the number of comments for each label</returns>
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

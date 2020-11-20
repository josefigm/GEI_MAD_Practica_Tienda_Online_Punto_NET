using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao;
using System;
using System.Management.Instrumentation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Amazonia.Model.LabelServiceImp
{
    public interface ILabelService
    {
        ICommentDao CommentDao { set; }
        ILabelDao LabelDao { set; }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns></returns>
        [Transactional]
        Label CreateLabel(string value, long commentId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        [Transactional]
        void DeleteLabel(long labelId);

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
    }
}

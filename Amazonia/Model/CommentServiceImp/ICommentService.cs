using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using System.Collections.Generic;
using System;
using System.Management.Instrumentation;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.Amazonia.Model.CommentServiceImp
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }
        IProductDao ProductDao { set; }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="value">The value.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="clientId">The user's login.</param>param>
        /// <exception cref="ArgumentNullException"/>

        /// <returns></returns>
        [Transactional]
        Comment AddComment(string title, string value, long productId, long clientId);

        /// <summary>
        /// Finds the comments of product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <exception cref="InstanceNotFoundException"/> 
        /// <exception cref="AlreadyCommentedThisProduct"/>
        /// <returns></returns>
        [Transactional]
        List<Comment> FindCommentsOfProduct(long productId);



        /// <summary>
        /// Finds the comments by label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <exception cref="InstanceNotFoundException"/> 
        /// <returns></returns>
        [Transactional]
        List<Comment> FindCommentsByLabel(long labelId);

    }
}

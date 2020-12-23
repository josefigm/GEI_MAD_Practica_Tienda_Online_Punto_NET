using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.DTOs;
using Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao;
using Es.Udc.DotNet.Amazonia.Model.DAOs.ProductDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using Es.Udc.DotNet.Amazonia.Model.CommentServiceImp.Exceptions;

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
        /// <exception cref="AlreadyCommentedThisProduct"/>
        /// <returns></returns>
        [Transactional]
        Comment AddComment(string title, string value, long productId, long clientId);

        /// <summary>
        /// Removes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotAllowedToDeleteComment"/>
        [Transactional]
        void RemoveComment(long commentId, long clientId);

        /// <summary>
        /// Finds the comments of product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="AlreadyCommentedThisProduct"/>
        /// <returns></returns>
        [Transactional]
        List<CommentDTO> FindCommentsOfProduct(long productId);


        /// <summary>
        /// Finds the comments by label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <returns></returns>
        [Transactional]
        List<Comment> FindCommentsByLabel(long labelId);

        /// <summary>
        /// Change the value and title of a comment
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="title">New title for the comment.</param>
        /// <param name="value">New value for the comment.</param>
        /// <param name="clientId">The client identifier 
        /// who wants to change the comment.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NotAllowedToChangeCommentException"/>
        /// <returns></returns>
        [Transactional]
        Comment ChangeComment(long commentId, string title, string value, long clientId); 


    }
}
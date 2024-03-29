﻿using System.Collections.Generic;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        public List<Comment> FindCommentsOfProductAndClient(long productId, long clientId)
        {
            DbSet<Comment> commentList = Context.Set<Comment>();

            List<Comment> result =
                (from c in commentList
                 where c.productId == productId &&
                       c.clientId == clientId
                 orderby c.date
                 select c).ToList<Comment>();

            return result;
        }

        public List<Comment> FindCommentsByLabel(Label label)
        {

            DbSet<Comment> commentList = Context.Set<Comment>();

            List<Comment> result =
                (from c in commentList
                 where c.Labels.Select(l => l.id).Contains(label.id)
                 select c).ToList<Comment>();

            return result;

        }

        public List<Comment> FindCommentsOfProductPaged(long productId, int startIndex, int count)
        {
            DbSet<Comment> commentList = Context.Set<Comment>();

            List<Comment> result =
                (from c in commentList
                 where c.productId == productId
                 orderby c.date descending
                 select c).Skip(startIndex).Take(count).ToList<Comment>();

            return result;
        }

        public void ReplaceLabels(long commentId, List<Label> newLabels)
        {
            Comment comment = Find(commentId);
            comment.Labels = newLabels;
            Update(comment);
        }
    }
}

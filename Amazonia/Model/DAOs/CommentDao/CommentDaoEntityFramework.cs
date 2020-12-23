using System.Collections.Generic;
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

        public List<Comment> FindCommentsOfProduct(long productId)
        {
            DbSet<Comment> commentList = Context.Set<Comment>();

            List<Comment> result =
                (from c in commentList
                 where c.productId == productId
                 orderby c.date
                 select c).ToList<Comment>();

            return result;
        }

    }
}

using System.Collections.Generic;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        public List<Comment> FindCommentsOfProduct(long productId)
        {
            DbSet<Comment> commentList = Context.Set<Comment>();

            List<Comment> result = (List<Comment>)
                from c in commentList
                where c.productId == productId
                orderby c.date
                select c;

            return result;
        }
    }
}

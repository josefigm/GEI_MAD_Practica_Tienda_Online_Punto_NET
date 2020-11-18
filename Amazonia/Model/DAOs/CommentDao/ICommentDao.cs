using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        List<Comment> FindCommentsOfProduct(long productId);
    }
}

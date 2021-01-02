using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        List<Comment> FindCommentsOfProductAndClient(long productId, long clientId);

        List<Comment> FindCommentsOfProduct(long productId);

        List<Comment> FindCommentsOfProductPaged(long productId, int startIndex, int count);

        List<Comment> FindCommentsByLabel(Label label);
        
    }
}

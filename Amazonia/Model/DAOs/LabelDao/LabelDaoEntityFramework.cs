using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao
{
    public class LabelDaoEntityFramework : GenericDaoEntityFramework<Label, long>, ILabelDao
    {

        public List<Label> FindLabelsOfComment(Comment comment)
        {
            // TODO
            DbSet<Label> labelList = Context.Set<Label>();

            List<Label> result =
                (from l in labelList
                where l.Comments.Contains(comment)
                select l).ToList<Label>();

            return result;

        }
    }
}

using Es.Udc.DotNet.Amazonia.Model.LabelServiceImp.DTOs;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.Amazonia.Model.DAOs.LabelDao
{
    public class LabelDaoEntityFramework : GenericDaoEntityFramework<Label, long>, ILabelDao
    {

        public List<Label> FindLabelsOfComment(long commentId)
        {
            // TODO
            DbSet<Label> labelList = Context.Set<Label>();

            List<Label> result =
                (from l in labelList
                where l.Comments.Select(c => c.id).Contains(commentId)
                select l).ToList<Label>();

            return result;

        }

        public List<LabelDTO> FindMostUsedLabels()
        {
            DbSet<Label> labelList = Context.Set<Label>();

            // We only select the id in order to be as efficient as possible
            List<long> labelIds =
                (from l in labelList
                 orderby l.Comments.Count descending
                 select l.id).ToList<long>();

            // The labelDTOs are created
            List<LabelDTO> result = new List<LabelDTO>();
            for (int index = 0; index < labelIds.Count; index++)
            {
                result.Add(new LabelDTO(labelIds[index]));
            }

            return result;
        }

        public int GetNumberOfCommentsForLabel(long labelId)
        {
            Label label = Find(labelId);

            return label.Comments.Count();
        }
    }

}
